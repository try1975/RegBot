using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScenarioApp.Controls.Interfaces;

namespace ScenarioApp.Controls
{
    public partial class CreateVkGroupControl : UserControl, ICreateVkGroupControl
    {
        public CreateVkGroupControl()
        {
            InitializeComponent();
        }
    }
}
