using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Drawing;
using UNET_Classes;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace UNET_ServiceStatus
{
    public class getData
    {

        private int InstructorID = Convert.ToInt16(ConfigurationManager.AppSettings["InstructorID"].ToString());
        /// WCF service
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();

        public void InitSettings()
        {

        }

        public void GetAndReportStatus()
        {

            // we ask the WCF service (UNET_service) what exercises there are and display them on the screen by making buttons
            // visible/invisible and also set the statusled
            //  {
            if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
                service.Open();
            }
            //we moeten  de huidige status ophalen van de instructeur/exercises/trainee/roles/radios
            //en hiermee de knoppen de juiste kleur geven
            UNET_Service.Instructor currentInstructor = service.GetAllInstructorData(InstructorID);

            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(DateTime.Now.ToString() + " Exact importer Get basic data from Service (Sim)");
                Console.Write("*");


                //var resultlist = service.GetExercises();

                //List<UNET_Classes.Exercise> lst = resultlist.ToList<UNET_Classes.Exercise>(); //C# v3 manier om een array in een list te krijgen
                //foreach (UNET_Classes.Exercise exercise in lst) //then ENABLE them, based on whatever comes from the service
                //{
                //    Console.ForegroundColor = ConsoleColor.White;
                //    Console.Write("Exercise::  " + exercise.Number + " Name: " + exercise.ExerciseName);
                //    Console.Write(Environment.NewLine);


                //    foreach (UNET_Classes.Trainee trn in exercise.TraineesAssigned)
                //    {
                //        Console.ForegroundColor = ConsoleColor.Magenta;
                //        Console.Write("Exit, omdat de testloop is overschreden");
                //    }

                //}


                Console.Write(Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(DateTime.Now.ToString() + "The above is the status at time: " + DateTime.Now.ToString("D"));
            }
            catch (Exception ex)
            {
                //  SendMailViaMijnhostingpartner("Exact Online Import error processing dealerinfo. Errorinfo: " & ex.Message, "no filename relevant")

            }




        }
    }
}
