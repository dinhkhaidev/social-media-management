using SocialManager.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialManager.frm
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
            lblUsername.Text = AuthSessionService.CurrentUser.UserName;
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void btnNewPost_Click(object sender, EventArgs e)
        {
            frmPost frmPost = new frmPost();
            frmPost.ShowDialog();
        }
    }
}
