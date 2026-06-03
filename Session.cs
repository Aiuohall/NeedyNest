using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeedyNest
{
    public static class Session
    {
        public static string LoggedInUsername { get; set; }
        public static string LoggedInRole { get; set; }

        public static void Clear()
        {
            LoggedInUsername = null;
            LoggedInRole = null;
        }
    }

}
