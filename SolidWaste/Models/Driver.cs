using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolidWaste.Models
{
    public class Driver
    {
        public int DriverID { get; set; }
        public int? TruckID { get; set; }
        public string name { get; set; }

        public virtual Truck Truck { get; set; }


    }
}