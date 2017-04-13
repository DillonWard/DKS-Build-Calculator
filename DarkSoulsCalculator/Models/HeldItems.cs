using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkSoulsCalculator.Models
{
    class HeldItems
    {
        public string name { get; set; }
        public double physical { get; set; }
        public double fire { get; set; }
        public double magic { get; set; }
        public double lightning { get; set; }
        public double bleed { get; set; }

        public HeldItems(String name, double physical, double fire, double magic, double lightning, double bleed)
        {
            this.name = name;
            this.physical = physical;
            this.fire = fire;
            this.magic = magic;
            this.lightning = lightning;
            this.bleed = bleed;

        }

        public override string ToString()
        {
            return "" + physical + "," + fire + "," + magic + "," + lightning + "," + bleed;
        }
    }
}
