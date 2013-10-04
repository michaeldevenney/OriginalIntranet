using System;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.SessionState;

namespace Shared {
    public static class Utils {
        /// <summary>
        ///     Returns True if the passed character is between A-M
        /// </summary>
        /// <param name="inStartingChar">First character of the string being tested</param>
        /// <returns>true if passed character is between A and M</returns>
        public static bool BetweenAandM(string inStartingChar)
        {
            char[] firstChar = inStartingChar.ToLower().ToCharArray();
            char nChar = 'n';
            char testChar = firstChar[0];


            return (testChar < nChar);
        }

        /// <summary>
        ///     This method is used on our external sites to format the first.last type usernames to a readable format
        /// </summary>
        /// <param name="inUserName"></param>
        /// <returns></returns>
        public static string GetFormattedUserNameExternal(string inUserName)
        {
            string tempUser = inUserName;
            tempUser = tempUser.Replace(".", " ");
            TextInfo UsaTextInfo = new CultureInfo("en-US", false).TextInfo;
            tempUser = UsaTextInfo.ToTitleCase(tempUser);
            return tempUser;
        }

        /// <summary>
        ///     This method is used on our internal sites to format the domain\first.last type usernames to a readable format
        /// </summary>
        /// <param name="inUserName"></param>
        /// <returns></returns>
        public static string GetFormattedUserNameInternal(string inUserName)
        {
            string tempUser = inUserName;
            tempUser = tempUser.Substring(tempUser.LastIndexOf("\\") + 1);
            tempUser = tempUser.Replace(".", " ");

            TextInfo UsaTextInfo = new CultureInfo("en-US", false).TextInfo;
            tempUser = UsaTextInfo.ToTitleCase(tempUser);

            return tempUser;
        }

        public static decimal NullSafeDecimal(decimal? inbound)
        {
            return inbound == null ? 0m : decimal.Parse(inbound.ToString());
        }

        public static bool? NullSafeBool(string inbound)
        {
            switch (inbound)
            {
                case "0":
                    return false;
                case "1":
                    return true;
                default:
                    return null;
            }
        }

        public static string NullableBoolToRadioButton(bool? inbound)
        {
            switch (inbound)
            {
                case true:
                    return "1";
                case false:
                    return "0";
                default:
                    return null;
            }
        }

        public static int? ParseNullableInt(string s)
        {
            if (!String.IsNullOrEmpty(s)) {
                int retVal;
                if (int.TryParse(s, out retVal)) {
                    return retVal;
                }
            }
            return null;
        }

        public static DateTime? ParseNullableDateTime(string s)
        {
            if (!String.IsNullOrEmpty(s))
            {
                DateTime retVal;
                if (DateTime.TryParse(s, out retVal))
                {
                    return retVal;
                }
            }
            return null;
        }

        public static int NullSafeInt(int? inbound)
        {
            return inbound == null ? 0 : int.Parse(inbound.ToString());
        }
    }

    public static class Paths {
        public static string Exec = ConfigurationManager.AppSettings["ExecFolderPath"];
        public static string Projects = ConfigurationManager.AppSettings["ProjectsFolderPath"];
        public static string Prospects = ConfigurationManager.AppSettings["PropsectsFolderPath"];
        public static string ProspectsFolderTemplate = ConfigurationManager.AppSettings["ProspectFolderTemplate"];
    }

    public class SessionVar {
        private static HttpSessionState Session
        {
            get
            {
                if (HttpContext.Current == null) {
                    throw new ApplicationException("No Http Context, No Session to Get!");
                }

                return HttpContext.Current.Session;
            }
        }

        public static T Get<T>(string key)
        {
            if (Session[key] == null) {
                return default(T);
            }
            else {
                return (T) Session[key];
            }
        }

        public static void Set<T>(string key, T value)
        {
            Session[key] = value;
        }
    }
}