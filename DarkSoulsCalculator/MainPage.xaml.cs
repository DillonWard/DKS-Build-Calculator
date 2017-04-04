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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DarkSoulsCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       public MainPage()
        {
           this.InitializeComponent();
            DataContext = this;
            Connection();

        }
        private static readonly HttpClient client = new HttpClient();
        public string returnedJson;

        public void Connection()
        {
            data();
            
            /*
            using (var db = new MyCouchClient("https://couchdb-68effd.smileupps.com/dks-calc/_all_docs", null))
            {
                // querys go here I assume
                var query = new MyCouch.Requests.QueryViewRequest("text/plain");
                System.Diagnostics.Debug.WriteLine(query);
                var resp = db.Views.QueryAsync(query);


                System.Diagnostics.Debug.WriteLine("BEFORE QUERY");
                System.Diagnostics.Debug.WriteLine(resp.Id);
                System.Diagnostics.Debug.WriteLine("AFTER QUERY");

            }*/
        }



        async void data()
        {

            var response = await client.GetAsync("https://couchdb-68effd.smileupps.com/dks-calc/_all_docs");
            string responseString = await response.Content.ReadAsStringAsync();

            JObject root = JObject.Parse(responseString);
          //  JsonArray rows =  root. GetValue("rows");



            // create list of json objs 

            // parse Json obj key value pairs

        }


        #region Offence
        public class Offence
        {
            public string weaponName { get; set; }
            public double physicalOffence { get; set; }
            public double magicOffence { get; set; }
            public double fireOffence { get; set; }
            public double lightningOffence { get; set; }
            public double bleedOffence { get; set; }
        }
        #endregion

        #region Defence
        public class Defence
        {
            public string armorName { get; set; }
            public string armorType { get; set; }
            public double physicalDefence { get; set; }
            public double magicDefence { get; set; }
            public double fireDefence { get; set; }
            public double lightningDefence { get; set; }
            public double poise { get; set; }
        }
        #endregion

        class Item
        {
            static void Items(String[] args)
            {
                Offence[] offenceItems = new Offence[50];
                Defence[] defenceItems = new Defence[50];

                for (int i = 0; i < 50; i++)
                {
                    offenceItems[i] = new Offence();
                    defenceItems[i] = new Defence();
                }
            }
        }

    }
}
