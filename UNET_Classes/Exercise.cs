using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace UNET_Classes
{
    public class Exercise
    {
        /// <summary>
        /// de desigenserialisation annotation staat erboven omdat anders het gebruik van de
        /// wcf service mislukt.
        /// Deze oplossing staat hier beschrven: https://stackoverflow.com/questions/4452001/object-of-type-data-timelinechartedday-cannot-be-converted-to-type-data-tim
        /// </summary>

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Number { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SpecificationName { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ExerciseName { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Trainee> TraineesAssigned { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Role> RolesAssigned { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Selected { get; set; }

        public Exercise()
        {
            TraineesAssigned = new List<Trainee>();
        }

    }
}