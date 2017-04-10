using DarkSoulsCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace DarkSoulsCalculator.Parser
{
    class JSonParser
    {
        public List<Defence> parseDefense(JsonArray tempDef)
        {
            // a temporary list is initialized for storing items read in
            List<Defence> tempList = new List<Defence>();

            foreach(var item in tempDef)
            {
                // objects are read in and stored in obj
                var obj = item.GetObject();

                Defence defence = new Defence();

                foreach (var key in obj.Keys)
                {
                    IJsonValue val;

                    if (!obj.TryGetValue(key, out val))
                        continue;

                    // the keys are read in and stored based on how the JSon is read in
                    // the JSon contains keys such as "itemName", and "itemType" and are stored accordingly
                    // they are stored into the Defence list as items
                    switch(key)
                    {
                        case "itemName":
                            defence.armorName = val.GetString();
                            break;

                        case "itemType":
                            defence.armorType = val.GetString();
                            break;

                        case "physDefence":
                            defence.physicalDefence = val.GetNumber();
                            break;

                        case "fireDefence":
                            defence.magicDefence = val.GetNumber();
                            break;

                        case "magicDefence":
                            defence.fireDefence = val.GetNumber();
                            break;

                        case "lightningDefence":
                            defence.lightningDefence = val.GetNumber();
                            break;

                        case "poise":
                            defence.poise = val.GetNumber();
                            break;
                    }

                   
                } // inner loop 
                // the temp items are added to the Defence list
                tempList.Add(defence);
            } // outter loop

            return tempList;
        }


        public List<Offence> parseOffence(JsonArray tempDef)
        {
            List<Offence> tempList = new List<Offence>();

            foreach (var item in tempDef)
            {
                var obj = item.GetObject();

                Offence offence = new Offence();

                foreach (var key in obj.Keys)
                {
                    IJsonValue val;

                    if (!obj.TryGetValue(key, out val))
                        continue;

                    switch (key)
                    {
                        case "itemName":
                            offence.weaponName = val.GetString();
                            break;

                        case "itemType":
                            offence.weaponType = val.GetString();
                            break;

                        case "physOffence":
                            offence.physicalOffence = val.GetNumber();
                            break;

                        case "fireOffence":
                            offence.fireOffence= val.GetNumber();
                            break;

                        case "magicOffence":
                            offence.magicOffence = val.GetNumber();
                            break;

                        case "lightningOffence":
                            offence.lightningOffence = val.GetNumber();
                            break;

                        case "bleedOffence":
                            offence.bleedOffence = val.GetNumber();
                            break;
                    }

                }
                tempList.Add(offence);
            }

            return tempList;
        }
    }
}
