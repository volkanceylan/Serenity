﻿using System;

namespace Serenity.Data
{
    /// <summary>
    ///   Used to define aliases like (T0).</summary>
    public class Alias : IAlias
    {
        /// <summary>
        /// Static t0 alias
        /// </summary>
        public static readonly Alias T0 = new Alias(0);

        /// <summary>
        /// Static t1 alias
        /// </summary>
        public static readonly Alias T1 = new Alias(1);

        /// <summary>
        /// Static t2 alias
        /// </summary>
        public static readonly Alias T2 = new Alias(2);

        /// <summary>
        /// Static t3 alias
        /// </summary>
        public static readonly Alias T3 = new Alias(3);

        /// <summary>
        /// Static t4 alias
        /// </summary>
        public static readonly Alias T4 = new Alias(4);

        /// <summary>
        /// Static t5 alias
        /// </summary>
        public static readonly Alias T5 = new Alias(5);

        /// <summary>
        /// Static t6 alias
        /// </summary>
        public static readonly Alias T6 = new Alias(6);

        /// <summary>
        /// Static t7 alias
        /// </summary>
        public static readonly Alias T7 = new Alias(7);

        /// <summary>
        /// Static t8 alias
        /// </summary>
        public static readonly Alias T8 = new Alias(8);

        /// <summary>
        /// Static t9 alias
        /// </summary>
        public static readonly Alias T9 = new Alias(9);

        private string alias;
        private string aliasDot;
        private string table;
        private string tableHint;

        /// <summary>
        /// Initializes a new instance of the <see cref="Alias"/> class.
        /// </summary>
        /// <param name="alias">The alias.</param>
        public Alias(int alias)
        {
            this.alias = alias.TableAlias();
            this.aliasDot = alias.TableAliasDot();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Alias"/> class.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <exception cref="System.ArgumentNullException">alias</exception>
        public Alias(string alias)
        {
            if (String.IsNullOrEmpty(alias))
                throw new ArgumentNullException(nameof(alias));

            this.alias = alias;
            this.aliasDot = alias + ".";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Alias"/> class.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="alias">The alias.</param>
        public Alias(string table, int alias) : this(alias)
        {
            this.table = table;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Alias"/> class.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="alias">The alias.</param>
        public Alias(string table, string alias) : this(alias)
        {
            this.table = table;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Alias"/> class.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="hint">The table hint.</param>
        public Alias(string table, int alias, string hint) : this(table, alias)
        {
            this.tableHint = hint;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Alias"/> class.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="hint">The table hint.</param>
        public Alias(string table, string alias, string hint) : this(table, alias)
        {
            this.tableHint = hint;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return alias; }
        }

        /// <summary>
        /// Gets the name dot.
        /// </summary>
        /// <value>
        /// The name dot.
        /// </value>
        public string NameDot
        {
            get { return aliasDot; }
        }

        /// <summary>
        /// Gets the table.
        /// </summary>
        /// <value>
        /// The table.
        /// </value>
        public string Table
        {
            get { return table; }
        }

        /// <summary>
        /// Gets the table hint.
        /// </summary>
        /// <value>
        /// The table hint.
        /// </value>
        public string TableHint
        {
            get { return tableHint; }
        }

        /// <summary>
        /// Gets the prefixed expression with the specified field name.
        /// </summary>
        /// <value>
        /// The <see cref="System.String"/>.
        /// </value>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>Expression like t0.fieldName</returns>
        public string this[string fieldName]
        {
            get { return this.aliasDot + fieldName; }
        }

        /// <summary>
        /// Gets the prefixed expression with the specified field.
        /// </summary>
        /// <value>
        /// The <see cref="System.String"/>.
        /// </value>
        /// <param name="field">The field.</param>
        /// <returns>Expression like t0.fieldName</returns>
        /// <exception cref="System.ArgumentNullException">field</exception>
        public string this[IField field]
        {
            get
            {
                if (field == null)
                    throw new ArgumentNullException(nameof(field));

                return this.aliasDot + field.Name;
            }
        }

        /// <summary>
        /// Gets a criteria containing prefixed field.
        /// Only here for backward compatibility.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>Criteria containing prefixed field</returns>
        public Criteria _(string field)
        {
            return new Criteria(this[field]);
        }

        /// <summary>
        /// Gets a criteria containing prefixed field.
        /// Only here for backward compatibility.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>Criteria containing prefixed field</returns>
        public Criteria _(IField field)
        {
            return new Criteria(this[field]);
        }

        /// <summary>
        /// Gets a criteria containing prefixed field.
        /// Only here for backward compatibility.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// String containing prefixed field
        /// </returns>
        public static string operator +(Alias alias, string fieldName)
        {
            return alias.aliasDot + fieldName;
        }

        /// <summary>
        /// Gets a criteria containing prefixed field.
        /// Only here for backward compatibility.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="field">The field.</param>
        /// <returns>
        /// String containing prefixed field
        /// </returns>
        /// <exception cref="System.ArgumentNullException">field</exception>
        public static string operator +(Alias alias, IField field)
        {
            if (field == null)
                throw new ArgumentNullException(nameof(field));

            return alias.aliasDot + field.Name;
        }
    }
}