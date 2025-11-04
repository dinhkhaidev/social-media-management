using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialManager.constants
{
  public class UserConstant
  {
    public static string GetRoleText(int role)
    {
      return role switch
      {
        0 => "User",
        1 => "Admin",
        2 => "Moderator",
        _ => "Unknown"
      };
    }

    public static string GetStatusText(int status)
    {
      return status switch
      {
        1 => "Active",
        0 => "Inactive",
        -1 => "Banned",
        _ => "Unknown"
      };
    }
  }
}
