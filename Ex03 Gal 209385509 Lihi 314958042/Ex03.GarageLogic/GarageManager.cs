using System;
using System.Collections.Generic;
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

        public bool IsDatabaseContainsLicensePlate(string i_LicensePlate)
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
    }
}