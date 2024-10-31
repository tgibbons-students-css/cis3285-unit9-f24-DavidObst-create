using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple {
    public class URLTradeDataProvider : ITradeDataProvider {
        string tradeURL;
        ILogger logger;

        public URLTradeDataProvider(string tradeURL, ILogger logger) {

            this.tradeURL = tradeURL;
            this.logger = logger;

        }

        public IEnumerable<string> GetTradeData() {
            List<string> tradeData = new List<string>();
            logger.LogInfo("Reading trades from URL: " + tradeURL);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(tradeURL).Result;
                if (!response.IsSuccessStatusCode)
                {
                    //log error and throw an exception if the URL fails
                    logger.LogInfo($"{response.StatusCode}");
                    throw new NullReferenceException("No valid url detected");

                }

                // set up a Stream and StreamReader to access the data
                using (Stream stream = response.Content.ReadAsStreamAsync().Result)
                using (StreamReader reader = new StreamReader(stream))
                {
                    //Read each line of the text file using reader.Readline()
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {  //previously tried using (!reader.Read().Equals(null))
                        tradeData.Add(line);
                    }
                    // Read until the reader returns a null line (while !reader.Readline = null)
                    //Add each line to the tradeData list

                }
            }
            return tradeData;

        }
    }
}
