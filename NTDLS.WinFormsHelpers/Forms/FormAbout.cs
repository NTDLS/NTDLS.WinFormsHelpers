using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace NTDLS.WinFormsHelpers.Forms
{
    /// <summary>
    /// Represents an About dialog that displays the application title, icon, logo, and discovered assembly versions;
    /// applies dark-mode fixups and optionally presents as a topmost taskbar window with an animated rotating logo.
    /// </summary>
    /// <remarks>Constructor configures AcceptButton and CancelButton, applies dark-mode fixups, sets
    /// StartPosition and TopMost according to showInTaskbar, and accepts optional title, icon, and logo.
    /// Renders a smoothly animated, sinusoidally rotated logo via a Timer and a custom Paint handler that scales the image to fit at the maximum rotation angle.
    /// On load it enumerates nearby assemblies to populate the version list.</remarks>
    public partial class FormAbout
        : Form
    {
        readonly Assembly assembly = Assembly.GetExecutingAssembly();
        private Image? _originalImage;
        private double _rotationPhase = 0;
        private float _rotationAngle = 0;
        private System.Windows.Forms.Timer? _rotationTimer;
        private readonly string _path;

        /// <summary>
        /// Initializes a new FormAbout instance with optional title, icon, and logo, and configures dialog buttons,
        /// theming, start position, and taskbar visibility.
        /// </summary>
        /// <remarks>Sets AcceptButton and CancelButton to cmdOk and applies dark-mode fixups. When
        /// showInTaskbar is true StartPosition is CenterScreen and TopMost is true; otherwise StartPosition is
        /// CenterParent and TopMost is false.</remarks>
        /// <param name="title">Window title to display; if null the existing title is retained.</param>
        /// <param name="icon">Window icon to display; if null the existing icon is retained.</param>
        /// <param name="logoImage">Logo image to display in the picture box; if null the default image is used. Size should be square and preferably at least 100x100 pixels.</param>
        /// <param name="showInTaskbar">True to show the form in the taskbar, center on the screen, and make it topmost; false to center it to the
        /// parent and not show in the taskbar.</param>
        /// <param name="path">The path to search for NetworkDLS assemblies.</param>
        public FormAbout(string? title = null, Icon? icon = null, Image? logoImage = null, bool showInTaskbar = true, string? path = null)
        {
            InitializeComponent();
            Theming.InitializeForm(this);

            CancelButton = cmdOk;
            AcceptButton = cmdOk;

            if (_path == null)
            {
                _path = Path.GetDirectoryName(assembly.Location) ?? string.Empty;
            }
            else
            {
                _path = path ?? string.Empty;
            }

            if (icon != null)
                SetIcon(icon);

            if (logoImage != null)
                SetLogoImage(logoImage);

            if (title != null)
                SetTitleText(title);

            if (showInTaskbar)
            {
                ShowInTaskbar = true;
                StartPosition = FormStartPosition.CenterScreen;
                TopMost = true;
            }
            else
            {
                ShowInTaskbar = false;
                StartPosition = FormStartPosition.CenterParent;
                TopMost = false;
            }
        }

        /// <summary>
        /// Sets the form title text in a thread safe manner.
        /// </summary>
        /// <param name="text"></param>
        public void SetTitleText(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(SetTitleText), text);
                return;
            }

            Text = text;
        }

        /// <summary>
        /// Sets the logo image in a thread safe manner.
        /// </summary>
        /// <param name="image"></param>
        public void SetLogoImage(Image image)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Image>(SetLogoImage), image);
                return;
            }

            if (image.Size.Height < pictureBoxLogo.Height || image.Size.Width < pictureBoxLogo.Width)
            {
                pictureBoxLogo.Height = image.Size.Height;
                pictureBoxLogo.Width = image.Size.Width;
            }
            else
            {
                _originalImage = image;
            }

            pictureBoxLogo.Top = ((listViewVersions.Height - listViewVersions.Top) / 2) - ((pictureBoxLogo.Height - listViewVersions.Top) / 2);
            pictureBoxLogo.Left = (listViewVersions.Left / 2) - (pictureBoxLogo.Width / 2);

            pictureBoxLogo.Image = image;
        }

        /// <summary>
        /// Sets the form icon in a thread safe manner.
        /// </summary>
        /// <param name="icon"></param>
        public void SetIcon(Icon icon)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Icon>(SetIcon), icon);
                return;
            }

            Icon = icon;
        }

        private void PictureBoxLogo_Paint(object? sender, PaintEventArgs e)
        {
            if (_originalImage == null) return;

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            float cx = pictureBoxLogo.Width / 2f;
            float cy = pictureBoxLogo.Height / 2f;

            g.TranslateTransform(cx, cy);
            g.RotateTransform(_rotationAngle);
            g.TranslateTransform(-cx, -cy);

            // Compute the largest scale at which the image still fits inside the box
            // at the maximum rotation angle. For a rectangle (w,h) rotated by θ, its
            // axis-aligned bounding box is (w·cosθ + h·sinθ) × (w·sinθ + h·cosθ).
            const double maxAngleDeg = 8.0;
            double maxAngleRad = maxAngleDeg * Math.PI / 180.0;
            double cosA = Math.Cos(maxAngleRad);
            double sinA = Math.Sin(maxAngleRad);
            float imgW = _originalImage.Width;
            float imgH = _originalImage.Height;
            float boxW = pictureBoxLogo.Width;
            float boxH = pictureBoxLogo.Height;
            float scale = (float)Math.Min(
                boxW / (imgW * cosA + imgH * sinA),
                boxH / (imgW * sinA + imgH * cosA));
            float drawW = imgW * scale;
            float drawH = imgH * scale;
            float x = (boxW - drawW) / 2f;
            float y = (boxH - drawH) / 2f;

            g.DrawImage(_originalImage, x, y, drawW, drawH);
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            AcceptButton = cmdOk;
            CancelButton = cmdOk;

            _originalImage = pictureBoxLogo.Image;
            pictureBoxLogo.Image = null;
            pictureBoxLogo.Paint += PictureBoxLogo_Paint;

            _rotationTimer = new System.Windows.Forms.Timer { Interval = 16 };
            _rotationTimer.Tick += (s, _) =>
            {
                _rotationPhase += 0.05;
                _rotationAngle = (float)(Math.Sin(_rotationPhase) * 8.0);
                pictureBoxLogo.Invalidate();
            };
            _rotationTimer.Start();

            FormClosed += (s, _) => { _rotationTimer.Stop(); _rotationTimer.Dispose(); };

            if (assembly == null || assembly.Location == null || string.IsNullOrEmpty(_path))
            {
                return;
            }

            var files = Directory.EnumerateFiles(_path, "*.dll", SearchOption.AllDirectories).ToList();
            files.AddRange(Directory.EnumerateFiles(_path, "*.exe", SearchOption.AllDirectories));

            HashSet<string> seenAssemblies = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);

                if (!seenAssemblies.Contains(fileName))
                {
                    seenAssemblies.Add(fileName);
                    AddAssembly(file);
                }
            }

            listViewVersions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewVersions.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void AddAssembly(string appPath)
        {
            try
            {
                var componentAssembly = AssemblyName.GetAssemblyName(appPath);
                var versionInfo = FileVersionInfo.GetVersionInfo(appPath);
                var companyName = versionInfo.CompanyName;

                if (componentAssembly.Version != null && companyName?.ToLower()?.Contains("networkdls") == true)
                {
                    var verStr = string.Join('.', componentAssembly.Version.ToString().Split('.').Take(3));
                    listViewVersions.Items.Add(new ListViewItem([componentAssembly.Name ?? "", verStr]));
                }
            }
            catch
            {
            }
        }

        private void LinkWebsiteNtdls_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "http://www.NetworkDLS.com",
                    UseShellExecute = true
                });
            }
            catch
            {
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
