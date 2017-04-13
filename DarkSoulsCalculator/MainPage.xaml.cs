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
        private double[,] totals = new double[6, 5];
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
            double physDefTotal = 0;
            double fireDefTotal = 0;
            double lightDefTotal = 0;
            double magicDefTotal = 0;
            double poiseTotal = 0;

            double physAtkTotal = 0;
            double fireAtkTotal = 0;
            double lightAtkTotal = 0;
            double magicAtkTotal = 0;
            double bleedTotal = 0;
            #endregion

            // requests are made to the databases
            var requestDef = await client.GetAsync("http://52.11.81.164:8080/dksItems/get-armor");
            var requestOff = await client.GetAsync("http://52.11.81.164:8080/dksItems/get-weapons");

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
                 */
                Debug.Write("\n Armor: " + item.armorName + "\n Physical Defence: " + item.physicalDefence + "\n Fire Defence: " + item.fireDefence + "\n Magical Defence: " + item.magicDefence + "\n Lightning Defence: " + item.lightningDefence + "\n Poise: " + item.poise);
               
                #region breaking items into sections
                switch (item.armorType.ToString())
                {
                    case "Chest":
                        var newChestItems = new ChestItems(item.armorName, item.physicalDefence, item.fireDefence, item.magicDefence, item.lightningDefence, item.poise);
                        chestList.Add(newChestItems); // adding the new item to chestItems or 'ChestItems' class

                        break;

                    case "Helm":
                        var newHeadItems = new HeadItems(item.armorName, item.physicalDefence, item.fireDefence, item.magicDefence, item.lightningDefence, item.poise);
                        headList.Add(newHeadItems);
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
            // looks for the "name" variable for each item and displays them
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

            // event handlers for whenever a dropdown box is changed
            headDropdown.SelectionChanged += HeadDropdown_SelectionChanged;
            chestDropdown.SelectionChanged += ChestDropdown_SelectionChanged;
            armsDropdown.SelectionChanged += ArmsDropdown_SelectionChanged;
            legsDropdown.SelectionChanged += LegsDropdown_SelectionChanged;
            leftHandDropdown.SelectionChanged += LeftHandDropdown_SelectionChanged;
            rightHandDropdown.SelectionChanged += RightHandDropdown_SelectionChanged;
        }


        #region Dropdown box event handlers
        private void RightHandDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            populateValues(5);

        }

        private void LeftHandDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            populateValues(4);

        }

        private void LegsDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            populateValues(3);

        }

        private void ArmsDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            populateValues(2);
        }

        private void ChestDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            populateValues(1);
        }

        private void HeadDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            populateValues(0);

        }
        #endregion

        // reads in which dropdown box has been changed with a controller variable
        private void populateValues(int dropdownmenu)
        {
            string selectedItem = string.Empty;
            int control = 0;
            string[] stats = new string[5];
            double[] statsToDouble = new double[5]; // parses stats to double

            /* a switch case for the dropdown box changed
             * depending on which one is changed and the controller,
             * the seleccted item will be assigned to the dropdown box changed
             */
            switch (dropdownmenu)
            {
                case 0:
                    selectedItem = headDropdown.SelectedItem.ToString();
                    break;
                case 1:
                    selectedItem = chestDropdown.SelectedItem.ToString();
                    break;
                case 2:
                    selectedItem = armsDropdown.SelectedItem.ToString();
                    break;
                case 3:
                    selectedItem = legsDropdown.SelectedItem.ToString();
                    break;
                case 4:
                    selectedItem = leftHandDropdown.SelectedItem.ToString();
                    break;
                case 5:
                    selectedItem = rightHandDropdown.SelectedItem.ToString();
                    break;
                default:
                    break;
            }

            // the stats are read in and are seperated by a comma ','
            for (int i = 0; i <= selectedItem.Length - 1; i++)
            {

                if (selectedItem[i].Equals(',') == true)
                {
                    control++;
                }
                else if (control == 0)
                {
                    stats[0] += selectedItem[i];
                }
                else if (control == 1)
                {
                    stats[1] += selectedItem[i];
                }
                else if (control == 2)
                {
                    stats[2] += selectedItem[i];
                }
                else if (control == 3)
                {
                    stats[3] += selectedItem[i];
                }
                else if (control == 4)
                {
                    stats[4] += selectedItem[i];
                }


            }

            for (int i = 0; i < stats.Length; i++)
            {
                statsToDouble[i] = double.Parse(stats[i]);// parses the stats to double
            }

            // passes the values into the next function for calculation depending on the stats/what was changed
            calculateStats(statsToDouble, dropdownmenu);
            
        }
        
        private void calculateStats(double[] doubleStats, int control)
        {
            switch (control)
            {
                case 0://helmet
                    for(int i = 0; i < doubleStats.Length; i++)
                    {
                        totals[control, i] = doubleStats[i];
                    }
                    break;
                case 1://chest
                    for (int i = 0; i < doubleStats.Length; i++)
                    {
                        totals[control, i] = doubleStats[i];
                    }
                    break;
                case 2://arms
                    for (int i = 0; i < doubleStats.Length; i++)
                    {
                        totals[control, i] = doubleStats[i];
                    }
                    break;
                case 3://legs
                    for (int i = 0; i < doubleStats.Length; i++)
                    {
                        totals[control, i] = doubleStats[i];
                    }
                    break;
                case 4://lhand
                    for (int i = 0; i < doubleStats.Length; i++)
                    {
                        totals[control, i] = doubleStats[i];
                    }
                    break;
                case 5://rhand
                    for (int i = 0; i < doubleStats.Length; i++)
                    {
                        totals[control, i] = doubleStats[i];
                    }
                    break;
                default:
                    break;
            }

            double[] totalStatsDef = new double[5];
            double[] totalStatsAtt = new double[5];

            // the first 4 dropdown boxes are for defence
            // if 'i' is greater than 3, the stats are for offence
            for (int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if(i > 3)
                    {
                        totalStatsAtt[j] += totals[i, j];
                    }
                    else
                    {
                        totalStatsDef[j] += totals[i, j];
                    }
                }
            }

            // prints them out to the screen
            physDef.Text = totalStatsDef[0].ToString();
            fireDef.Text = totalStatsDef[1].ToString();
            lightDef.Text = totalStatsDef[2].ToString();
            magicDef.Text = totalStatsDef[3].ToString();
            poise.Text = totalStatsDef[4].ToString();

            physAtk.Text = totalStatsAtt[0].ToString();
            fireAtk.Text = totalStatsAtt[1].ToString();
            lightAtk.Text = totalStatsAtt[2].ToString();
            magicAtk.Text = totalStatsAtt[3].ToString();
            bleedAtk.Text = totalStatsAtt[4].ToString();

        }
    }

}