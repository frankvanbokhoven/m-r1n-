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
using UNET_ServiceStatus.UNET_Service;


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

            try
            {
                Console.Clear();
                //  Console.BackgroundColor = ConsoleColor.Green;
                Console.Write("************************************************************************************");
                Console.Write(Environment.NewLine);

                Console.Write("HSO Marine 2017 - UNET Service Status Version 04 december 2017");
                Console.Write(Environment.NewLine);
                Console.Write("UNET Status weergever van de UNET_Service");
                Console.Write(Environment.NewLine);
                Console.Write("************************************************************************************");
                Console.Write(Environment.NewLine);


                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(DateTime.Now.ToString() + " Get instructor data");
                Instructor currentInstructor = service.GetAllInstructorData(InstructorID);
                Console.Write(Environment.NewLine);



                //Instructors
                var instrlist = service.GetInstructors();

                List<UNET_Classes.Instructor> instructorlist = instrlist.ToList<UNET_Classes.Instructor>(); //C# v3 manier om een array in een list te krijgen
                if (instructorlist.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("No instructors in UNET_Service!");
                    Console.Write(Environment.NewLine);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    foreach (UNET_Classes.Instructor instr in instructorlist) //then ENABLE them, based on whatever comes from the service
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Instructor::  " + instr.ID + " Name: " + instr.Name);
                        Console.Write(Environment.NewLine);
                        foreach (UNET_Classes.Exercise exe in instr.Exercises)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("                 Exerciseid: " + exe.Number + " ExerciseName: " + exe.ExerciseName + "  Specificationname: " + exe.SpecificationName);
                            Console.Write(Environment.NewLine);
                            foreach (UNET_Classes.Trainee trn in exe.TraineesAssigned)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("                    Assigned Trainee:  Traineeid:: " + trn.ID + "  Trainee name: " + trn.Name);
                                Console.Write(Environment.NewLine);
                            }

                            foreach (UNET_Classes.Role rol in exe.RolesAssigned)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("                 Assigned role: " + rol.ID + "  Role name: " + rol.Name);
                                Console.Write(Environment.NewLine);
                            }

                            foreach (UNET_Classes.Radio rad in exe.RadiosAssigned)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("                 Assigned radio: " + rad.ID + "  Radio name: " + rad.Description + " NoiseLevel: " + rad.NoiseLevel + " Freq: " + rad.Frequency);
                                Console.Write(Environment.NewLine);
                            }

                        }

                        foreach (UNET_Classes.Role rol in instr.AssignedRoles)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("                  Assigned instructor role:  Roleid:: " + rol.ID + "  Trainee name: " + rol.Name);
                            Console.Write(Environment.NewLine);
                        }

                    }

                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("************************************************************************************");
                Console.Write(Environment.NewLine);
                Console.Write(DateTime.Now.ToString() + " Hieronder de toestand van de stam gegevens");
                Console.Write(Environment.NewLine);
                Console.Write("************************************************************************************");
                Console.Write(Environment.NewLine);

                //exercises
                var resultlist = service.GetExercises();
                List<UNET_Classes.Exercise> lst = resultlist.ToList<UNET_Classes.Exercise>(); //C# v3 manier om een array in een list te krijgen
                if (lst.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("No data in UNET_Service");
                    Console.Write(Environment.NewLine);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    foreach (UNET_Classes.Exercise exercise in lst) //then ENABLE them, based on whatever comes from the service
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Exercise::  " + exercise.Number + " Name: " + exercise.ExerciseName);
                        Console.Write(Environment.NewLine);


                        foreach (UNET_Classes.Trainee trn in exercise.TraineesAssigned)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(string.Format("Exercise: {2} Trainee: {0} Name: {1}", trn.ID, trn.Name, exercise.Number));
                            Console.Write(Environment.NewLine);

                        }

                    }
                }
                //trainees
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("------------------------------------------------------------------------------------");
                Console.Write(Environment.NewLine);
                Console.Write(DateTime.Now.ToString() + " Aangemelde trainees");
                Console.Write(Environment.NewLine);
                Console.Write("------------------------------------------------------------------------------------");
                Console.Write(Environment.NewLine);


                var resultlisttrainees = service.GetTrainees();
                List<UNET_Classes.Trainee> lstTrainee = resultlisttrainees.ToList<UNET_Classes.Trainee>(); //C# v3 manier om een array in een list te krijgen
                if (lst.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("No data in UNET_Service for trainees");
                    Console.Write(Environment.NewLine);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    foreach (UNET_Classes.Trainee trainee in lstTrainee) //then ENABLE them, based on whatever comes from the service
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Trainee::  " + trainee.ID + " Name: " + trainee.Name + "  Aangemeld sinds: " + trainee.RegisteredSince.ToString());
                        Console.Write(Environment.NewLine);
                        foreach (Radio rn in trainee.Radios)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(string.Format("                              Assigned radio: {0} Trainee: {0} Descr: {1} Noise: {2}", rn.ID, rn.Description, rn.NoiseLevel));
                            Console.Write(Environment.NewLine);

                        }

                    }
                }
                //Assists
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("------------------------------------------------------------------------------------");
                Console.Write(Environment.NewLine);
                Console.Write(DateTime.Now.ToString() + " Assists");
                Console.Write(Environment.NewLine);
                Console.Write("------------------------------------------------------------------------------------");
                Console.Write(Environment.NewLine);


                var resultlistassists = service.GetAssists(-1);
                List<UNET_Classes.Assist> lstAssist = resultlistassists.ToList<UNET_Classes.Assist>();
                if (lst.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("No data in UNET_Service for Assist");
                    Console.Write(Environment.NewLine);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    foreach (UNET_Classes.Assist assist in lstAssist) //then ENABLE them, based on whatever comes from the service
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Pending assist::  " + assist.ID + " Started by: " + assist.TraineeID + " " + assist.TraineeInfo + "  Requested: " + assist.RequestTime.ToString());
                        Console.Write(Environment.NewLine);
                       

                    }
                }




                //  Console.Write(Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("************************************************************************************");
                Console.Write(Environment.NewLine);
                Console.Write(DateTime.Now.ToString() + " The above is the status at time: " + DateTime.Now.ToString());
                Console.Write(Environment.NewLine);
                Console.Write("************************************************************************************");
                Console.Write(Environment.NewLine);

            }
            catch (Exception ex)
            {
                string message = ex.Message;

            }




        }
    }
}
