namespace NTDLS.WinFormsHelpers
{
    /// <summary>
    /// Support for WinForms dark mode, which follows the Windows "app mode" setting.
    /// Requires net10.0-windows or later and the Windows App SDK.
    /// Call Theming.InitializeApplication() before creating any forms to enable dark mode.
    /// </summary>
    public static class Theming
    {
#pragma warning disable WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        /// <summary>
        /// Gets a value indicating whether the application is using a dark theme.
        /// </summary>
        /// <remarks>Delegates to Application.IsDarkModeEnabled. The value reflects the current
        /// application or system theme and may change at runtime.</remarks>

        public static bool IsDarkMode => Application.IsDarkModeEnabled;
        /// <summary>
        /// Follows the Windows light/dark setting. Must be called before any form is created.
        /// </summary>
        public static void InitializeApplication() => Application.SetColorMode(SystemColorMode.System);
#pragma warning restore WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

        /// <summary>
        /// Represents the color used for hyperlinks.
        /// </summary>
        public static readonly Color LinkColor = Color.FromArgb(0x8A, 0xB4, 0xF8);

        /// <summary>
        /// Color used for active links.
        /// </summary>
        /// <remarks>Used for the active state of links in the UI. Represents a light blue color with ARGB
        /// components (184, 210, 251) and hex value #B8D2FB.</remarks>
        public static readonly Color ActiveLinkColor = Color.FromArgb(0xB8, 0xD2, 0xFB);

        /// <summary>
        /// Color used for visited links.
        /// </summary>
        /// <remarks>Specified as RGB components (0xC5, 0x8A, 0xF9). Alpha is 255 (opaque).</remarks>
        public static readonly Color VisitedLinkColor = Color.FromArgb(0xC5, 0x8A, 0xF9);

        /// <summary>
        /// Colors for the few things that are painted by hand.
        /// </summary>
        public static Color Highlight => IsDarkMode ? Color.FromArgb(0x6B, 0x5B, 0x00) : Color.FromArgb(0xFF, 0xEB, 0x7A);

        /// <summary>
        /// Gets a muted color that adapts to the current theme for use with secondary UI elements and text.
        /// </summary>
        /// <remarks>Intended for secondary or disabled text and controls. Returns Color.FromArgb(0x9A,
        /// 0x9A, 0x9A) in dark mode and Color.FromArgb(0x60, 0x60, 0x60) in light mode.</remarks>
        public static Color Muted => IsDarkMode ? Color.FromArgb(0x9A, 0x9A, 0x9A) : Color.FromArgb(0x60, 0x60, 0x60);

        /// <summary>
        /// Works around controls that WinForms dark mode does not fully restyle. Applies to the control, its children, and any added later.
        /// </summary>
        public static void InitializeForm(Control root)
        {
            if (!IsDarkMode)
            {
                return;
            }

            if (root is TabPage tabPage)
            {
                tabPage.UseVisualStyleBackColor = false;
                tabPage.BackColor = SystemColors.Control;
            }
            else if (root is LinkLabel linkLabel)
            {
                linkLabel.LinkColor = LinkColor;
                linkLabel.ActiveLinkColor = ActiveLinkColor;
                linkLabel.VisitedLinkColor = VisitedLinkColor;
            }

            foreach (Control child in root.Controls)
            {
                InitializeForm(child);
            }

            root.ControlAdded -= Root_ControlAdded;
            root.ControlAdded += Root_ControlAdded;
        }

        private static void Root_ControlAdded(object? sender, ControlEventArgs e)
        {
            if (e.Control != null)
            {
                InitializeForm(e.Control);
            }
        }
    }
}
