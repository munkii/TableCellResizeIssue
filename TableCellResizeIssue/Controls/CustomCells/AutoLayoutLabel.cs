using System;
using UIKit;
using CoreGraphics;

namespace TableCellResizeIssue.CustomCells
{
    public class AutoLayoutLabel : UILabel
    {
        public override CGRect Bounds
        {
            get
            {
                return base.Bounds;
            }
            set
            {
                base.Bounds = value;
                if(this.Lines == 0 && Bounds.Size.Width != PreferredMaxLayoutWidth)
                {
                    PreferredMaxLayoutWidth = Bounds.Size.Width;
                    SetNeedsUpdateConstraints();
                }
            }
        }
    }
}

