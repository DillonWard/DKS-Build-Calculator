using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MyCouch;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Windows.Data.Json;
using System.Diagnostics;
using DarkSoulsCalculator.Parser;
using DarkSoulsCalculator.Models;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DarkSoulsCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static readonly HttpClient client = new HttpClient();
        public string returnedJson;
        JSonParser parser;
        List<Defence> defItems;
        List<Offence> offItems;

       public MainPage()
        {
            parser = new JSonParser();
            defItems = new List<Defence>();
            offItems = new List<Offence>();

            this.InitializeComponent();
            DataContext = this;
            data();
        }

        #region data()
        async void data()
        {

            var requestDef = await client.GetAsync("http://localhost:8080/dksItems/get-armor");
            //var requestOff = await client.GetAsync("https://couchdb-68effd.smileupps.com/dks-calc-off/_all_docs");

            string responseDef = await requestDef.Content.ReadAsStringAsync();
        //    string responseOff = await requestOff.Content.ReadAsStringAsync();            

            defItems = parser.parseDefense(JsonArray.Parse(responseDef));

            foreach (var item in defItems)
            {
                Debug.Write("\n Item: " + item.armorName.ToString());
            }
            
          //  offItems = parser.parseOffence(JsonArray.Parse(responseOff));

        }
        #endregion 
    }
}
