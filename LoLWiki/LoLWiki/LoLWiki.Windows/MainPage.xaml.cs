using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace LoLWiki
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //Task task = new Task(WikiaDotNet.WikiaDotNet.GetRevisionsContent("http://leagueoflegends.wikia.com/", new string[] { "Template:Data_Ziggs" }));
            //this.Frame.Navigate(typeof(ChampionPage));
            DebugTxt.Text = WikiaDotNet.WikiaDotNet.GetAllImages("http://leagueoflegends.wikia.com", new string[] { "Ziggs" }).Result;
        }
    }
}
