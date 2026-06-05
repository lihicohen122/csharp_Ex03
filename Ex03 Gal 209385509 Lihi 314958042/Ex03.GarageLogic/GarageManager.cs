using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private const string k_DatabaseFileName = "VehiclesDB.txt";
        private readonly Dictionary<string, VehicleOwner> r_Vehicles;

        private enum ePropertyType
        {
            VehicleType,
            LicensePlate,
            ModelName,
            EnergySourcePercentage,
            TierModel,
            CurrentAirPressure,
            OwnerName,
            OwnerPhoneNumber,
        }

        private void checkIfValidNonStringValues(bool i_IsEnergySourcePercentageValid, bool i_IsCurrentAirPressureValid)
        {
            if(!i_IsEnergySourcePercentageValid && !i_IsCurrentAirPressureValid)
            {
                throw new ArgumentException("Invalid energy source percentage and air wheel pressure values");
            }
            else if(!i_IsEnergySourcePercentageValid)
            {
                throw new ArgumentException("Invalid energy source percentage value");
            }
            else if(!i_IsCurrentAirPressureValid)
            {
                throw new ArgumentException("Invalid air wheel pressure value");
            }
        }
        private Vehicle getVehicleByLicensePlate(string i_LicensePlate)
        {
            if(r_Vehicles.TryGetValue(i_LicensePlate, out VehicleOwner vehicleOwner))
            {
                return vehicleOwner.Vehicle;
            }
            throw new KeyNotFoundException($"The provided license plate '{i_LicensePlate}' was not found in the garage.");
        }

        public GarageManager()
        {
            r_Vehicles = new Dictionary<string, VehicleOwner>();
        }

        public void LoadDatafromDatabaseFile()
        {
            string[] fileContentLines = System.IO.File.ReadAllLines(k_DatabaseFileName);

            foreach(string line in fileContentLines)
            {
                string[] vehicleProperties = line.Split(',');
                string vehicleType = vehicleProperties[(int)ePropertyType.VehicleType];
                string licensePlate = vehicleProperties[(int)ePropertyType.LicensePlate];
                string modelName = vehicleProperties[(int)ePropertyType.ModelName];
                string tierModel = vehicleProperties[(int)ePropertyType.TierModel];
                string ownerName = vehicleProperties[(int)ePropertyType.OwnerName];
                string ownerPhoneNumber = vehicleProperties[(int)ePropertyType.OwnerPhoneNumber];
                bool isEnergySourcePercentageValid = float.TryParse(vehicleProperties[(int)ePropertyType.EnergySourcePercentage], out float energySourcePercentage);
                bool isCurrentAirPressureValid = float.TryParse(vehicleProperties[(int)ePropertyType.CurrentAirPressure], out float currentAirPressure);

                checkIfValidNonStringValues(isEnergySourcePercentageValid, isCurrentAirPressureValid);
                Vehicle newVehicle = VehicleCreator.CreateVehicle(vehicleType, licensePlate, modelName);

                newVehicle.InitializeWheels(tierModel, currentAirPressure);
                newVehicle.InitializeEnergySource(energySourcePercentage);
                newVehicle.initializeSpecificVehicleProperties(vehicleProperties);
                VehicleOwner vehicleOwner = new VehicleOwner(ownerName, ownerPhoneNumber, newVehicle);

                r_Vehicles.Add(licensePlate, vehicleOwner);
            }
        }

        public string DisplayAllLicensePlates()
        {
            return string.Join(", ", r_Vehicles.Keys);
        }

        public bool DoesDatabaseContainLicensePlate(string i_LicensePlate)
        {
            return r_Vehicles.ContainsKey(i_LicensePlate);
        }

        public void SetVehicleInRepairByLicensePlate(string i_LicensePlate)
        {
            SetVehicleState(i_LicensePlate, eVehicleState.InRepair.ToString());
        }

        public void SetVehicleState(string i_LicensePlate, string i_NewVehicleState)
        {
            if(r_Vehicles.TryGetValue(i_LicensePlate, out VehicleOwner vehicleOwner))
            {
                if(Enum.TryParse(i_NewVehicleState, out eVehicleState newVehicleState))
                {
                    vehicleOwner.Vehicle.VehicleState = newVehicleState;
                }
                else
                {
                    throw new ArgumentException($"The provided vehicle state '{i_NewVehicleState}' is not valid."
                                                + $"The possible vehicle states are: {string.Join(", ", Enum.GetNames(typeof(eVehicleState)))}");
                }
            }
            else
            {
                throw new KeyNotFoundException($"The provided license plate '{i_LicensePlate}' was not found in the garage.");
            }
        }

        public void InflateAllWheelsOfVehicleByLicensePlate(string i_LicensePlate)
        {
            if(r_Vehicles.TryGetValue(i_LicensePlate, out VehicleOwner vehicleOwner))
            {
                vehicleOwner.Vehicle.InflateAllWheels();
            }
            else
            {
                throw new KeyNotFoundException($"The provided license plate '{i_LicensePlate}' was not found in the garage.");
            }
        }
        
        public bool IsValidFuelType(string i_FuelType) // do we need wrapper to encapsulate?
        {
            return Enum.TryParse(i_FuelType, out eFuelType newFuelType);
        }

        public void FillGasForFuelVehicle(string i_LicensePlate, string i_FuelType, string i_FuelAmountInput)
        {
            if(!float.TryParse(i_FuelAmountInput, out float fuelAmount) || fuelAmount <= 0)
            {
                throw new FormatException($"The provided fuel amount '{i_FuelAmountInput}' is not a valid real and positive number.");
            }
            if(!Enum.TryParse(i_FuelType, out eFuelType fuelType))
            {
                throw new FormatException($"The provided fuel type '{i_FuelType}' is not valid. "
                                          + $"The possible fuel types are: {string.Join(", ", Enum.GetNames(typeof(eFuelType)))}");
            }
            
            Vehicle vehicleToFillWithGas = getVehicleByLicensePlate(i_LicensePlate);
            Engine vehicleEngine = vehicleToFillWithGas.EnergySource as Engine;
            
            if(vehicleEngine == null)
            {
                throw new FormatException($"The vehicle with license plate '{i_LicensePlate}' is not fuel based and therefore cannot be filled with fuel.");
            }

            vehicleEngine.addFuelIfPossible(fuelAmount, fuelType);
        }

        public void ChargeBatteryForElectricVehicle(string i_LicensePlate, string i_MinutesToLoadBatteryWithUserUnput)
        {
            if(!float.TryParse(i_MinutesToLoadBatteryWithUserUnput, out float minutesToLoadBattery) || minutesToLoadBattery <= 0)
            {
                throw new FormatException($"The provided time in minutes '{i_MinutesToLoadBatteryWithUserUnput}' is not a valid real and positive number.");
            }

            float hoursToLoadBattery = minutesToLoadBattery / 60f;
            Vehicle vehicleToChargeWithElectricity = getVehicleByLicensePlate(i_LicensePlate);
            Battery vehicleBattery = vehicleToChargeWithElectricity.EnergySource as Battery;
            
            if(vehicleBattery == null)
            {
                throw new FormatException($"The vehicle with license plate '{i_LicensePlate}' is not fuel based and therefore cannot be filled with fuel.");
            }

            vehicleBattery.addHoursToBatteryCapacityIfPossible(hoursToLoadBattery);
        }

        public string DisplayAllLicensePlatesFilteredByState(string i_VehicleState)
        {
            if(!Enum.TryParse(i_VehicleState, out eVehicleState vehicleState))
            {
                throw new ArgumentException($"Invalid vehicle state '{i_VehicleState}'. "
                    + $"Valid states are: {string.Join(", ", Enum.GetNames(typeof(eVehicleState)))}");
            }

            List<string> filteredLicenses = new List<string>();
            foreach(KeyValuePair<string, VehicleOwner> kvp in r_Vehicles)
            {
                if(kvp.Value.Vehicle.VehicleState == vehicleState)
                {
                    filteredLicenses.Add(kvp.Key);
                }
            }

            return string.Join(", ", filteredLicenses);
        }

        public string GetAvailableVehicleStates()
        {
            return string.Join(", ", Enum.GetNames(typeof(eVehicleState)));
        }

        public string GetFullVehicleProperties(string i_LicensePlate)
        {
            if(r_Vehicles.TryGetValue(i_LicensePlate, out VehicleOwner vehicleOwner))
            {
                Vehicle vehicle = vehicleOwner.Vehicle;
                StringBuilder result = new StringBuilder();
                result.AppendLine("=== FULL VEHICLE INFORMATION ===");
                result.AppendLine($"License Plate: {vehicle.LicenseId}");
                result.AppendLine($"Model Name: {vehicle.ModelName}");
                result.AppendLine($"Owner Name: {vehicleOwner.OwnerName}");
                result.AppendLine($"Vehicle State: {vehicle.VehicleState}");
                result.AppendLine($"Owner Phone: {vehicleOwner.OwnerPhoneNumber}");
                result.AppendLine();
                result.Append(vehicle.GetVehicleDetailsBody());
                return result.ToString();
            }
            else
            {
                throw new KeyNotFoundException($"The provided license plate '{i_LicensePlate}' was not found in the garage.");
            }
        }
    }
}