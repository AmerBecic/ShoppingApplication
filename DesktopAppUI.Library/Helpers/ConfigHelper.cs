using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAppUI.Library.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        //TODO Move this from Config to the API
        public decimal GetTaxRate()
        {
            string taxRateText = ConfigurationManager.AppSettings["taxRate"];

            bool isValidTaxRate = Decimal.TryParse(taxRateText, out decimal output);

            if (isValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("The tax rate is not set up properly");
            }

            return output / 100;
        }
    }
}

