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
        public static async Task SaveSessionAsync()
        {
            await SecureStorage.SetAsync("userId", CurrentUserId);
            await SecureStorage.SetAsync("userEmail", CurrentUserEmail);
        }

        // Method to load session information
        public static async Task LoadSessionAsync()
        {
            CurrentUserId = await SecureStorage.GetAsync("userId");
            CurrentUserEmail = await SecureStorage.GetAsync("userEmail");
        }
    }
}
