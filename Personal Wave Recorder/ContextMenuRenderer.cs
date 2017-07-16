#region Directives
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Diagnostics;
#endregion

namespace Personal_Wave_Recorder
{
    public class ContextMenuRenderer : ToolStripSystemRenderer
    {
        #region Enums
        private enum ButtonSelectedState : int
        {
            None = 0,
            Disabled,
            Checked,
            Focused,
            Pressed
        }
        #endregion

        #region Constructor
        public ContextMenuRenderer()
        {
            this.FocusedItemForeColor = Color.Black;
            this.FocusedItemGradientBegin = Color.White;
            this.FocusedItemGradientEnd = Color.LightGray;
            this.FocusedItemBorderColor = Color.DarkGray;
            this.MenuBackGroundColor = Color.FromArgb(48, 48, 48);
            this.MenuBorderColorLight = Color.DarkGray;
            this.MenuBorderColorDark = Color.FromArgb(96, 96, 96);
            this.MenuImageMarginColor = Color.FromArgb(32, 32, 32);
            this.MenuItemForeColor = Color.LightGray;
            this.SeperatorInnerColor = Color.FromArgb(64, 64, 64);
            this.SeperatorOuterColor = Color.DarkGray;
            this.MenuImageMarginText = "";
            this.CheckImageColor = Color.WhiteSmoke;
        }
        #endregion

        #region Properties
        /// <summary>Get/Set the checkbox image color.</summary>
        public Color CheckImageColor { get; set; }

        /// <summary>Get/Set the focused item border color.</summary>
        public Color FocusedItemBorderColor { get; set; }

        /// <summary>Get/Set the  focused item ForeColor.</summary>
        public Color FocusedItemForeColor { get; set; }

        /// <summary>Get/Set the starting color of the focused item gradient.</summary>
        public Color FocusedItemGradientBegin { get; set; }

        /// <summary>Get/Set the ending color of the focused item gradient.</summary>
        public Color FocusedItemGradientEnd { get; set; }

        /// <summary>Get/Set the background color.</summary>
        public Color MenuBackGroundColor { get; set; }

        /// <summary>Get/Set the dark border color.</summary>
        public Color MenuBorderColorDark { get; set; }

        /// <summary>Get/Set the light border color.</summary>
        public Color MenuBorderColorLight { get; set; }

        /// <summary>Get/Set the border strip color.</summary>
        public Color MenuImageMarginColor { get; set; }

        /// <summary>Get/Set the border strip text.</summary>
        public string MenuImageMarginText { get; set; }

        /// <summary>Get/Set the ForeColor.</summary>
        public Color MenuItemForeColor { get; set; }

        /// <summary>Get/Set the seperator inner color.</summary>
        public Color SeperatorInnerColor { get; set; }

        /// <summary>Get/Set the seperator outer color.</summary>
        public Color SeperatorOuterColor { get; set; }
        #endregion

        #region Overrides
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.ToolStrip is ContextMenuStrip)
            {
                Rectangle bounds = new Rectangle(Point.Empty, e.Item.Bounds.Size);
                DrawMenuItem(e.Graphics, bounds, GetButtonState(e.ToolStrip, e.Item));
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            if (e.ToolStrip is ContextMenuStrip)
            {
                Rectangle bounds = new Rectangle(Point.Empty, e.Item.Bounds.Size);
                DrawSeperator(e.Graphics, bounds);
            }
            else
            {
                base.OnRenderSeparator(e);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.ToolStrip is ContextMenuStrip)
            {
                if (!e.Item.Enabled)
                {
                    e.TextColor = Color.FromKnownColor(KnownColor.InactiveCaptionText);
                }
                else
                {
                    if (e.Item.Pressed || e.Item.Selected)
                        e.TextColor = this.FocusedItemForeColor;
                    else
                        e.TextColor = this.MenuItemForeColor;
                }
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                base.OnRenderItemText(e);
            }
            else
            {
                base.OnRenderItemText(e);
            }
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is ContextMenuStrip)
            {
                Rectangle bounds = new Rectangle(Point.Empty, e.ToolStrip.Bounds.Size);
                DrawBackGround(e.Graphics, e.ConnectedArea, e.ToolStrip);
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is ContextMenuStrip)
            {
                Rectangle bounds = new Rectangle(Point.Empty, e.ToolStrip.Bounds.Size);
                bounds.Inflate(1, 1);
                DrawFrame(e.Graphics, bounds, this.MenuBorderColorLight, this.MenuBorderColorDark);
            }
            else
            {
                base.OnRenderToolStripBorder(e);
            }
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            if (e.ToolStrip is ContextMenuStrip)
            {
                CheckState state = CheckState.Unchecked;
                if (e.Item is ToolStripMenuItem)
                {
                    ToolStripMenuItem item = (ToolStripMenuItem)e.Item;
                    state = item.CheckState;
                }
                DrawCheckBox(e.Graphics, e.ImageRectangle, state);
            }
            else
            {
                base.OnRenderItemCheck(e);
            }
        }
        #endregion

        #region Methods
        private void DrawBackGround(Graphics g, Rectangle area, ToolStrip toolStrip)
        {
            Rectangle bounds = new Rectangle(Point.Empty, toolStrip.Bounds.Size);

            // right to left offset
            int offset;
            if (toolStrip.RightToLeft == RightToLeft.Yes)
                offset = 2;
            else
                offset = 25;
            //!area.IsEmpty
            if (this.MenuImageMarginColor == Color.Transparent)
            {
                DrawFrame(g, bounds, this.MenuBorderColorLight, this.MenuBorderColorDark);
                bounds.Inflate(-1, -1);
                using (Brush fillBrush = new SolidBrush(this.MenuBackGroundColor))
                    g.FillRectangle(fillBrush, bounds);
            }
            else
            {
                Rectangle borderRect = new Rectangle(0, 0, 25, bounds.Height);
                DrawFrame(g, borderRect, this.MenuBorderColorLight, this.MenuBorderColorDark);
                borderRect.Inflate(-1, -1);
                using (Brush fillBrush = new SolidBrush(this.MenuImageMarginColor))
                    g.FillRectangle(fillBrush, borderRect);
                bounds.X = 25;
                bounds.Width -= 25;
                using (Brush bodyBrush = new SolidBrush(this.MenuBackGroundColor))
                    g.FillRectangle(bodyBrush, bounds);
                // margin text
                if (!string.IsNullOrEmpty(this.MenuImageMarginText))
                {
                    using (StringFormat sf = new StringFormat())
                    {
                        using (GraphicsMode md = new GraphicsMode(g, SmoothingMode.HighQuality))
                        {
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                            sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = StringAlignment.Near;
                            int voffset = 0;
                            int hoffset = 0;
                            // vertical text
                            sf.FormatFlags = StringFormatFlags.DirectionVertical;

                            // create a new matrix and rotate
                            Matrix mt = new Matrix();
                            mt.Rotate(180f);
                            // apply the transform
                            g.Transform = mt;

                            // draw the text
                            using (Font ft = new Font("Arial", 12f, FontStyle.Bold))
                            {
                                SizeF sz = g.MeasureString(this.MenuImageMarginText, ft);
                                hoffset = (borderRect.Width - (int)sz.Height) / 2;
                                voffset += -borderRect.Bottom;
                                hoffset += -borderRect.Right;

                                using (Brush captionBrush = new SolidBrush(this.MenuItemForeColor))
                                    g.DrawString(this.MenuImageMarginText, ft, captionBrush, new RectangleF(hoffset, voffset, borderRect.Width, borderRect.Height), sf);
                            }
                        }
                    }
                }
            }
        }

        private void DrawCheckBox(Graphics g, Rectangle bounds, CheckState state)
        {
            switch (state)
            {
                case CheckState.Checked:
                    {
                        using (GraphicsMode md = new GraphicsMode(g, SmoothingMode.HighQuality))
                        {
                            using (GraphicsPath gp = new GraphicsPath())
                            {
                                bounds.Inflate(-6, -6);
                                // Get the center point of the rect
                                int x = bounds.X + bounds.Width / 2;
                                int y = bounds.Y + bounds.Height / 2;
                                gp.AddLine(x - 4, y, x - 2, y + 4);
                                gp.AddLine(x - 2, y + 4, x + 3, y - 5);
                                // Draw the tick with a thickish brush
                                using (Pen tickPen = new Pen(this.CheckImageColor, 2f))
                                    g.DrawPath(tickPen, gp);
                            }
                        }
                        break;
                    }
                case CheckState.Indeterminate:
                    {
                        using (GraphicsMode md = new GraphicsMode(g, SmoothingMode.HighQuality))
                        {
                            bounds.Inflate(-6, -6);
                            bounds.X--;
                            using (GraphicsPath gp = CreateRoundRectanglePath(g, bounds, 2f))
                            {
                                using (Brush fillBrush = new SolidBrush(Color.LightGray))
                                    g.FillPath(fillBrush, gp);
                                using (Pen tickPen = new Pen(this.CheckImageColor, 2f))
                                    g.DrawPath(tickPen, gp);
                            }
                        }
                        break;
                    }
            }
        }

        private void DrawFrame(Graphics g, Rectangle bounds, Color light, Color dark)
        {
            using (Pen lightPen = new Pen(light), darkPen = new Pen(dark))
            {
                g.DrawLines(lightPen, new Point[] {
                        new Point (0, bounds.Height - 1), 
                        new Point (0, 0), 
                        new Point (bounds.Width - 1, 0)});
                g.DrawLines(darkPen, new Point[] {
                        new Point (0, bounds.Height - 1), 
                        new Point (bounds.Width - 1, bounds.Height - 1), 
                        new Point (bounds.Width - 1, 0)});
            }
        }

        private void DrawMenuItem(Graphics g, Rectangle bounds, ButtonSelectedState state)
        {
            if (state == ButtonSelectedState.Pressed)
            {
                bounds.Inflate(-1, -1);
                using (Brush backBrush = new SolidBrush(this.MenuImageMarginColor))
                    g.FillRectangle(backBrush, bounds);
                // draw connecting frame 
                if (this.MenuImageMarginColor != Color.Transparent)
                {
                    using (Pen framePen = new Pen(this.MenuBorderColorDark))
                        g.DrawLine(framePen, new Point(24, bounds.Y), new Point(24, bounds.Height));
                }
            }
            else if (state == ButtonSelectedState.Focused)
            {
                DrawSelectorBar(g, bounds);
            }
        }

        private void DrawSeperator(Graphics g, Rectangle bounds)
        {
            int offset = (this.MenuImageMarginColor == Color.Transparent ? 4 : 26);
            using (Pen inner = new Pen(this.SeperatorOuterColor, 1f), outer = new Pen(this.SeperatorInnerColor, 1f))
            {
                g.DrawLine(outer, new Point(bounds.X + offset, bounds.Y + 1), new Point(bounds.Width - 4, bounds.Y + 1));
                g.DrawLine(inner, new Point(bounds.X + offset, bounds.Y + 2), new Point(bounds.Width - 4, bounds.Y + 2));
            }
        }

        private void DrawSelectorBar(Graphics g, Rectangle bounds)
        {
            bounds.Inflate(-2, -1);
            using (GraphicsMode md = new GraphicsMode(g, SmoothingMode.HighQuality))
            {
                using (GraphicsPath barPath = CreateRoundRectanglePath(g, bounds, 2f))
                {
                    // draw the border
                    using (Pen borderPen = new Pen(this.FocusedItemBorderColor, 1f))
                        g.DrawPath(borderPen, barPath);

                    // fill with a gradient
                    using (LinearGradientBrush fillBrush = new LinearGradientBrush(bounds, Color.FromArgb(150, this.FocusedItemGradientBegin), Color.FromArgb(50, this.FocusedItemGradientEnd), LinearGradientMode.Vertical))
                    {
                        Blend blnd = new Blend();
                        blnd.Positions = new float[] { 0f, .4f, .5f, .8f, 1f };
                        blnd.Factors = new float[] { 0f, .2f, .5f, 1f, .6f };
                        fillBrush.Blend = blnd;
                        // fill the path
                        g.FillPath(fillBrush, barPath);
                    }
                }
            }
            
        }

        private GraphicsPath CreateRoundRectanglePath(Graphics g, Rectangle bounds, float radius)
        {
            // create a path
            GraphicsPath pathBounds = new GraphicsPath();
            // arc top left
            pathBounds.AddArc(bounds.Left, bounds.Top, radius, radius, 180, 90);
            // line top
            pathBounds.AddLine(bounds.Left + radius, bounds.Top, bounds.Right - radius, bounds.Top);
            // arc top right
            pathBounds.AddArc(bounds.Right - radius, bounds.Top, radius, radius, 270, 90);
            // line right
            pathBounds.AddLine(bounds.Right, bounds.Top + radius, bounds.Right, bounds.Bottom - radius);
            // arc bottom right
            pathBounds.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
            // line bottom
            pathBounds.AddLine(bounds.Right - radius, bounds.Bottom, bounds.Left + radius, bounds.Bottom);
            // arc bottom left
            pathBounds.AddArc(bounds.Left, bounds.Bottom - radius, radius, radius, 90, 90);
            // line left
            pathBounds.AddLine(bounds.Left, bounds.Bottom - radius, bounds.Left, bounds.Top + radius);
            pathBounds.CloseFigure();
            return pathBounds;
        }

        private ButtonSelectedState GetButtonState(ToolStrip toolStrip, ToolStripItem item)
        {
            if (!item.Enabled)
                return ButtonSelectedState.Disabled;

            if (item is ToolStripButton)
            {
                ToolStripButton button = (ToolStripButton)item;
                if (button.Checked)
                    return ButtonSelectedState.Checked;
            }

            if (item.Pressed)
            {
                return ButtonSelectedState.Pressed;
            }
            else
            {
                Point cursorPos = toolStrip.PointToClient(Cursor.Position);
                if ((item.Visible) && (item.Bounds.Contains(cursorPos)))
                    return ButtonSelectedState.Focused;
                else
                    return ButtonSelectedState.None;
            }
        }

        private float ParseColor(byte clr)
        {
            return clr == 0 ? 0 : ((float)clr / 255);
        }
        #endregion

        #region Graphics Mode
        /// <summary>Maintains graphic object state</summary>
        internal class GraphicsMode : IDisposable
        {
            #region Fields
            private Graphics _gGraphicCopy;
            private SmoothingMode _eOldMode;
            #endregion

            #region Methods
            /// <summary>
            /// Initialize a new instance of the class.
            /// </summary>
            /// <param name="g">Graphics instance.</param>
            /// <param name="mode">Desired Smoothing mode.</param>
            public GraphicsMode(Graphics g, SmoothingMode mode)
            {
                _gGraphicCopy = g;
                _eOldMode = _gGraphicCopy.SmoothingMode;
                _gGraphicCopy.SmoothingMode = mode;
            }

            /// <summary>
            /// Revert the SmoothingMode to original setting.
            /// </summary>
            public void Dispose()
            {
                _gGraphicCopy.SmoothingMode = _eOldMode;
            }
            #endregion
        }
        #endregion
    }
}
