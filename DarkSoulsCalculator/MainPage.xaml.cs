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
        List<HeadItems> headItems;
        List<ChestItems> chestItems;
        List<HandItems> handItems;
        List<LegItems> legItems;
        List<HeldItems> heldItems;


        public MainPage()
        {
            headItems = new List<HeadItems>();
            chestItems = new List<ChestItems>();
            handItems = new List<HandItems>();
            legItems = new List<LegItems>();
            heldItems = new List<HeldItems>();
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
            var requestOff = await client.GetAsync("http://localhost:8080/dksItems/get-weapons");

            string responseDef = await requestDef.Content.ReadAsStringAsync();
            string responseOff = await requestOff.Content.ReadAsStringAsync();            

            defItems = parser.parseDefense(JsonArray.Parse(responseDef));
            offItems = parser.parseOffence(JsonArray.Parse(responseOff));

            foreach (var item in defItems)
            {
                Debug.Write("\n Armor: " + item.armorName + "\n Physical Defence: " + item.physicalDefence + "\n Fire Defence: " + item.fireDefence + "\n Magical Defence: " + item.magicDefence + "\n Lightning Defence: " + item.lightningDefence + "\n Poise: " + item.poise);
                switch (item.armorType.ToString())
                {
                    case "Chest":
                        chestItems.Add( new ChestItems(item.armorName, item.physicalDefence, item.fireDefence, item.magicDefence, item.lightningDefence, item.poise));
                        break;

                    case "Helm":
                        headItems.Add(new HeadItems(item.armorName, item.physicalDefence, item.fireDefence, item.magicDefence, item.lightningDefence, item.poise));
                        break;

                    case "Hands":
                        handItems.Add(new HandItems(item.armorName, item.physicalDefence, item.fireDefence, item.magicDefence, item.lightningDefence, item.poise));
                        break;

                    case "Legs":
                        legItems.Add(new LegItems(item.armorName, item.physicalDefence, item.fireDefence, item.magicDefence, item.lightningDefence, item.poise));
                        break;

                }
            }

            foreach (var wep in offItems)
            {
                heldItems.Add(new HeldItems(wep.weaponName, wep.physicalOffence, wep.fireOffence, wep.magicOffence, wep.lightningOffence, wep.bleedOffence));
                Debug.Write("\n Weapon Name: " + wep.weaponName + "\n Physical Offence: " + wep.physicalOffence + "\n Fire Offence: " + wep.fireOffence + "\n Magic Offence: " + wep.magicOffence + "\n Lightning Offence: " + wep.lightningOffence + "\n Bleed Offence: " + wep.bleedOffence);
            }
        }
        #endregion 


    }
}
