using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace UNET_Classes
{

    public class Radio
    {
        private UNETRadioState state;
        /// <summary>
        /// de desigenserialisation annotation staat erboven omdat anders het gebruik van de
        /// wcf service mislukt.
        /// Deze oplossing staat hier beschrven: https://stackoverflow.com/questions/4452001/object-of-type-data-timelinechartedday-cannot-be-converted-to-type-data-tim
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ID { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Description { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Frequency { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int NoiseLevel { get; set; }
        public UNETRadioState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        public Radio()
        {

        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_description"></param>
        /// <param name="_frequency"></param>
        public Radio(int _id, string _description, string _frequency)
        {
            ID = _id;
            Description = _description;
            Frequency = _frequency;
        }

    }
}