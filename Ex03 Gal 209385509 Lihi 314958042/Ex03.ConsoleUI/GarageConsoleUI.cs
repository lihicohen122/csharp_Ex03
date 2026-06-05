using System;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class GarageConsoleUI
    {
        private const int k_QuitOption = 10;
        private Menu m_garageUIMenu;
        private GarageManager m_GarageManager;

        private void printWelcomingUserMessage()
        {
            Console.WriteLine("Welcome to Gal and Lihi's garage system!");
        }

        private void loadDatafromDatabaseFile()
        {
            try
            {
                m_GarageManager.LoadDatafromDatabaseFile();
                Console.WriteLine("Data loaded successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error loading database: {e.Message}");
            }
        }

        private void enterNewVehicleEntryToGarage()
        {
            Console.WriteLine("Please enter the license plate of your vehicle: ");
            string licensePlate = Console.ReadLine();
            if(m_GarageManager.DoesDatabaseContainLicensePlate(licensePlate))
            {
                Console.WriteLine($"A vehicle with the license plate {licensePlate} is already in the database!");
                m_GarageManager.SetVehicleInRepairByLicensePlate(licensePlate);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void displayListOfAllLicensePlatesInGarage()
        {
            Console.WriteLine("Would you like to filter the list by vehicle state? (Yes/No)");
            string filterAnswer = Console.ReadLine();
            
            if(filterAnswer == "Yes")
            {
                Console.WriteLine($"Available states: {m_GarageManager.GetAvailableVehicleStates()}");
                Console.WriteLine("Please enter the vehicle state to filter by:");
                string vehicleState = Console.ReadLine();
                
                try
                {
                    Console.WriteLine($"Vehicles in the garage with state '{vehicleState}': {m_GarageManager.DisplayAllLicensePlatesFilteredByState(vehicleState)}");
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine($"All license plates in the garage: {m_GarageManager.DisplayAllLicensePlates()}");
            }
        }

        private void changeVehicleStateInGarage()
        {
            Console.WriteLine("Please enter the license plate of your vehicle: ");
            string licensePlate = Console.ReadLine();
            if(m_GarageManager.DoesDatabaseContainLicensePlate(licensePlate))
            {
                Console.WriteLine($"Please enter the requested new vehicle state for the vehicle with the license plate '{licensePlate}' in the garage ('In Repair', 'Repaired' or 'Paid'): ");
                string newVehicleState = Console.ReadLine();

                newVehicleState = newVehicleState?.Replace(" ", string.Empty);
                try
                {
                    m_GarageManager.SetVehicleState(licensePlate, newVehicleState);
                    Console.WriteLine($"Success! The vehicle state for the vehicle with license plate '{licensePlate}' has been updated to '{newVehicleState}'!");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Error: There is no vehicle with the license plate {licensePlate} in the database.");
            }
        }

        private void inflateAllWheelsOfVehicleInGarage()
        {
            Console.WriteLine("Please enter the license plate of your vehicle: ");
            string licensePlate = Console.ReadLine();

            try
            {
                m_GarageManager.InflateAllWheelsOfVehicleByLicensePlate(licensePlate);
                Console.WriteLine($"Success! All wheels in vehicle with license plate '{licensePlate}' are inflated to their maximum pressure!");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private void fillFuelForFuelBasedVehicleInGarageIfPossible()
        {
            Console.WriteLine("Please enter the license plate of your vehicle: ");
            string licensePlate = Console.ReadLine();
            
            if(m_GarageManager.DoesDatabaseContainLicensePlate(licensePlate))
            {
                Console.WriteLine("Please enter vehicle fuel type (Soler, Octan95, Octan97 or Octan98): ");
                string fuelType = Console.ReadLine();
                
                if(m_GarageManager.IsValidFuelType(fuelType))
                {
                    Console.WriteLine("Please enter the amount of fuel to fill (in liters): ");
                    string fuelAmountInput = Console.ReadLine();
                    try
                    {
                        m_GarageManager.FillGasForFuelVehicle(licensePlate, fuelType, fuelAmountInput);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: Invalid fuel type. Please enter one of the following fuel types: Soler, Octan95, Octan97 or Octan98.");
                }
            }
            else
            {
                Console.WriteLine($"Error: There is no vehicle with the license plate {licensePlate} in the database.");
            }
        }

        private void chargeBatteryForElectricVehicleInGarageIfPossible()
        {
            Console.WriteLine("Please enter the license plate of your vehicle: ");
            string licensePlate = Console.ReadLine();
            
            if(m_GarageManager.DoesDatabaseContainLicensePlate(licensePlate))
            {
                Console.WriteLine("How many minutes should we charge the vehicle's battery?");
                string minutesToLoadBatteryWithUserUnput = Console.ReadLine();
                try
                {
                    m_GarageManager.ChargeBatteryForElectricVehicle(licensePlate, minutesToLoadBatteryWithUserUnput);
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Error: There is no vehicle with the license plate {licensePlate} in the database.");
            }
        }

        private void printFullVehicleDataOfVehicleInGarage()
        {
            Console.WriteLine("Please enter the license plate of your vehicle: ");
            string licensePlate = Console.ReadLine();
            
            if(m_GarageManager.DoesDatabaseContainLicensePlate(licensePlate))
            {
                Console.WriteLine(m_GarageManager.GetFullVehicleProperties(licensePlate));
            }
            else
            {
                Console.WriteLine($"Error: There is no vehicle with the license plate {licensePlate} in the database.");
            }
        }

        public GarageConsoleUI()
        {
            m_garageUIMenu = new Menu();
            m_GarageManager = new GarageManager();
        }

        public void RunApp()
        {
            int userOption = -1;

            printWelcomingUserMessage();
            while(userOption != k_QuitOption)
            {
                m_garageUIMenu.PrintMenu();
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
