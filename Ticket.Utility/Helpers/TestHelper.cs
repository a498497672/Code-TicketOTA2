using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Utility.Helpers
{
    public static class TestHelper
    {
        private static bool isAction = true;
        public static bool IsAction
        {
            get { return isAction; }
            set { isAction = value; }
        }
    }
}
