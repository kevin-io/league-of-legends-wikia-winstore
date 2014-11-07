using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace WikiaDotNet
{
    // Get Champions Stats
    //http://leagueoflegends.wikia.com/api.php?action=query&prop=revisions&titles=Template:Data_Ziggs|Template:Data_Nidalee&rvprop=content&format=json
    // Get Champions Skills
    //http://leagueoflegends.wikia.com/api.php?action=query&prop=revisions&titles=Ziggs|Nidalee&rvprop=content&format=json
    // Get All Images
    //http://leagueoflegends.wikia.com/api.php?action=query&list=allimages&ailimit=500&aiprefix=Kalista&aiprop=dimensions|url

    class WikiaDotNet
    {        
        /// <summary>
        /// Get last revision content from page
        /// </summary>
        /// <param name="wikiDomain"></param>
        /// <param name="pageTitles"></param>
        /// <returns>JSON string</returns>
        public static async Task<string> GetRevisionsContent(string wikiDomain, string[] pageTitles)
        {            
            var uri = new Uri(String.Format("{0}/api.php?action=query&prop=revisions&titles={1}&rvprop=content&format=json", wikiDomain, BuildArrayForUrlRequests(pageTitles)));
            HttpClient httpClient = new HttpClient();
            string result = string.Empty;
            // Always catch network exceptions for async methods
            try
            {
                result = await httpClient.GetStringAsync(uri);
            }
            catch
            {
                // Details in ex.Message and ex.HResult.       
            }

            // Once your app is done using the HttpClient object call dispose to 
            // free up system resources (the underlying socket and memory used for the object)
            httpClient.Dispose();
            return result;
        }

        private static string BuildArrayForUrlRequests(string[] array)
        {
            string result = String.Empty;

            for(int i = 0; i < array.Length; i++)
            {
                result += (i == 0) ? (array[i]) : ("|" + array[i]);
            }
            return result;
        }
    }
}
