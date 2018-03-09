using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace UNET_Classes
{
     public class Role
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ID { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Name { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Platform> PlatformsAssigned{ get; set; }



        public Role()
        {
            PlatformsAssigned = new List<Platform>();

        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_name"></param>
        public Role(int _id, string _name)
        {
            ID = _id;
            Name = _name;
            PlatformsAssigned = new List<Platform>();
        }
    }
}