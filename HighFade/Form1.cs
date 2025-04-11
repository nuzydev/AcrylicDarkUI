using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HighFade
{
    public partial class Form1 : Form
    {
        private const int CORNER_RADIUS = 3;

        private readonly Color BACKGROUND_COLOR = Color.FromArgb(15, 15, 15);

        private bool mouseDown;
        private Point lastLocation;

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(500, 500);
            this.Text = "Blur UI";
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = BACKGROUND_COLOR;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;

            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CORNER_RADIUS, CORNER_RADIUS));

            EnableBlur(this.Handle);


            this.MouseDown += Form_MouseDown;
            this.MouseMove += Form_MouseMove;
            this.MouseUp += Form_MouseUp;

            this.SizeChanged += (s, e) =>
            {
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CORNER_RADIUS, CORNER_RADIUS));
            };
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        #region Win32 API e métodos para blur e cantos arredondados

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        [StructLayout(LayoutKind.Sequential)]
        private struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        private enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }

        private enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        private void EnableBlur(IntPtr handle)
        {
            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

            accent.GradientColor = (230 << 24) | (15 << 16) | (15 << 8) | 15;

            var accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                Data = accentPtr,
                SizeOfData = accentStructSize
            };

            SetWindowCompositionAttribute(handle, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }
        #endregion
    }
}
