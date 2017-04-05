using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkSoulsCalculator.Models
{
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

        public double totalPhysicalDefence = 0;
        public double totalMagicDefence = 0;
        public double totalFireDefence = 0;
        public double totalLightningDefence = 0;
        public double totalPoise = 0;


    }
    #endregion
}
