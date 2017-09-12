﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNET_Classes
{
    public class Instructor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Exercise> Exercises { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_name"></param>
        public Instructor(int _id, string _name)
        {
            Exercises = new List<Exercise>();
            ID = _id;
            Name = _name;

        }

        public Instructor()
        {

        }

    }


}