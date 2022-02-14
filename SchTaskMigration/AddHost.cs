using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchTaskMigration
{
    public partial class AddHost : Form
    {
        public string HostAddress { get => tbxHostAddress.Text; }

        public string Domain { get => tbxUserDomain.Text; }

        public string Username { get => tbxUsername.Text; }

        public string Password { get => tbxPassword.Text; }

        public AddHost()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            tbxHostAddress.Text = "";
            tbxUserDomain.Text = "";
            tbxUsername.Text = "";
            tbxPassword.Text = "";
        }
    }
}
