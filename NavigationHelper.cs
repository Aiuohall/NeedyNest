using System.Windows.Forms;

namespace NeedyNest
{
    /// <summary>
    /// Centralizes "go back to my dashboard" navigation. The destination is based
    /// on the role of whoever is logged in (Session.LoggedInRole) — NOT on a fresh
    /// database lookup that used to default to "moderator" when the DB was offline
    /// or ambiguous. This guarantees an admin always returns to the admin dashboard,
    /// even after working in a shared screen (courses, distribution, etc.).
    /// </summary>
    internal static class NavigationHelper
    {
        public static Form CreateDashboardForLoggedInUser(string uName)
        {
            switch (Session.LoggedInRole)
            {
                case "Admin":       return new admindashboardform(uName);
                case "Moderator":   return new moderatordash(uName);
                case "User":        return new userdashboard(uName);
                case "Distributor": return new form_distributor(uName);
                default:            return new Login();
            }
        }

        public static void GoToDashboard(Form current, string uName)
        {
            CreateDashboardForLoggedInUser(uName).Show();
            current.Hide();
        }
    }
}
