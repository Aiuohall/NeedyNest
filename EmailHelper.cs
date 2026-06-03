using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace NeedyNest
{
    /// <summary>
    /// Best-effort SMTP email sender. Reads settings from App.config &lt;appSettings&gt;.
    /// If SMTP isn't configured (SmtpHost blank) or the recipient has no address,
    /// <see cref="Send"/> simply does nothing — it never throws or blocks a workflow.
    ///
    /// Configure in App.config:
    ///   SmtpHost, SmtpPort, SmtpUser, SmtpPassword, SmtpFrom, SmtpSsl
    /// </summary>
    internal static class EmailHelper
    {
        public static bool IsConfigured =>
            !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SmtpHost"]);

        public static void Send(string toEmail, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(toEmail) || !IsConfigured) return;

            try
            {
                string host = ConfigurationManager.AppSettings["SmtpHost"];
                int    port = int.TryParse(ConfigurationManager.AppSettings["SmtpPort"], out var p) ? p : 587;
                string user = ConfigurationManager.AppSettings["SmtpUser"];
                string pass = ConfigurationManager.AppSettings["SmtpPassword"];
                string from = string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SmtpFrom"])
                                ? user : ConfigurationManager.AppSettings["SmtpFrom"];
                bool   ssl  = !bool.TryParse(ConfigurationManager.AppSettings["SmtpSsl"], out var s) || s;

                using (var client = new SmtpClient(host, port)
                {
                    EnableSsl = ssl,
                    Credentials = new NetworkCredential(user, pass)
                })
                using (var msg = new MailMessage(from, toEmail, subject, body))
                {
                    client.Send(msg);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "EmailHelper.Send"); // never surface email failures to the user
            }
        }
    }
}
