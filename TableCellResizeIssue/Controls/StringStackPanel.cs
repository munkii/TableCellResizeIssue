//------------------------------------------------------------------
//
// Copyright (c) 2012 - 2014 Adaptive Apps Ltd. All rights reserved.
// 
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Copyright (c) Cirrious Ltd. http://www.cirrious.com
//
//------------------------------------------------------------------

namespace PatientHub.Framework.Touch.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Cirrious.FluentLayouts.Touch;
    using Cirrious.MvvmCross.Binding.Touch.Views;

    using CoreGraphics;

    using TableCellResizeIssue.CustomCells;

    using UIKit;

    public class StringStackPanel : MvxView
    {
        private List<string> items;
        
         /// <summary>
        /// Initializes a new instance of the <see cref="StringStackPanel"/> class.
        /// </summary>
        public StringStackPanel() : base()
        {
            this.CreateView();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="StringStackPanel"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public StringStackPanel(IntPtr handle)
            : base(handle)
        {
            this.CreateView();
        }

        public List<string> Items
        {
            get
            {
                return this.items;
            }

            set
            {
                if (this.items == value)
                {
                    return;
                }

                this.items = value;

                this.CreateStack();
            }
        }

        private void CreateStack()
        {
            foreach (UILabel label in this.Subviews)
            {
                label.RemoveFromSuperview();
            }

            if (this.Items != null)
            {
                foreach (string item in this.Items)
                {
                    this.AddSubview(new UILabel()
                                              {
                                                  Font = UIFont.PreferredCaption1, 
                                                  BackgroundColor = UIColor.Red, 
                                                  Lines = 0, 
                                                  LineBreakMode = UILineBreakMode.WordWrap,
                                                  Text = item, 
                                                  TranslatesAutoresizingMaskIntoConstraints = false
                                              });
                }

                ////this.LayoutManually();
                this.LayoutWithFluentCopy(this, null, this.Subviews);
                ////this.VerticalStackPanelConstraints(null, this.Subviews);
            }

            this.SetNeedsLayout();
            this.LayoutIfNeeded();
            this.InvalidateIntrinsicContentSize();
       }

        /// <summary>
        /// Layouts the Stack of Labels using a copy of FluentLayout's VerticalStackPanelConstraints. 
        /// Allows easy experimenting with the if (parentView is UIScrollView) line
        /// </summary>
        /// <param name="parentView">The parent view.</param>
        /// <param name="margins">The margins.</param>
        /// <param name="views">The views.</param>
        private void LayoutWithFluentCopy(UIView parentView, Margins margins,
                                                                              params UIView[] views)
        {
            margins = margins ?? new Margins();

            UIView previous = null;
            foreach (var view in views)
            {
                this.AddConstraints(view.Left().EqualTo().LeftOf(parentView).Plus(margins.Left));
                this.AddConstraints(view.Width().EqualTo().WidthOf(parentView).Minus(margins.Right + margins.Left));
                if (previous != null)
                    this.AddConstraints(view.Top().EqualTo().BottomOf(previous).Plus(margins.VSpacing));
                else
                    this.AddConstraints(view.Top().EqualTo().TopOf(parentView).Plus(margins.Top));
                previous = view;
            }
            ////if (parentView is UIScrollView)
            this.AddConstraints(previous.Bottom().EqualTo().BottomOf(parentView).Minus(margins.Bottom));
        }

        /// <summary>
        /// Layouts the Stack of Labels using FluentLayout but without calling VerticalStackPanelConstraints
        /// </summary>
        private void LayoutManually()
        {
            UILabel previousLabel = null;

            foreach (UILabel label in this.Subviews)
            {
                if (previousLabel == null)
                {
                    this.AddConstraints(label.AtTopOf(this));
                }
                else
                {
                    this.AddConstraints(label.Below(previousLabel));
                }

                this.AddConstraints(
                    label.AtLeftOf(this),
                    label.AtRightOf(this));

                if (label == this.Subviews.Last())
                {
                    this.AddConstraints(label.AtBottomOf(this));
                }

                previousLabel = label;
            }
        }

        /// <summary>
        /// Creates the layout.
        /// </summary>
        private void CreateView()
        {
            this.BackgroundColor = UIColor.Orange;
            this.TranslatesAutoresizingMaskIntoConstraints = false;

            this.SetNeedsUpdateConstraints();
        }
    }
}