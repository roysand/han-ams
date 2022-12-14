using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public static class TaxToolBox
    {
        public static decimal CalculateTax(decimal value)
        {
            return value * 25 / 100;
        }
    }
}
