﻿using System;
using System.Collections.Generic;

namespace Serenity.Data
{
    /// <summary>
    ///   Corresponds to an SQL JOIN (INNER, OUTER, CROSS etc.)</summary>
    public abstract class Join : Alias
    {
        private IDictionary<string, Join> joins;
        private ICriteria onCriteria;
        private HashSet<string> referencedAliases;

        /// <summary>
        /// Gets the keyword.
        /// </summary>
        /// <returns></returns>
        public abstract string GetKeyword();

        /// <summary>
        /// Initializes a new instance of the <see cref="Join"/> class.
        /// </summary>
        /// <param name="joins">The joins dictionary.</param>
        /// <param name="toTable">To table.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="onCriteria">The ON criteria.</param>
        /// <param name="hint">The table hint.</param>
        /// <exception cref="System.ArgumentException"></exception>
        protected Join(IDictionary<string, Join> joins, string toTable, string alias, ICriteria onCriteria, string hint = null)
            : base(toTable, alias, hint)
        {
            this.joins = joins;
            this.onCriteria = onCriteria;

            if (!ReferenceEquals(this.onCriteria, null))
            {
                var aliases = JoinAliasLocator.Locate(this.onCriteria.ToStringIgnoreParams());
                if (aliases != null && aliases.Count > 0)
                    referencedAliases = aliases;
            }

            var toTableAliases = JoinAliasLocator.Locate(this.Table);
            if (toTableAliases != null && toTableAliases.Count > 0)
            {
                if (referencedAliases == null)
                    referencedAliases = toTableAliases;
                else
                {
                    foreach (var x in toTableAliases)
                        referencedAliases.Add(x);
                }
            }

            if (joins != null)
            {
                if (joins.ContainsKey(this.Name))
                    throw new ArgumentException(String.Format(
                        "There is already a join with alias '{0}'", this.Name));

                joins.Add(this.Name, this);
            }
        }

        /// <summary>
        /// Gets the ON criteria.
        /// </summary>
        /// <value>
        /// The ON criteria.
        /// </value>
        public ICriteria OnCriteria
        {
            get
            {
                return onCriteria;
            }
        }

        /// <summary>
        /// Gets the referenced aliases.
        /// </summary>
        /// <value>
        /// The referenced aliases.
        /// </value>
        public HashSet<string> ReferencedAliases
        {
            get
            {
                return referencedAliases;
            }
        }

        /// <summary>
        /// Gets the joins.
        /// </summary>
        /// <value>
        /// The joins.
        /// </value>
        public IDictionary<string, Join> Joins
        {
            get { return joins; }
        }

        /// <summary>
        /// Gets or sets the type of the row.
        /// </summary>
        /// <value>
        /// The type of the row.
        /// </value>
        public Type RowType { get; set; }
    }
}