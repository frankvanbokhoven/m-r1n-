using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace UNET_Classes
{
    public class Platform
    {
        public int ID { get; set; }

        [Description("ShortDescription is the text on the button")]
        public string ShortDescription { get; set; }
        public string Description { get; set; }


        public Platform()
        {
        
        }


        public Platform(string _description)
        {
            ID = -1;
            Description = _description;
            ShortDescription = _description;
        }
    }
}
