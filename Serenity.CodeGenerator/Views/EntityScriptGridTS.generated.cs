﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Serenity.CodeGenerator.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class EntityScriptGridTS : RazorGenerator.Templating.RazorTemplateBase
    {
#line hidden
 public dynamic Model { get; set; } 
        public override void Execute()
        {


WriteLiteral(Environment.NewLine);



                                                   
    var dotModule = Model.Module == null ? "" : ("." + Model.Module);
    var moduleDot = Model.Module == null ? "" : (Model.Module + ".");


WriteLiteral("namespace ");


      Write(Model.RootNamespace);


                            Write(dotModule);

WriteLiteral(" {" + Environment.NewLine + "    " + Environment.NewLine + "    ");


WriteLiteral("@Serenity.Decorators.registerClass()" + Environment.NewLine + "    export class ");


             Write(Model.ClassName);

WriteLiteral("Grid extends Serenity.EntityGrid<");


                                                                Write(Model.RowClassName);

WriteLiteral(", any> {" + Environment.NewLine + "        protected getColumnsKey() { return \'");


                                        Write(moduleDot);


                                                    Write(Model.ClassName);

WriteLiteral("\'; }" + Environment.NewLine + "        protected getDialogType() { return ");


                                       Write(Model.ClassName);


                                                             WriteLiteral("Dialog; }");

                                                                       if (Model.Identity != null) {
WriteLiteral(Environment.NewLine + "        protected getIdProperty() { return ");


                                       Write(Model.RowClassName);

WriteLiteral(".idProperty; }");


                                                                                     }

WriteLiteral(Environment.NewLine + "        protected getLocalTextPrefix() { return ");


                                            Write(Model.RowClassName);

WriteLiteral(".localTextPrefix; }" + Environment.NewLine + "        protected getService() { return ");


                                    Write(Model.ClassName);

WriteLiteral("Service.baseUrl; }" + Environment.NewLine + Environment.NewLine + "        constructor(container: JQuery) {" + Environment.NewLine + "            super" +
"(container);" + Environment.NewLine + "        }" + Environment.NewLine + "    }" + Environment.NewLine + "}");


        }
    }
}
#pragma warning restore 1591
