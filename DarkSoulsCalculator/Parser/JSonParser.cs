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
            List<Defence> tempList = new List<Defence>();

            foreach(var item in tempDef)
            {
                var obj = item.GetObject();

                Defence defence = new Defence();

                foreach (var key in obj.Keys)
                {
                    IJsonValue val;

                    if (!obj.TryGetValue(key, out val))
                        continue;

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

                    tempList.Add(offence);
                }
            }

            return tempList;
        }
    }
}
