using LoLWiki.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace LoLWiki
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ChampionPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        Dictionary<string, string> keyValuePairs;

        public ChampionPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;



            //Task<string> championImages = new Task());

            UpdateChampionUI();

            //pageSubti
            //image.Source = new BitmapImage(new Uri("http://img1.wikia.nocookie.net/__cb20120201131554/leagueoflegends/images/5/55/ZiggsSquare.png", UriKind.Absolute));
            //image1.Source = new BitmapImage(new Uri("http://img2.wikia.nocookie.net/__cb20120201131642/leagueoflegends/images/9/93/Ziggs_OriginalSkin.jpg", UriKind.Absolute));
        }

        public async void UpdateChampionUI()
        {
            String championStats = await WikiaDotNet.WikiaDotNet.GetRevisionsContent("http://leagueoflegends.wikia.com/", new string[] { "Template:Data_Syndra" });
            championStats = championStats.Substring(championStats.IndexOf("disp_name="));
            keyValuePairs = championStats.Split('|').Select(value => value.Split('=')).ToDictionary(pair => pair[0], pair => pair[1]);

            pageTitle.Text = keyValuePairs["disp_name"];
            pageSubtitle.Text = UppercaseFirst(keyValuePairs["title"]);

            JObject allimages = JObject.Parse(await WikiaDotNet.WikiaDotNet.GetAllImages("http://leagueoflegends.wikia.com", new string[] { "Syndra" }));
            JArray imagesArray = (JArray)allimages["query"]["allimages"];

            

            for(int i = 0; i < imagesArray.Count; i++)
            {
                if((string)imagesArray[i]["name"] == keyValuePairs["image"])
                {
                    image.Source = new BitmapImage(new Uri((string)imagesArray[i]["url"]));
                }

                if ((string)imagesArray[i]["name"] == keyValuePairs["disp_name"] + "_OriginalSkin.jpg")
                {
                    image1.Source = new BitmapImage(new Uri((string)imagesArray[i]["url"]));
                }
            }
            /*foreach (string str in allimages["query"]["allimages"])
            {
                pageSubtitle.Text = (string)allimages["query"]["allimages"][0]["name"];
            }*/
        }

        static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="Common.NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="Common.SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="Common.NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="Common.NavigationHelper.LoadState"/>
        /// and <see cref="Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
