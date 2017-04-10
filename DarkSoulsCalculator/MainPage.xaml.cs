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
using System.Collections.ObjectModel;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DarkSoulsCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region creating instances
        private static readonly HttpClient client = new HttpClient();
        public string returnedJson;
        JSonParser parser;
        List<Defence> defItems;
        List<Offence> offItems;
        List<HeadItems> headList;
        List<ChestItems> chestList;
        List<HandItems> handsList;
        List<HeldItems> lHandList;
        List<HeldItems> rHandList;
        List<LegItems> legList;
        #endregion

        public MainPage()
        {
            
            #region initializing
            headList = new List<HeadItems>();
            chestList = new List<ChestItems>();
            handsList = new List<HandItems>();
            lHandList = new List<HeldItems>();
            rHandList = new List<HeldItems>();
            legList = new List<LegItems>();
            /*
            headItems = new List<HeadItems>();
            chestItems = new List<ChestItems>();
            handItems = new List<HandItems>();
            legItems = new List<LegItems>();
            heldItems = new List<HeldItems>();
            */
            parser = new JSonParser();
            defItems = new List<Defence>();
            offItems = new List<Offence>();
            #endregion

            this.InitializeComponent();
            DataContext = this;
            data();
        }

    async void data()
        {
            #region Totals
            // totals which are calculated based off of items picked
            int physDefTotal = 0;
            int fireDefTotal = 0;
            int lightDefTotal = 0;
            int magicDefTotal = 0;
            int poiseTotal = 0;

            int physAtkTotal = 0;
            int fireAtkTotal = 0;
            int lightAtkTotal = 0;
            int magicAtkTotal = 0;
            int bleedTotal = 0;
            #endregion

            // requests are made to the databases
            var requestDef = await client.GetAsync("http://localhost:8080/dksItems/get-armor");
            var requestOff = await client.GetAsync("http://localhost:8080/dksItems/get-weapons");

            // the response from the database is read in
            string responseDef = await requestDef.Content.ReadAsStringAsync();
            string responseOff = await requestOff.Content.ReadAsStringAsync();            

            // the response is read in and then sent to the Parse class, where it is parsed from JSon
            defItems = parser.parseDefense(JsonArray.Parse(responseDef));
            offItems = parser.parseOffence(JsonArray.Parse(responseOff));

            foreach (var item in defItems)
            {
                /* Items are read in and are parsed
                 *  after the items are parsed, they are broken down into categories based on their armor 'type'
                 *  they are added to seperate lists depending on their 'type', which is read in and changed to string
                 *  they are also added to observable collections
                 */
                Debug.Write("\n Armor: " + item.armorName + "\n Physical Defence: " + item.physicalDefence + "\n Fire Defence: " + item.fireDefence + "\n Magical Defence: " + item.magicDefence + "\n Lightning Defence: " + item.lightningDefence + "\n Poise: " + item.poise);
               
                #region breaking items into sections
                switch (item.armorType.ToString())
                {
                    case "Chest":
                        var newChestItems = new ChestItems(item.armorName, item.physicalDefence, item.fireDefence, item.magicDefence, item.lightningDefence, item.poise);
                        chestList.Add(newChestItems); // adding the new item to chestItems or 'ChestItems' class
                        //chestList.Add(newChestItems.name); // adding it to list so it can be used in an observable collection

                        break;

                    case "Helm":
                        var newHeadItems = new HeadItems(item.armorName, item.physicalDefence, item.fireDefence, item.magicDefence, item.lightningDefence, item.poise);
                        headList.Add(newHeadItems);

                        // headList.Add(headItems);
                        break;

                    case "Arms":
                        var newHandsItem = new HandItems(item.armorName, item.physicalDefence, item.fireDefence, item.magicDefence, item.lightningDefence, item.poise);
                        handsList.Add(newHandsItem);
                        break;

                    case "Legs":
                       var newLegItem = new LegItems(item.armorName, item.physicalDefence, item.fireDefence, item.magicDefence, item.lightningDefence, item.poise);
                        legList.Add(newLegItem);
                        break;
                }
                #endregion
            }

            #region storing weapons
            foreach (var wep in offItems)
            {
                // adds items read in to 'HeldItems' list
                var newWeapon = new HeldItems(wep.weaponName, wep.physicalOffence, wep.fireOffence, wep.magicOffence, wep.lightningOffence, wep.bleedOffence);
                rHandList.Add(newWeapon); // adds to observable collection
                lHandList.Add(newWeapon);
                Debug.Write("\n Weapon Name: " + wep.weaponName + "\n Physical Offence: " + wep.physicalOffence + "\n Fire Offence: " + wep.fireOffence + "\n Magic Offence: " + wep.magicOffence + "\n Lightning Offence: " + wep.lightningOffence + "\n Bleed Offence: " + wep.bleedOffence);
            }
            #endregion

            #region making variables visible on form
            // makes the variables visible 
            physDef.Text = physDefTotal.ToString();
            fireDef.Text = fireDefTotal.ToString();
            lightDef.Text = lightDefTotal.ToString();
            magicDef.Text = magicDefTotal.ToString();
            poise.Text = poiseTotal.ToString();

            physAtk.Text = physAtkTotal.ToString();
            fireAtk.Text = fireAtkTotal.ToString();
            lightAtk.Text = lightAtkTotal.ToString();
            magicAtk.Text = magicAtkTotal.ToString();
            bleedAtk.Text = bleedTotal.ToString();
            #endregion

            #region Binding to dropdown boxes
            // binds the source for the dropdown as the observable collections
            headDropdown.ItemsSource = headList;
            headDropdown.DisplayMemberPath = "name";
            chestDropdown.ItemsSource = chestList;
            chestDropdown.DisplayMemberPath = "name";
            armsDropdown.ItemsSource = handsList;
            armsDropdown.DisplayMemberPath = "name";
            legsDropdown.ItemsSource = legList;
            legsDropdown.DisplayMemberPath = "name";
            leftHandDropdown.ItemsSource = rHandList;
            leftHandDropdown.DisplayMemberPath = "name";
            rightHandDropdown.ItemsSource = lHandList;
            rightHandDropdown.DisplayMemberPath = "name";
            #endregion

        }

    }

}