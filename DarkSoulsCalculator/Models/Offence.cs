using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkSoulsCalculator.Models
{
    #region Offence
    public class Offence
    {
        public string weaponName { get; set; }
        public string weaponType { get; set; }
        public double physicalOffence { get; set; }
        public double magicOffence { get; set; }
        public double fireOffence { get; set; }
        public double lightningOffence { get; set; }
        public double bleedOffence { get; set; }
    }
    #endregion
}
