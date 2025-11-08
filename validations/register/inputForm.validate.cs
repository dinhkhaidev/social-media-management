using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialManager.validations.register
{
  public class inputValidateEvent
  {
    public static void ValidateUsername(object? sender, EventArgs e)
    {
      TextBox? txt = sender as TextBox;
      if (txt != null && txt.Text.Length > 0 && txt.Text.Length < 3)
      {
        txt.BackColor = Color.FromArgb(255, 240, 240);
      }
      else if (txt != null)
      {
        txt.BackColor = Color.FromArgb(248, 249, 250);
      }
    }

    public static void ValidateEmail(object? sender, EventArgs e)
    {
      TextBox? txt = sender as TextBox;
      if (txt != null && txt.Text.Length > 0 && !IsValidEmail(txt.Text))
      {
        txt.BackColor = Color.FromArgb(255, 240, 240);
      }
      else if (txt != null)
      {
        txt.BackColor = Color.FromArgb(248, 249, 250);
      }
    }

    public static void ValidatePassword(object? sender, EventArgs e)
    {
      TextBox? txt = sender as TextBox;
      if (txt != null && txt.Text.Length > 0 && txt.Text.Length < 6)
      {
        txt.BackColor = Color.FromArgb(255, 240, 240);
      }
      else if (txt != null)
      {
        txt.BackColor = Color.FromArgb(248, 249, 250);
      }
    }
    public static bool IsValidEmail(string email)
    {
      try
      {
        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase);
      }
      catch
      {
        return false;
      }
    }
    public static void ValidateConfirmPassword(object? sender, EventArgs e)
    {
      TextBox? txtConfirmPassword = sender as TextBox;
      TextBox? txtPassword = txtConfirmPassword?.Tag as TextBox;

      if (txtConfirmPassword != null && txtPassword != null)
      {
        if (txtConfirmPassword.Text.Length > 0 && txtConfirmPassword.Text != txtPassword.Text)
        {
          txtConfirmPassword.BackColor = Color.FromArgb(255, 240, 240);
        }
        else
        {
          txtConfirmPassword.BackColor = Color.FromArgb(248, 249, 250);
        }
      }
    }

  }
  //public class
}
