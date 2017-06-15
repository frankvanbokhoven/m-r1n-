using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace UNET_Classes
{

    public class Radio
    {

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
        public int NoiseLevel { get; set; }
    }
}