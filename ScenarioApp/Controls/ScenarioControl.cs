﻿using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Forms;
using ScenarioApp.Ninject;
using System;
using System.Windows.Forms;

namespace ScenarioApp.Controls
{
    public partial class ScenarioControl : UserControl, IScenarioControl
    {
        #region private
        private IGoogleSearchControl _googleSearchControl;
        private ICollectVkWallControl _collectVkWallControl;
        private IYandexSearchControl _yandexSearchControl;
        private IForumSearchControl _forumSearchControl;
        private IWhoisControl _whoisControl;
        private IPostVkControl _postVkControl;
        private ICheckVkAccountControl _checkVkAccountControl;
        private ICheckVkCredentialControl _checkVkCredentialControl;
        private IRegBotControl _regBotControl;
        private IEmailCheckControl _emailCheckControl;
        private ICheckFbAccountControl _checkFbAccountControl;
        private ICheckFbCredentialControl _checkFbCredentialControl;
        private ICreateVkGroupControl _createVkGroupControl;
        private ISelectPersonControl _selectPersonControl;
        private ISendMailControl _sendMailControl;
        private IGenerateAccountDataControl _generateAccountDataControl;
        private ICaptchaControl _captchaControl;
        #endregion

        public ScenarioControl()
        {
            InitializeComponent();

            btnRegBotControl.Click += BtnRegBotControl_Click;
            btnGoogleSearchControl.Click += BtnGoogleSearchControl_Click;
            btnYandexSearchControl.Click += BtnYandexSearchControl_Click;
            btnForumSearchControl.Click += BtnForumSearchControl_Click;
            btnWhoisControl.Click += BtnWhoisControl_Click;

            btnCollectVkWallControl.Click += BtnCollectVkWallControl_Click;
            btnPostVkControl.Click += BtnPostVkControl_Click;
            btnCheckVkAccount.Click += BtnCheckVkAccount_Click;
            btnCheckVkCredential.Click += BtnCheckVkCredential_Click;

            btnEmailCheckControl.Click += BtnEmailCheckControl_Click;
            btnCheckFbAccountControl.Click += BtnCheckFbAccountControl_Click;
            btnCheckFbCredentialControl.Click += BtnCheckFbCredentialControl_Click;
            btnCreateVkGroupControl.Click += BtnCreateVkGroupControl_Click;
            btnSelectPersonControl.Click += BtnSelectPersonControl_Click;
            btnSendMailControl.Click += BtnSendMailControl_Click;
            btnGenerateAccountDataControl.Click += BtnGenerateAccountDataControl_Click;
            btnCaptchaControl.Click += BtnCaptchaControl_Click;

            Load += ScenarioControl_Load;
        }

        private void BtnSendMailControl_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_sendMailControl == null) _sendMailControl = CompositionRoot.Resolve<ISendMailControl>();
                AddControlToWorkArea((Control)_sendMailControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<ISendMailControl>(), true);
        }

        private void ScenarioControl_Load(object sender, EventArgs e)
        {
            BtnSelectPersonControl_Click(btnSelectPersonControl, EventArgs.Empty);
        }

        private void BtnSelectPersonControl_Click(object sender, EventArgs e)
        {
            if (_selectPersonControl == null) _selectPersonControl = CompositionRoot.Resolve<ISelectPersonControl>();
            AddControlToWorkArea((Control)_selectPersonControl, false);
        }

        private void BtnCreateVkGroupControl_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_createVkGroupControl == null) _createVkGroupControl = CompositionRoot.Resolve<ICreateVkGroupControl>();
                AddControlToWorkArea((Control)_createVkGroupControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<ICreateVkGroupControl>(), true);
        }

        private void BtnCheckFbCredentialControl_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_checkFbCredentialControl == null) _checkFbCredentialControl = CompositionRoot.Resolve<ICheckFbCredentialControl>();
                AddControlToWorkArea((Control)_checkFbCredentialControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<ICheckFbCredentialControl>(), true);
        }

        private void BtnCheckFbAccountControl_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_checkFbAccountControl == null) _checkFbAccountControl = CompositionRoot.Resolve<ICheckFbAccountControl>();
                AddControlToWorkArea((Control)_checkFbAccountControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<ICheckFbAccountControl>(), true);
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

        private void BtnRegBotControl_Click(object sender, EventArgs e)
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

        private void BtnForumSearchControl_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_forumSearchControl == null) _forumSearchControl = CompositionRoot.Resolve<IForumSearchControl>();
                AddControlToWorkArea((Control)_forumSearchControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<IForumSearchControl>(), true);
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

        private void BtnGenerateAccountDataControl_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_generateAccountDataControl == null) _generateAccountDataControl = CompositionRoot.Resolve<IGenerateAccountDataControl>();
                AddControlToWorkArea((Control)_generateAccountDataControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<IGenerateAccountDataControl>(), true);
        }

        private void BtnCaptchaControl_Click(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                if (_captchaControl == null) _captchaControl = CompositionRoot.Resolve<ICaptchaControl>();
                AddControlToWorkArea((Control)_captchaControl, false);
                return;
            }
            AddControlToWorkArea((Control)CompositionRoot.Resolve<ICaptchaControl>(), true);
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
    }
}
