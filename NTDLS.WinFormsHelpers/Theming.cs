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
        /// Background for a row or cell that reports a problem (disconnected, failed...). Use <see cref="StatusText"/> for its text.
        /// </summary>
        public static Color StatusErrorBack => IsDarkMode ? Color.FromArgb(110, 40, 40) : Color.FromArgb(255, 200, 200);

        /// <summary>
        /// Background for a row or cell that reports something that needs attention (connecting, stopped...).
        /// Use <see cref="StatusText"/> for its text.
        /// </summary>
        public static Color StatusWarningBack => IsDarkMode ? Color.FromArgb(125, 90, 0) : Color.FromArgb(255, 200, 0);

        /// <summary>
        /// Background for a row or cell that reports that all is well (connected, running...). Use <see cref="StatusText"/> for its text.
        /// </summary>
        public static Color StatusGoodBack => IsDarkMode ? Color.FromArgb(35, 95, 45) : Color.FromArgb(200, 255, 200);

        /// <summary>
        /// The text color for text on <see cref="StatusErrorBack"/>, <see cref="StatusWarningBack"/> and <see cref="StatusGoodBack"/>.
        /// </summary>
        public static Color StatusText => IsDarkMode ? Color.White : Color.Black;

        /// <summary>
        /// Works around controls that WinForms dark mode does not fully restyle (link labels, tab pages, combo boxes, the headers and link columns of
        /// data grids). Applies to the control, its children, and any added later.
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
            else if (root is Form form)
            {
                form.Shown -= Form_Shown;
                form.Shown += Form_Shown;
            }
            else if (root is DataGridView grid)
            {
                InitializeGrid(grid);
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

        /// <summary>
        /// The color of the header cells of a grid in dark mode.
        /// </summary>
        private static readonly Color GridHeaderBackColor = Color.FromArgb(0x3A, 0x3A, 0x3A);

        /// <summary>
        /// Gives a DataGridView the colors it does not get from dark mode by itself: its header cells stay white, and the links in
        /// its link columns keep the default dark blue, which cannot be read on a dark background. Columns that are added later are
        /// handled as they are added.
        /// </summary>
        private static void InitializeGrid(DataGridView grid)
        {
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = GridHeaderBackColor;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = GridHeaderBackColor;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            grid.RowHeadersDefaultCellStyle.BackColor = GridHeaderBackColor;
            grid.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.RowHeadersDefaultCellStyle.SelectionBackColor = GridHeaderBackColor;
            grid.GridColor = Color.FromArgb(0x55, 0x55, 0x55);

            foreach (DataGridViewColumn column in grid.Columns)
            {
                InitializeGridColumn(column);
            }

            grid.ColumnAdded -= Grid_ColumnAdded;
            grid.ColumnAdded += Grid_ColumnAdded;
        }

        private static void Grid_ColumnAdded(object? sender, DataGridViewColumnEventArgs e)
            => InitializeGridColumn(e.Column);

        private static void InitializeGridColumn(DataGridViewColumn column)
        {
            if (column is DataGridViewLinkColumn linkColumn)
            {
                linkColumn.LinkColor = LinkColor;
                linkColumn.ActiveLinkColor = ActiveLinkColor;
                linkColumn.VisitedLinkColor = VisitedLinkColor;
            }
        }


        private static readonly System.Reflection.MethodInfo? _recreateHandle =
            typeof(Control).GetMethod("RecreateHandle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        private static void Form_Shown(object? sender, EventArgs e)
        {
            if (sender is Form form)
            {
                RecreateComboBoxes(form);
            }
        }

        /// <summary>
        /// WinForms gives a combo box the dark theme when its handle is created, but not when that happens while the form itself is being
        /// created, which is when the combo boxes on the page that is showing first get theirs: those stay white. Recreating their handles
        /// once the form is shown gives them the dark theme. Combo boxes that get their handle later (on the other tab pages) are
        /// fine already, so only the ones that have a handle at this point are touched.
        /// </summary>
        private static void RecreateComboBoxes(Control root)
        {
            if (root is ComboBox combo && combo.IsHandleCreated)
            {
                try
                {
                    _recreateHandle?.Invoke(combo, null);
                }
                catch
                {
                    //The combo box stays as it is.
                }
            }

            foreach (Control child in root.Controls)
            {
                RecreateComboBoxes(child);
            }
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
