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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime RegisteredSince { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Radio> Radios { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Role> Roles { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Online { get; set; }


        /// <summary>
        /// paremeterless constructor for Trainee class
        /// </summary>
        public Trainee()
        {
            Radios = new List<Radio>();
            Online = false;

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
            RegisteredSince = DateTime.Now;
            Radios = new List<Radio>();
            Roles = new List<Role>();
            Online = false;
        }
    }
}