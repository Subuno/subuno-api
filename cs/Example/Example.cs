using System;
using System.Collections.Generic;
using Subuno;

namespace Example
{
    class Example
    {
        static void Main(string[] args)
        {
            var data = new Dictionary<string, string>();

            data.Add("t_id", "7d3n89wn");
            data.Add("ip_addr", "24.24.24.24");
            data.Add("customer_name", "John Doe");
            data.Add("phone", "212-456-7890");
            data.Add("email", "john.doe@domain.com");
            data.Add("company", "Doe LLC");
            data.Add("price", "50.0");
            data.Add("bin", "480128");

            data.Add("bill_street1", "12 East 71th St");
            data.Add("bill_street2", "#12");
            data.Add("bill_city", "New York");
            data.Add("bill_state", "NY");
            data.Add("bill_country", "US");
            data.Add("bill_zip", "10021");

            data.Add("ship_street1", "12 East 71th St");
            data.Add("ship_street2", "#12");
            data.Add("ship_city", "New York");
            data.Add("ship_state", "NY");
            data.Add("ship_country", "US");
            data.Add("ship_zip", "10021");

            data.Add("avs_response", "X");
            data.Add("ccv_response", "M");
            data.Add("custom1", "first custom value");
            data.Add("custom2", "second custom value");
            data.Add("custom3", "third custom value");
            data.Add("issuer_phone", "18667750556");
            data.Add("source", "affiliate_code_here");

            try
            {
                string result = SubunoApi.Run(
                    // apikey
                    "2g4g747g843",
                    // data
                    data
                );

                // result is a string in JSON format with keys/value pairs with data returned by api.
                Console.WriteLine(result);

            }
            catch (SubunoApiError e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadLine();

        }
    }
}
