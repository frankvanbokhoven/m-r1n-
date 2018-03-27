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
        public string ShortDescription { get; set;  }
        public string Description { get; set; }


        public Platform()
        {
            Description = string.Empty;
            ShortDescription = string.Empty;
        }


        /// <summary>
        /// constructor Platform
        /// </summary>
        /// <param name="_description"></param>
        public Platform(string _description)
        {
            ID = -1;
            Description = _description;
            ShortDescription = _description.Substring(0, _description.Length > 8 ? 8 : _description.Length);
        }
    }
}
