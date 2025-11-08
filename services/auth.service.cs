using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialManager.services
{
  public static class AuthSessionService
  {
    private static User? _currentUser;

    public static User? CurrentUser
    {
      get => _currentUser;
      set => _currentUser = value;
    }

    public static bool IsLoggedIn => CurrentUser != null;

    public static void Login(User user)
    {
      CurrentUser = user;
    }

    public static void Logout()
    {
      CurrentUser = null!;
    }

    public static void RefreshCurrentUser()
    {
      if (_currentUser != null)
      {
        try
        {
          var userService = new UserService();
          var refreshedUser = userService.GetUserById(_currentUser.UserID);
          if (refreshedUser != null)
          {
            _currentUser = refreshedUser;
          }
        }
        catch (Exception ex)
        {
          System.Diagnostics.Debug.WriteLine($"Error refreshing current user: {ex.Message}");
        }
      }
    }

    public static void UpdateCurrentUser(User updatedUser)
    {
      if (_currentUser != null && updatedUser != null && _currentUser.UserID == updatedUser.UserID)
      {
        _currentUser = updatedUser;
      }
    }
  }
}
