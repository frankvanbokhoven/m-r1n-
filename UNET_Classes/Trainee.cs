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
        public string FreeswitchID { get; set; }
         [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime RegisteredSince { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Radio> Radios { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Role> Roles { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Online { get; set; }


        /// <summary>
         /// </summary>
        public Trainee(string _traineeName)
        {
            Radios = new List<Radio>();
            Roles = new List<Role>();
            Name = _traineeName;

        }

        /// <summary>
        /// paremeterless constructor for Trainee class
        /// </summary>
        public Trainee()
        {
            Radios = new List<Radio>();
            Roles = new List<Role>();
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
            FreeswitchID = _id.ToString(); //todo: moet nog expliciet worden gezet vanuit SIM
        }
    }
}