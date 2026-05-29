using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class GarageConsoleUI
    {
        private const int k_QuitOption = 9;
        private Menu m_garageUIMenu;
        private GarageManager m_garage;

        private void printWelcomingUserMessage()
        {
            Console.WriteLine("Welcome to Gal and Lihi's garage system!");
        }

        private void loadDatafromDatabaseFile()
        {
            m_garage.loadDatafromDatabaseFile();
        }

        private void enterNewVehicleEntryToGarage()
        {

        }

        private void displayListOfAllLicensePlatesInGarage()
        {

        }

        private void changeVehicleStateInGarage()
        {

        }

        private void inflateAllWheelsOfVehicleInGarage()
        {

        }

        private void fillFuelForFuelBasedVehicleInGarageIfPossible()
        {

        }

        private void chargeBatteryForElectricVehicleInGarageIfPossible()
        {

        }

        private void printFullVehicleDataOfVehicleInGarage()
        {

        }

        public GarageConsoleUI()
        {
            m_garageUIMenu = new Menu();
            m_garage = new GarageManager();
        }

        public void RunApp()
        {
            int userOption = -1;

            printWelcomingUserMessage();
            while(userOption != k_QuitOption)
            {
                m_garageUIMenu.printMenu();
                string userInput = Console.ReadLine();
                bool isValidUserOption = int.TryParse(userInput, out userOption);

                while (!isValidUserOption || userOption < 1 || userOption > k_QuitOption)
                {
                    Console.WriteLine("Invalid option number. Please enter a valid option.");
                    userInput = Console.ReadLine();
                    isValidUserOption = int.TryParse(userInput, out userOption);
                }

                switch (userOption)
                {
                    case 1:
                        loadDatafromDatabaseFile();
                        break;
                    case 2:
                        enterNewVehicleEntryToGarage();
                        break;
                    case 3:
                        displayListOfAllLicensePlatesInGarage();
                        break;
                    case 4:
                        changeVehicleStateInGarage();
                        break;
                    case 5:
                        inflateAllWheelsOfVehicleInGarage();
                        break;
                    case 6:
                        fillFuelForFuelBasedVehicleInGarageIfPossible();
                        break;
                    case 7:
                        chargeBatteryForElectricVehicleInGarageIfPossible();
                        break;
                    case 8:
                        printFullVehicleDataOfVehicleInGarage();
                        break;
                    default:
                        Console.WriteLine($"Invalid option. Please enter a valid option number (1-{k_QuitOption}).");
                        break;
                }
            }
        }
    }
}
