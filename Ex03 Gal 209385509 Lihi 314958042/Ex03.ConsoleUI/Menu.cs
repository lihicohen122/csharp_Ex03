using System;

namespace Ex03.ConsoleUI
{
    public class Menu
    {
        public void PrintMenu()
        {
            Console.WriteLine("1. Load the system with vehicle's data from the database file");
            Console.WriteLine("2. Insert a new vehicle record to the garage");
            Console.WriteLine("3. Display a list of all license plates of the vehicles in the garage, with an option to filter by vehicle state");
            Console.WriteLine("4. Change vehicle state in the garage");
            Console.WriteLine("5. Inflate all wheels of a vehicle in the garage");
            Console.WriteLine("6. Fill fuel for a fuel-based vehicle in the garage");
            Console.WriteLine("7. Charge battery for an electric vehicle in the garage");
            Console.WriteLine("8. Present full vehicle data of a vehicle in the garage");
            Console.WriteLine("9. Quit");
        }
    }
}