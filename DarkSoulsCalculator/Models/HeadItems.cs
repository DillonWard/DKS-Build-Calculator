using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkSoulsCalculator.Models
{
    public class HeadItems
    {
        public string name { get; set; }
        public double physical { get; set; }
        public double fire { get; set; }
        public double magic { get; set; }
        public double lightning { get; set; }
        public double poise { get; set; }

        public HeadItems(String name, double physical, double fire, double magic, double lightning, double poise)
        {
            this.name = name;
            this.physical = physical;
            this.fire = fire;
            this.magic = magic;
            this.lightning = lightning;
            this.poise = poise;


        }

        public override string ToString()
        {
            return ""+physical+","+fire+","+magic+","+lightning+","+poise;
        }
    }
}