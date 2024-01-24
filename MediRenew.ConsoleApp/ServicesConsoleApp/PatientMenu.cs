﻿namespace MediRenew.ConsoleApp.ServicesConsoleApp
{
    public class PatientMenu(PatientHandler patient)
    {
        private readonly PatientHandler _patient = patient;
        
        public async Task PatientMenuAsync()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do? ");

                string[] menu =
                {
                "1. Add patient",
                "2. Find a patient through ID",
                "3. View all patients",
                "4. Update patient",
                "5. Delete patient",
                "0. Return to main menu"
            };

                for (int i = 0; i < menu.Length; i++)
                {
                    Console.WriteLine(menu[i]);
                }

                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        await _patient.AddPatient();
                        break;

                    case "2":
                        await _patient.ViewOnePatientWithId();
                        break;

                    case "3":
                        await _patient.ViewAllPatiens();
                        break;

                    case "4":
                        break;

                    case "5":
                        break;

                    case "0":
                        running = false;
                        break;
                }
            }
        }
    }
}