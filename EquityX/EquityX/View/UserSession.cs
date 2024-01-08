using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityX.View
{
    public static class UserSession
    {
        public static string CurrentUserId { get; private set; }
        public static string CurrentUserEmail { get; private set; }
        public static bool IsUserLoggedIn => !string.IsNullOrEmpty(CurrentUserId);

        public static void SetCurrentUser(string userId, string userEmail)
        {
            CurrentUserId = userId;
            CurrentUserEmail = userEmail;
        }

        public static void ClearUserSession()
        {
            CurrentUserId = null;
            CurrentUserEmail = null;
        }
    }
}
