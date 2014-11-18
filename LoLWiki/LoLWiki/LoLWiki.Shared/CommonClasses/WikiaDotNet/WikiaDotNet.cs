using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace WikiaDotNet
{
    // Get Champions Stats
    //http://leagueoflegends.wikia.com/api.php?action=query&prop=revisions&titles=Template:Data_Ziggs&rvprop=content&format=json
    // Get Champions Skills
    //http://leagueoflegends.wikia.com/api.php?action=query&prop=revisions&titles=Ziggs&rvprop=content&format=json
    // Get All Images
    //http://leagueoflegends.wikia.com/api.php?action=query&list=allimages&ailimit=500&aiprop=dimensions|url&format=json&aiprefix=Ziggs

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

        public static async Task<string> GetAllImages(string wikiDomain, string[] pageTitles)
        {
            string uriStr = String.Format("{0}/api.php?action=query&list=allimages&ailimit=500&aiprop=dimensions|url&format=json&aiprefix={1}", wikiDomain, BuildArrayForUrlRequests(pageTitles));
            //uriStr = uriStr.Substring(0, uriStr.Length);
            var uri = new Uri(uriStr);

            HttpClient httpClient = new HttpClient();
            string result = string.Empty;
            // Always catch network exceptions for async methods

            result = await httpClient.GetStringAsync(uri);

            // Once your app is done using the HttpClient object call dispose to 
            // free up system resources (the underlying socket and memory used for the object)
            httpClient.Dispose();
            return result.ToString();
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
