using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Forms;
using ScenarioApp.Ninject;
using System;
using System.Windows.Forms;

namespace ScenarioApp.Controls
{
    public partial class ScenarioControl : UserControl, IScenarioControl
    {
        private IGoogleSearchControl _googleSearchControl;
        private ICollectVkWallControl _collectVkWallControl;
        private IYandexSearchControl _yandexSearchControl;
        private IWhoisControl _whoisControl;
        private IPostVkControl _postVkControl;
        private ICheckVkAccountControl _checkVkAccountControl;
        private ICheckVkCredentialControl _checkVkCredentialControl;
        private IRegBotControl _regBotControl;
        private IEmailCheckControl _emailCheckControl;

        public ScenarioControl()
        {
            InitializeComponent();

            btnRegBotControl.Click += btnRegBotControl_Click;
            btnGoogleSearchControl.Click += BtnGoogleSearchControl_Click;
            btnYandexSearchControl.Click += BtnYandexSearchControl_Click;
            btnWhoisControl.Click += BtnWhoisControl_Click;

            btnCollectVkWallControl.Click += BtnCollectVkWallControl_Click;
            btnPostVkControl.Click += BtnPostVkControl_Click;
            btnCheckVkAccount.Click+=BtnCheckVkAccount_Click;
            btnCheckVkCredential.Click += BtnCheckVkCredential_Click;

            btnEmailCheckControl.Click += BtnEmailCheckControl_Click;
        }


        private void AddControlToWorkArea(Control control, bool ctrlPressed = false)
        {
            if (!ctrlPressed)
            {
                control.Dock = DockStyle.Fill;
                pnlWorkArea.Controls.Clear();
                pnlWorkArea.Controls.Add(control);
                return;
            }
            var childForm = new ChildForm { Text = control.Name };
            childForm.AddControlToWorkArea(control);
            childForm.Show();
        }

        private void BtnEmailCheckControl_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_emailCheckControl == null) _emailCheckControl = CompositionRoot.Resolve<IEmailCheckControl>();
                AddControlToWorkArea((Control)_emailCheckControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<IEmailCheckControl>(), true);
        }

        
        private void BtnCollectVkWallControl_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_collectVkWallControl == null) _collectVkWallControl = CompositionRoot.Resolve<ICollectVkWallControl>();
                AddControlToWorkArea((Control)_collectVkWallControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<ICollectVkWallControl>(), true);
        }

        private void btnRegBotControl_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_regBotControl == null) _regBotControl = CompositionRoot.Resolve<IRegBotControl>();
                AddControlToWorkArea((Control)_regBotControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<IRegBotControl>(), true);
        }

        private void BtnGoogleSearchControl_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_googleSearchControl == null) _googleSearchControl = CompositionRoot.Resolve<IGoogleSearchControl>();
                AddControlToWorkArea((Control)_googleSearchControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<IGoogleSearchControl>(), true);
        }

        private void BtnYandexSearchControl_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_yandexSearchControl == null) _yandexSearchControl = CompositionRoot.Resolve<IYandexSearchControl>();
                AddControlToWorkArea((Control)_yandexSearchControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<IYandexSearchControl>(), true);
        }

        private void BtnWhoisControl_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_whoisControl == null) _whoisControl = CompositionRoot.Resolve<IWhoisControl>();
                AddControlToWorkArea((Control)_whoisControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<IWhoisControl>(), true);
        }

        private void BtnPostVkControl_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_postVkControl == null) _postVkControl = CompositionRoot.Resolve<IPostVkControl>();
                AddControlToWorkArea((Control)_postVkControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<IPostVkControl>(), true);
        }

        private void BtnCheckVkAccount_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_checkVkAccountControl == null) _checkVkAccountControl = CompositionRoot.Resolve<ICheckVkAccountControl>();
                AddControlToWorkArea((Control)_checkVkAccountControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<ICheckVkAccountControl>(), true);
        }

        private void BtnCheckVkCredential_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_checkVkCredentialControl == null) _checkVkCredentialControl = CompositionRoot.Resolve<ICheckVkCredentialControl>();
                AddControlToWorkArea((Control)_checkVkCredentialControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<ICheckVkCredentialControl>(), true);
        }
    }
}
