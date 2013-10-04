using System;
using System.ComponentModel;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomParameters
{
    public enum DaySetting
    {
        Today = 0,
        Yesterday = 1,
        Tomorrow = 2,
    }

    public class DayParameter : Parameter
    {
        [DefaultValue(DaySetting.Today)]
        public DaySetting DaySetting
        {
            get
            {
                object o = ViewState["DaySetting"];
                if (o != null)
                {
                    return (DaySetting)o;
                }
                return DaySetting.Today;
            }
            set
            {
                ViewState["DaySetting"] = value;
            }
        }

        public DayParameter()
        {
        }

        protected DayParameter(DayParameter original) : base(original)
        {
            DaySetting = original.DaySetting;
        }

        protected override Parameter Clone()
        {
            return new DayParameter(this);
        }

        protected override object Evaluate(HttpContext context, Control control)
        {
            switch (DaySetting)
            {
                case DaySetting.Today:
                    return DateTime.Now;
                case DaySetting.Yesterday:
                    return DateTime.Now.AddDays(-1);
                case DaySetting.Tomorrow:
                    return DateTime.Now.AddDays(1);
            }
            
            throw new InvalidOperationException("Unknown day setting.");
        }
    }

    public class CurrentUserParameter : Parameter
    {
        public CurrentUserParameter()
        {
        }

        protected CurrentUserParameter(CurrentUserParameter original) : base(original)
        {            
        }

        protected override Parameter Clone()
        {
            return new CurrentUserParameter(this);
        }

        protected override object Evaluate(HttpContext context, Control control)
        {
            return GetCurrentUser(context);
        }
        
        /// <summary>
        /// Requires that usernames are build in first.last format, don't use otherwise.
        /// </summary>
        /// <param name="inContext"></param>
        /// <returns></returns>
        private string GetCurrentUser(HttpContext inContext)
        {
            string tempUser = inContext.User.Identity.Name;
            tempUser = tempUser.Replace(".", " ");

            TextInfo UsaTextInfo = new CultureInfo("en-US", false).TextInfo;
            tempUser = UsaTextInfo.ToTitleCase(tempUser);

            return tempUser;
        }
    }


}