//using SocialManager.frm;
//using SocialManager.services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SocialManager.middlewares
//{
//    public class ProtectFormMiddleware : Form
//    {
//        protected override void OnLoad(EventArgs e)
//        {
//            base.OnLoad(e);
//            // Check if the user is authenticated
//            if (!AuthSessionService.IsLoggedIn)
//            {
//                MessageBox.Show("You must log in to continue.");
//                var loginForm = new frmLogin();
//                loginForm.Show();
//                this.Close();
//            }
//        }
//    }
//}
using SocialManager.frm;
using SocialManager.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialManager.middlewares
{
    public class ProtectFormMiddleware : Form
    {
        private static bool _isRedirecting = false; // Prevent multiple redirects

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Check if the user is authenticated
            if (!AuthSessionService.IsLoggedIn && !_isRedirecting)
            {
                _isRedirecting = true;

                MessageBox.Show("Session expired. Please log in again.", "Authentication Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Create login form
                var loginForm = new frmLogin();

                // Hide current form
                this.Hide();

                // Show login form and wait for result
                var result = loginForm.ShowDialog();

                // Reset redirect flag
                _isRedirecting = false;

                // If login was not successful, close this form
                if (!AuthSessionService.IsLoggedIn)
                {
                    this.Close();
                }
                else
                {
                    // If login successful, show this form again
                    this.Show();
                }
            }
        }
    }
}
