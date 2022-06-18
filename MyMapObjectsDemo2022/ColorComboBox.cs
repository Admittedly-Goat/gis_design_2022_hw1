//颜色拾取框
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyMapObjectsDemo2022
{
    //event handler delegate
    public delegate void ColorChangedHandler(object sender, ColorChangeArgs e);

    [ToolboxBitmap(typeof(ComboBox))]
    public partial class ColorComboBox : ComboBox
    {
        private PopupWindow popupWnd;
        private readonly ColorPopup colors = new ColorPopup();
        private Color selectedColor = Color.Black;
        private readonly Timer timer = new Timer();
        public event ColorChangedHandler ColorChanged;

        //constructor...
        public ColorComboBox()
            : this(Color.Black)
        {
        }

        public ColorComboBox(Color selectedColor)
        {
            SuspendLayout();
            // 
            // ColorCombo
            // 
            AutoSize = false;
            Size = new Size(39, 22);
            Text = string.Empty;
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
            ItemHeight = 16;

            timer.Tick += new EventHandler(OnCheckStatus);
            timer.Interval = 50;
            timer.Start();
            colors.SelectedColor = this.selectedColor = selectedColor;
            ResumeLayout(false);
        }

        [DefaultValue(typeof(Color), "Black")]
        public Color SelectedColor
        {
            get => selectedColor;
            set
            {
                selectedColor = value;
                colors.SelectedColor = value;
                Invalidate();
            }
        }

        protected override void WndProc(ref Message m)
        {
            //256: WM_KEYDOWN, 513: WM_LBUTTONDOWN, 515: WM_LBUTTONDBLCLK
            if (m.Msg == 256 || m.Msg == 513 || m.Msg == 515)
            {
                if (m.Msg == 513)
                {
                    PopupColorPalette();
                }

                return;
            }
            base.WndProc(ref m);
        }

        private void PopupColorPalette()
        {
            //create a popup window
            popupWnd = new PopupWindow(colors);

            //calculate its position in screen coordinates
            Rectangle rect = Bounds;
            rect = Parent.RectangleToScreen(rect);
            Point pt = new Point(rect.Left, rect.Bottom);

            //tell it that we want the ColorChanged event
            popupWnd.ColorChanged += new ColorChangedHandler(OnColorChanged);

            //show the popup
            popupWnd.Show(pt);
            //disable the button so that the user can't click it
            //while the popup is being displayed
            Enabled = false;
            timer.Start();
        }

        //event handler for the color change event from the popup window
        //simply relay the event to the parent control
        protected void OnColorChanged(object sender, ColorChangeArgs e)
        {
            //if a someone wants the event, and the color has actually changed
            //call the event handler
            if (ColorChanged != null && e.color != selectedColor)
            {
                selectedColor = e.color;
                ColorChanged(this, e);
            }
            else //otherwise simply make note of the new color
            {
                selectedColor = e.color;
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            e.DrawBackground();
            SolidBrush brush = new SolidBrush(selectedColor);
            Rectangle rect = e.Bounds;
            rect.Width -= 1;
            rect.Height -= 1;
            g.FillRectangle(brush, rect);
            g.DrawRectangle(Pens.Black, rect);
            e.DrawFocusRectangle();
        }

        //This is the timer call back function. It checks to see 
        //if the popup went from a visible state to an close state
        //if so then it will uncheck and enable the button
        private void OnCheckStatus(object sender, EventArgs e)
        {
            if (popupWnd != null && !popupWnd.Visible)
            {
                timer.Stop();
                Enabled = true;
            }
        }

        /// <summary>
        /// a button style radio button that shows a color
        /// </summary>
        private class ColorRadioButton : RadioButton
        {
            public ColorRadioButton(Color color, Color backColor)
            {
                ClientSize = new Size(21, 21);
                Appearance = Appearance.Button;
                Name = "button";
                Visible = true;
                ForeColor = color;
                FlatAppearance.BorderColor = backColor;
                FlatAppearance.BorderSize = 0;
                FlatStyle = FlatStyle.Flat;
                Paint += new PaintEventHandler(OnPaintButton);
            }

            private void OnPaintButton(object sender, PaintEventArgs e)
            {
                //paint a square on the face of the button using the controls foreground color
                Rectangle colorRect = new Rectangle(ClientRectangle.Left + 5, ClientRectangle.Top + 5, ClientRectangle.Width - 9, ClientRectangle.Height - 9);
                e.Graphics.FillRectangle(new SolidBrush(ForeColor), colorRect);
                e.Graphics.DrawRectangle(Pens.DarkGray, colorRect);
            }
        }

        ///<summary>
        ///this is the popup window.  This window will be the parent of the 
        ///window with the color controls on it
        ///</summary>
        private class PopupWindow : ToolStripDropDown
        {
            public event ColorChangedHandler ColorChanged;
            private readonly ToolStripControlHost host;
            private readonly ColorPopup content;

            public Color SelectedColor => content.SelectedColor;

            public PopupWindow(ColorPopup content)
            {
                this.content = content ?? throw new ArgumentNullException("content");
                AutoSize = false;
                DoubleBuffered = true;
                ResizeRedraw = true;
                //create a host that will host the content
                host = new ToolStripControlHost(content);

                Padding = Margin = host.Padding = host.Margin = Padding.Empty;
                MinimumSize = content.MinimumSize;
                content.MinimumSize = content.Size;
                MaximumSize = new Size(content.Size.Width + 1, content.Size.Height + 1);
                content.MaximumSize = new Size(content.Size.Width + 1, content.Size.Height + 1);
                Size = new Size(content.Size.Width + 1, content.Size.Height + 1);

                content.Location = Point.Empty;
                //add the host to the list
                _ = Items.Add(host);
            }

            protected override void OnClosed(ToolStripDropDownClosedEventArgs e)
            {
                //when the window close tell the parent that the color changed
                ColorChanged?.Invoke(this, new ColorChangeArgs(SelectedColor));
            }
        }

        ///<summary>
        ///this class represends the control that has all the color radio buttons.
        ///this control gets embedded into the PopupWindow class.
        ///</summary>
        private class ColorPopup : UserControl
        {
            //private Color[] colors = { Color.Black, Color.Gray, Color.Maroon, Color.Olive, Color.Green, Color.Teal, Color.Navy, Color.Purple, Color.White, Color.Silver, Color.Red, Color.Yellow, Color.Lime, Color.Aqua, Color.Blue, Color.Fuchsia };
            private readonly Color[] colors = {
                Color.Black, Color.Navy, Color.DarkGreen, Color.DarkCyan, Color.DarkRed, Color.DarkMagenta, Color.Olive,
                Color.LightGray, Color.DarkGray, Color.Blue, Color.Lime, Color.Cyan, Color.Red, Color.Fuchsia,
                Color.Yellow, Color.White, Color.RoyalBlue, Color.MediumBlue,  Color.LightGreen, Color.MediumSpringGreen, Color.Chocolate,
                Color.Pink, Color.Khaki, Color.WhiteSmoke, Color.BlueViolet, Color.DeepSkyBlue, Color.OliveDrab, Color.SteelBlue,
                Color.DarkOrange, Color.Tomato, Color.HotPink, Color.DimGray,
            };
            private readonly string[] colorNames = {
                "黑色", "藏青", "深绿", "深青", "红褐", "洋红", "褐绿",
                "浅灰", "灰色", "蓝色", "绿色", "青色", "红色", "紫红",
                "黄色", "白色", "蓝灰", "藏蓝", "淡绿", "青绿", "黄褐",
                "粉红", "嫩黄", "银白", "紫色", "天蓝", "灰绿", "青蓝",
                "橙黄", "桃红", "英红", "深灰"
            };
            private readonly ToolTip toolTip = new ToolTip();
            private ColorRadioButton[] buttons;
            private Button moreColorsBtn;
            private Color selectedColor = Color.Black;

            ///<summary>
            ///get or set the selected color
            ///</summary>
            public Color SelectedColor
            {
                get => selectedColor;
                set
                {
                    selectedColor = value;
                    Color[] colors = this.colors;
                    for (int i = 0; i < colors.Length; i++)
                    {
                        buttons[i].Checked = selectedColor == colors[i];
                    }
                }
            }

            private void InitializeComponent()
            {
                SuspendLayout();
                Name = "Color Popup";
                Text = string.Empty;
                ResumeLayout(false);
            }

            public ColorPopup()
            {
                InitializeComponent();

                SetupButtons();
                Paint += new PaintEventHandler(OnPaintBorder);
            }

            //place the buttons on the window.
            private void SetupButtons()
            {
                Controls.Clear();

                int x = 1;
                int y = 2;
                int breakCount = 7;
                Color[] colors = this.colors;
                buttons = new ColorRadioButton[colors.Length];
                ClientSize = new Size(139, 137);
                //color buttons
                for (int i = 0; i < colors.Length; i++)
                {
                    if (i > 0 && i % breakCount == 0)
                    {
                        y += 19;
                        x = 1;
                    }
                    buttons[i] = new ColorRadioButton(colors[i], BackColor)
                    {
                        Location = new Point(x, y)
                    };
                    // toolTip.SetToolTip(buttons[i], colorNames[i]);
                    Controls.Add(buttons[i]);
                    buttons[i].Click += new EventHandler(BtnClicked);
                    if (selectedColor == colors[i])
                    {
                        buttons[i].Checked = true;
                    }

                    x += 19;
                }

                //line...
                y += 24;
                Label label = new Label
                {
                    AutoSize = false,
                    Text = string.Empty,
                    Width = Width - 5,
                    Height = 2,
                    BorderStyle = BorderStyle.Fixed3D,
                    Location = new Point(4, y)
                };
                Controls.Add(label);

                //button
                y += 7;
                moreColorsBtn = new Button
                {
                    FlatStyle = FlatStyle.Popup,
                    Text = "其它颜色...",
                    Location = new Point(6, y),
                    ClientSize = new Size(127, 23)
                };
                moreColorsBtn.Click += new EventHandler(OnMoreClicked);
                Controls.Add(moreColorsBtn);
            }

            private void OnPaintBorder(object sender, PaintEventArgs e)
            {
                Rectangle rect = ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                e.Graphics.DrawRectangle(new Pen(SystemColors.WindowFrame), rect);
            }

            public void BtnClicked(object sender, EventArgs e)
            {
                selectedColor = ((ColorRadioButton)sender).ForeColor;
                ((ToolStripDropDown)Parent).Close();
            }

            public void OnMoreClicked(object sender, EventArgs e)
            {
                ColorDialog dlg = new ColorDialog
                {
                    Color = SelectedColor
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    selectedColor = dlg.Color;
                } ((ToolStripDropDown)Parent).Close();
            }
        }
    }

    //define the color changed event argument
    public class ColorChangeArgs : System.EventArgs
    {
        //the selected color
        public Color color;
        public ColorChangeArgs(Color color)
        {
            this.color = color;
        }
    }
}
