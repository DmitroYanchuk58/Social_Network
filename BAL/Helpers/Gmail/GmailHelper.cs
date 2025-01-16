using BAL.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Helpers.Gmail
{
    public class GmailHelper : IGmailHelper
    {
        public bool IsGmail(string gmail)
        {
            return gmail.EndsWith("@gmail.com");
        }
    }
}
