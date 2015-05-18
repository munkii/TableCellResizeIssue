using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace TableCellResizeIssue
{
    /// <summary>
    /// Simple class that holds a set of constraints and whether or not they have been applied
    /// </summary>
    internal class LayoutConstraintsSet
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LayoutConstraintsSet"/> has been applied.
        /// </summary>
        /// <value>
        ///   <c>true</c> if applied; otherwise, <c>false</c>.
        /// </value>
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the constraints.
        /// </summary>
        /// <value>
        /// The constraints.
        /// </value>
        public List<NSLayoutConstraint> Constraints { get; set; }
    }
}