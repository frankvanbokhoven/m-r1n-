using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace UNET_Classes
{
    public class Trainee
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ID { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Name { get; set; }


        public Trainee()
        {

        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_name"></param>
        public Trainee(int _id, string _name)
        {
            ID = _id;
            Name = _name;
        }
    }
}