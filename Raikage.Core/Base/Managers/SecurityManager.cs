using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaikageFramework.Base.Managers
{
    public static class SecurityManager
    {
        public static Type LoginType;
        public static bool IsAuthenticated { get; set; }
    }
}
