using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private const string k_DatabaseFileName = "VehiclesDB.txt";
        private readonly Dictionary<VehicleOwner, eCarState> r_Vehicles;

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

        public GarageManager()
        {
            r_Vehicles = new Dictionary<VehicleOwner, eCarState>();
        }

        public void loadDatafromDatabaseFile()
        {
            string[] fileContentLines = System.IO.File.ReadAllLines(k_DatabaseFileName);

            foreach(string line in fileContentLines)
            {
                string[] vehicleProperties = line.Split(',');
                Vehicle newVehicle = VehicleCreator.CreateVehicle(vehicleProperties[(int)ePropertyType.VehicleType],
                    vehicleProperties[(int)ePropertyType.LicensePlate], vehicleProperties[(int)ePropertyType.ModelName]);
                bool isEnergySourcePercentageValid = float.TryParse(vehicleProperties[(int)ePropertyType.EnergySourcePercentage], out float energySourcePercentage);

                if(isEnergySourcePercentageValid)
                {
                    newVehicle.EnergySource.EnergySourcePercentage = energySourcePercentage;
                }
                else
                {
                    throw new ArgumentException("Invalid energy source percentage value");
                }

                if (newVehicle.Wheels != null)
                {
                    bool isWheelAirPressureValid = float.TryParse(vehicleProperties[(int)ePropertyType.CurrentAirPressure], out float airWheelPressure);

                    if (!isWheelAirPressureValid)
                    {
                        throw new ArgumentException("Invalid air wheel pressure value");
                    }
                    else
                    {
                        for (int i = 0; i < newVehicle.Wheels.Length; ++i)
                        {
                            newVehicle.Wheels[i] = new Wheel(vehicleProperties[(int)ePropertyType.TierModel], airWheelPressure, 0); // Needs a fix, can't send 0 or itself.
                        }
                    }
                }

                VehicleOwner vehicleOwner = new VehicleOwner(vehicleProperties[(int)ePropertyType.OwnerName],
                    vehicleProperties[(int)ePropertyType.OwnerPhoneNumber], newVehicle);

                newVehicle.initializeSpecificVehicleProperties(vehicleProperties);
                r_Vehicles.Add(vehicleOwner, vehicleOwner.CarState);
            }
        }
    }
}