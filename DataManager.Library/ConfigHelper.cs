using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Library
{
        public class ConfigHelper
        {
            //TODO Move this from Config to the API
            public static decimal GetTaxRate()
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

