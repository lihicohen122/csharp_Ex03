using System;
using System.Collections.Generic;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Enums;

namespace Ex03.ConsoleUI
{
    internal class GarageConsoleUI
    {
        private const int k_QuitOption = 10;
        private Menu m_GarageUIMenu;
        private readonly GarageManager r_GarageManager;

        private void printWelcomingUserMessage()
        {
            Console.WriteLine("Welcome to Gal and Lihi's garage system!");
        }

        private void loadDataFromDatabaseFile()
        {
            try
            {
                r_GarageManager.LoadDataFromDatabaseFile();
                Console.WriteLine("Data loaded successfully!");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error loading database: {exception.Message}");
            }
        }

        private void enterNewVehicleEntryToGarage()
        {
            Console.WriteLine("What is the license plate of the vehicle? ");
            string licensePlate = Console.ReadLine();

            if (r_GarageManager.DoesDatabaseContainLicensePlate(licensePlate))
            {
                handleExistingVehicle(licensePlate);
            }
            else
            {
                handleNewVehicleRegistration(licensePlate);
            }
        }

        private void handleExistingVehicle(string i_LicensePlate)
        {
            Console.WriteLine($"A vehicle with license plate '{i_LicensePlate}' is already in the garage. Moving state to 'InRepair'.");
            r_GarageManager.SetVehicleInRepairByLicensePlate(i_LicensePlate);
        }

        private void handleNewVehicleRegistration(string i_LicensePlate)
        {
            string vehicleType = getValidVehicleType();

            Console.WriteLine("What is the vehicle's model name? ");
            string modelName = Console.ReadLine();

            r_GarageManager.BeginNewVehicleRegistration(vehicleType, i_LicensePlate, modelName);
            string[] vehicleDataArray = new string[10];

            collectBasicVehicleInfo(vehicleDataArray, vehicleType, i_LicensePlate, modelName);
            collectSpecificVehicleInfo(vehicleDataArray);

            try
            {
                r_GarageManager.CommitVehicleRegistration(vehicleDataArray);
                Console.WriteLine("Success! New vehicle has been successfully added to the garage.");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Error: Vehicle was not added to the garage due to the abovementioned reason(s). Please try again.");
            }
        }

        private string getValidVehicleType()
        {
            string supportedTypes = string.Join(", ", VehicleCreator.SupportedTypes);
            Console.WriteLine($"What is the vehicle type? ({supportedTypes}): ");
            string vehicleType = Console.ReadLine();

            while (!VehicleCreator.SupportedTypes.Contains(vehicleType))
            {
                Console.WriteLine("Unsupported vehicle type. Please select a valid type from the list:");
                vehicleType = Console.ReadLine();
            }

            return vehicleType;
        }

        private void collectBasicVehicleInfo(string[] i_DataArray, string i_Type, string i_License, string i_Model)
        {
            i_DataArray[(int)ePropertyType.VehicleType] = i_Type;
            i_DataArray[(int)ePropertyType.LicensePlate] = i_License;
            i_DataArray[(int)ePropertyType.ModelName] = i_Model;

            Console.WriteLine("Who is the owner of the vehicle? ");
            i_DataArray[(int)ePropertyType.OwnerName] = Console.ReadLine();
            Console.WriteLine("What is the owner's phone number? ");
            i_DataArray[(int)ePropertyType.OwnerPhoneNumber] = Console.ReadLine();
            Console.WriteLine("What is the current energy percentage (0-100)? ");
            i_DataArray[(int)ePropertyType.EnergySourcePercentage] = Console.ReadLine();
            Console.WriteLine("Who is the manufacturer of the wheels?");
            i_DataArray[(int)ePropertyType.TierModel] = Console.ReadLine();
            Console.WriteLine("What is the current wheels air pressure?");
            i_DataArray[(int)ePropertyType.CurrentAirPressure] = Console.ReadLine();
        }

        private void collectSpecificVehicleInfo(string[] i_DataArray)
        {
            List<string> specificQuestions = r_GarageManager.GetQuestionsForCurrentRegistration();
            int currentIndex = (int)ePropertyType.SpecificProperty1;

            foreach (string question in specificQuestions)
            {
                Console.WriteLine(question);
                i_DataArray[currentIndex] = Console.ReadLine();
                currentIndex++;
            }
        }

        private void displayListOfAllLicensePlatesInGarage()
        {
            Console.WriteLine("Would you like to filter the list by vehicle state? (Yes/No)");
            string filterAnswer = Console.ReadLine();
            
            if(filterAnswer == "Yes")
            {
                Console.WriteLine($"Available states: {r_GarageManager.GetAvailableVehicleStates()}");
                Console.WriteLine("Which vehicle state would you like to filter by? ");
                string vehicleState = Console.ReadLine();
                
                try
                {
                    Console.WriteLine($"Vehicles in the garage with state '{vehicleState}': {r_GarageManager.DisplayAllLicensePlatesFilteredByState(vehicleState)}");
                }
                catch(Exception exception)
                {
                    Console.WriteLine($"Error: {exception.Message}");
                }
            }
            else if(filterAnswer == "No")
            {
                Console.WriteLine($"All license plates in the garage: {r_GarageManager.DisplayAllLicensePlates()}");
            }
            else
            {
                Console.WriteLine("Invalid input. Please answer 'Yes' or 'No'.");
            }
        }

        private void changeVehicleStateInGarage()
        {
            Console.WriteLine("What is the license plate of the vehicle? ");
            string licensePlate = Console.ReadLine();

            if(r_GarageManager.DoesDatabaseContainLicensePlate(licensePlate))
            {
                Console.WriteLine($"What is the requested new vehicle state for the vehicle with the license plate '{licensePlate}' in the garage ('InRepair', 'Repaired' or 'Paid'): ");
                string newVehicleState = Console.ReadLine();
                
                try
                {
                    r_GarageManager.SetVehicleState(licensePlate, newVehicleState);
                    Console.WriteLine($"Success! The vehicle state for the vehicle with license plate '{licensePlate}' has been updated to '{newVehicleState}'!");
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Error: {exception.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Error: There is no vehicle with the license plate {licensePlate} in the database.");
            }
        }

        private void inflateAllWheelsOfVehicleInGarage()
        {
            Console.WriteLine("What is the license plate of the vehicle? ");
            string licensePlate = Console.ReadLine();

            try
            {
                r_GarageManager.InflateAllWheelsOfVehicleByLicensePlate(licensePlate);
                Console.WriteLine($"Success! All wheels in vehicle with license plate '{licensePlate}' are inflated to their maximum pressure!");
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
        }

        private void fillFuelForFuelBasedVehicleInGarageIfPossible()
        {
            Console.WriteLine("What is the license plate of the vehicle? ");
            string licensePlate = Console.ReadLine();
            
            if(r_GarageManager.DoesDatabaseContainLicensePlate(licensePlate))
            {
                Console.WriteLine("What type of fuel would you like to fill? (Soler, Octan95, Octan96, Octan98): ");
                string fuelType = Console.ReadLine();

                Console.WriteLine("How many liters of fuel would you like to fill? ");
                string fuelAmountInput = Console.ReadLine();

                try
                {
                    r_GarageManager.FillGasForFuelVehicle(licensePlate, fuelType, fuelAmountInput);
                    Console.WriteLine($"Success! Vehicle '{licensePlate}' was successfully fueled.");
                }
                catch(Exception exception)
                {
                    Console.WriteLine($"Error: {exception.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Error: There is no vehicle with the license plate {licensePlate} in the database.");
            }
        }

        private void chargeBatteryForElectricVehicleInGarageIfPossible()
        {
            Console.WriteLine("What is the license plate of the vehicle? ");
            string licensePlate = Console.ReadLine();
            
            if(r_GarageManager.DoesDatabaseContainLicensePlate(licensePlate))
            {
                Console.WriteLine("How many minutes should we charge the vehicle's battery?");
                string minutesToLoadBatteryWithUserUnput = Console.ReadLine();

                try
                {
                    r_GarageManager.ChargeBatteryForElectricVehicle(licensePlate, minutesToLoadBatteryWithUserUnput);
                    Console.WriteLine($"Success! Vehicle '{licensePlate}' was successfully charged.");
                }
                catch(Exception exception)
                {
                    Console.WriteLine($"Error: {exception.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Error: There is no vehicle with the license plate {licensePlate} in the database.");
            }
        }

        private void printFullVehicleDataOfVehicleInGarage()
        {
            Console.WriteLine("What is the license plate of the vehicle? ");
            string licensePlate = Console.ReadLine();
            
            if(r_GarageManager.DoesDatabaseContainLicensePlate(licensePlate))
            {
                Console.WriteLine(r_GarageManager.GetFullVehicleProperties(licensePlate));
            }
            else
            {
                Console.WriteLine($"Error: There is no vehicle with the license plate {licensePlate} in the database.");
            }
        }

        public GarageConsoleUI()
        {
            m_GarageUIMenu = new Menu();
            r_GarageManager = new GarageManager();
        }

        public void RunApp()
        {
            int userOption = -1;

            printWelcomingUserMessage();
            while(userOption != k_QuitOption)
            {
                m_GarageUIMenu.PrintMenu();
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
                        loadDataFromDatabaseFile();
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
