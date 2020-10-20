using log4net;
using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Forms;
using ScenarioContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ScenarioApp.Controls
{
    public partial class BrowserProfilesControl : UserControl, IBrowserProfilesControl
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BrowserProfilesControl));
        private readonly IBrowserProfileService _browserProfileService;
        private readonly IBrowserProfileControl _browserProfileControl;
        private readonly BindingSource bindingSource;
        private readonly IEnumerable<IBrowserProfile> browserProfiles;
        private string _folder;

        public string Folder
        {
            get => _folder;
            set
            {
                _folder = value;
                label1.Text = _folder;
            }
        }

        public BrowserProfilesControl(IBrowserProfileService browserProfileService, IBrowserProfileControl browserProfileControl)
        {
            _browserProfileService = browserProfileService;
            _browserProfileControl = browserProfileControl;

            InitializeComponent();
            btnNewBrowserProfile.Click += BtnNewBrowserProfile_Click;
            btnEditBrowserProfile.Click += BtnEditBrowserProfile_Click;
            btnDeleteBrowserProfile.Click += BtnDeleteBrowserProfile_Click;
            btnBrowserProfileStart.Click += BtnBrowserProfileStart_Click;

            label1.Text = nameof(BrowserProfilesControl);

            browserProfiles = _browserProfileService.GetBrowserProfiles();
            bindingSource = new BindingSource
            {
                DataSource = browserProfiles
            };

            dataGridView1.DataSource = bindingSource;
            bindingSource.CurrentChanged += BindingSource_CurrentChanged;
            BindingSource_CurrentChanged(bindingSource, null);
        }



        private void BtnNewBrowserProfile_Click(object sender, EventArgs e)
        {
            IBrowserProfile browserProfile = _browserProfileService.GetNew();
            var childForm = new ChildForm();
            _browserProfileControl.SetData(browserProfile);
            childForm.AddControlToWorkArea((Control)_browserProfileControl);
            if (childForm.ShowDialog() == DialogResult.OK)
            {
                _browserProfileService.Add(browserProfile);
                _browserProfileService.SaveProfiles();
                bindingSource.ResetBindings(false);
                //datagridview1.Update();
                //datagridview1.Refresh();
            }
        }

        private void BtnEditBrowserProfile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Folder)) return;
            var browserProfile = browserProfiles.FirstOrDefault(z => z.Folder.Equals(Folder));
            if (browserProfile != null)
            {
                var childForm = new ChildForm();
                _browserProfileControl.SetData(browserProfile);
                childForm.AddControlToWorkArea((Control)_browserProfileControl);
                if (childForm.ShowDialog() == DialogResult.OK)
                {
                    _browserProfileService.SaveProfiles();
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void BtnDeleteBrowserProfile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Folder)) return;
            var browserProfile = browserProfiles.FirstOrDefault(z => z.Folder.Equals(Folder));
            if (browserProfile == null) return;
            var confirmResult = MessageBox.Show($"Вы уверены, что хотите удалить профиль {browserProfile.Name} ??", "Подтвердите удаление!!", MessageBoxButtons.YesNo);
            if (confirmResult != DialogResult.Yes) return;
            _browserProfileService.RemoveByFolder(browserProfile.Folder);
            _browserProfileService.SaveProfiles();
            bindingSource.ResetBindings(false);
        }

        private async void BtnBrowserProfileStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Folder)) return;
            var browserProfile = browserProfiles.FirstOrDefault(z => z.Folder.Equals(Folder));
            try
            {
                if (browserProfile != null) await _browserProfileService.StartProfile(browserProfile.Folder);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception}");
                Log.Error($"{exception}");
            }
            
        }

        private void BindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingSource.Current == null) return;
            IBrowserProfile browserProfile = (IBrowserProfile)bindingSource.Current;
            Folder = browserProfile.Folder;
        }

    }
}
