﻿#if ASPNETMVC
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Web.Hosting;
using Serenity.Services;

namespace Serenity.Web.HttpHandlers
{
    /// <summary>
    ///   HTTP handler to server script resources (in upload folder), only used when script packages is enabled.</summary>
    public class DynamicScriptHandler : IHttpHandler
    {
        public static void ProcessScriptRequest(HttpContext context, string scriptKey,
            string contentType)
        {
            var response = context.Response;
            var request = context.Request;

            DynamicScriptManager.Script script;
            try
            {
                script = DynamicScriptManager.GetScript(scriptKey);
            }
            catch (ValidationError ve)
            {
                if (ve.ErrorCode == "AccessDenied")
                {
                    response.StatusCode = 403;
                    return;
                }

                throw;
            }

            if (script == null)
            {
                response.StatusCode = 404;
                response.StatusDescription = "A dynamic script with key " + scriptKey + " is not found!";
                return;
            }

            int expiresOffset = 365; // Cache for 365 days in browser cache
            response.ContentType = contentType;
            response.Charset = "utf-8";

            response.Cache.SetExpires(DateTime.Now.AddDays(expiresOffset));

            // allow CDNs to cache anonymous resources
            if (!string.IsNullOrEmpty(request.QueryString["v"]) &&
                !Authorization.IsLoggedIn)
                response.Cache.SetCacheability(HttpCacheability.Public);
            else
                response.Cache.SetCacheability(HttpCacheability.Private);

            response.Cache.SetValidUntilExpires(false);

            response.Cache.VaryByHeaders["Accept-Encoding"] = true;

            var enc = Regex.Replace("" + request.Headers["Accept-Encoding"], @"\s+", "").ToLower();
            var supportsGzip = script.CompressedBytes != null && enc.IndexOf("gzip") != -1 || request.Headers["---------------"] != null;

            if (supportsGzip)
                response.AppendHeader("Content-Encoding", "gzip");

            WriteWithIfModifiedSinceControl(context, 
                supportsGzip ? script.CompressedBytes : script.UncompressedBytes, script.Time);
        }

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;

            
            /*WPS change .Url.AbsolutePath from to .Url.LocalPath for Non-Ascii Entity Names
		        AbsolutePath	"/DynJS.axd/Lookup.ACMS.Vw%E9%83%A8%E9%97%A8%E5%90%8D%E7%A7%B0.js"	string
		        LocalPath	    "/DynJS.axd/Lookup.ACMS.Vw部门名称.js"	string
             */
            var path = request.Url.LocalPath;            
            string dyn = "/DynJS.axd/";
            var pos = path.IndexOf(dyn, StringComparison.InvariantCultureIgnoreCase);
            if (pos >= 0)
                path = path.Substring(pos + dyn.Length);

            var contentType = "text/javascript";
            if (path.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
                path = path.Substring(0, path.Length - 3);
            else if (path.EndsWith(".css", StringComparison.OrdinalIgnoreCase))
            {
                contentType = "text/css";
                path = path.Substring(0, path.Length - 4);
            }

            ProcessScriptRequest(context, path, contentType);
        }

        public static void WriteWithIfModifiedSinceControl(HttpContext context, byte[] bytes, DateTime lastWriteTime)
        {
            string ifModifiedSince = context.Request.Headers["If-Modified-Since"];
            if (ifModifiedSince != null && ifModifiedSince.Length > 0)
            {
                DateTime date;
                if (DateTime.TryParseExact(ifModifiedSince, "R", Invariants.DateTimeFormat, DateTimeStyles.None,
                    out date))
                {
                    if (date.Year == lastWriteTime.Year &&
                        date.Month == lastWriteTime.Month &&
                        date.Day == lastWriteTime.Day &&
                        date.Hour == lastWriteTime.Hour &&
                        date.Minute == lastWriteTime.Minute &&
                        date.Second == lastWriteTime.Second)
                    {
                        context.Response.StatusCode = 304;
                        context.Response.StatusDescription = "Not Modified";
                        return;
                    }
                }
            }

            var utcNow = DateTime.UtcNow;
            if (lastWriteTime >= utcNow)
                lastWriteTime = utcNow;
            context.Response.Cache.SetLastModified(lastWriteTime);
            context.Response.BinaryWrite(bytes);
        }

        /// <summary>
        ///   Returns true to set this handler as reusable</summary>
        public bool IsReusable
        {
            get { return true; }
        }
    }
}
#endif
