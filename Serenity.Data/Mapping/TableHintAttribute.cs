using System;

namespace Serenity.Data.Mapping
{
    /// <summary>
    /// Determines table hint for the row.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class TableHintAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableHintAttribute"/> class.
        /// </summary>
        /// <param name="hint">The table hint.</param>
        /// <exception cref="System.ArgumentNullException">hint</exception>
        public TableHintAttribute(string hint)
        {
            if (string.IsNullOrEmpty(hint))
                throw new ArgumentNullException(nameof(hint));

            Hint = hint;
        }

        /// <summary>
        /// Gets the table hint.
        /// </summary>
        /// <value>
        /// The table hint.
        /// </value>
        public string Hint { get; private set; }
    }
}
