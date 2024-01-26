namespace MediRenew.ConsoleApp.ServicesConsoleApp;

public class DoctorMenu(DoctorHandler doctorHandler)
{
    private readonly DoctorHandler _doctorHandler = doctorHandler;

    public async Task DoctorMenuAsync()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("What would you like to do? ");

            string[] menu =
            {
                "1. Add a doctor",
                "2. Search for one doctor",
                "3. View all doctors",
                "4. Update a doctor",
                "5. Delete a doctor",
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
                    await _doctorHandler.AddDoctor();
                    break;

                case "2":
                    await _doctorHandler.UpdateDoctorById();
                    break;

                case "3":
                    await _doctorHandler.ViewAllDoctors();
                    break;

                case "4":
                    await _doctorHandler.UpdateDoctorById();
                    break;
                case "5":
                    await _doctorHandler.DeleteDoctorById();
                    break;
                case "0":
                    running = false;
                    break;
            }
        }
    }
}
