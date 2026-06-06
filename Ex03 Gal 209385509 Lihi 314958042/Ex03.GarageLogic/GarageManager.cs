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
        private Vehicle m_VehicleUnderRegistration;

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
            m_VehicleUnderRegistration = null;
        }

        public void LoadDatafromDatabaseFile()
        {
            string[] fileContentLines = System.IO.File.ReadAllLines(k_DatabaseFileName);

            foreach(string line in fileContentLines)
            {
                string[] vehicleProperties = line.Split(',');
                BeginNewVehicleRegistration(vehicleProperties[(int)ePropertyType.VehicleType],
                    vehicleProperties[(int)ePropertyType.LicensePlate], vehicleProperties[(int)ePropertyType.ModelName]);
                CommitVehicleRegistration(vehicleProperties);
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
                throw new FormatException($"The vehicle with license plate '{i_LicensePlate}' is not battery based and therefore cannot be filled with electricity.");
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
                StringBuilder result = new StringBuilder();

                result.AppendLine("=== FULL VEHICLE INFORMATION ===");
                result.AppendLine($"Owner Name: {vehicleOwner.OwnerName}");
                result.AppendLine($"Owner Phone: {vehicleOwner.OwnerPhoneNumber}");
                result.Append(vehicleOwner.Vehicle);

                return result.ToString();
            }
            else
            {
                throw new KeyNotFoundException($"The provided license plate '{i_LicensePlate}' was not found in the garage.");
            }
        }

        public void BeginNewVehicleRegistration(string i_VehicleType, string i_LicensePlate, string i_ModelName)
        {
            m_VehicleUnderRegistration = VehicleCreator.CreateVehicle(i_VehicleType, i_LicensePlate, i_ModelName);
            if(m_VehicleUnderRegistration == null)
            {
                throw new ArgumentException("Unsupported vehicle type.");
            }
        }

        public List<string> GetQuestionsForCurrentRegistration()
        {
            if(m_VehicleUnderRegistration == null)
            {
                throw new NullReferenceException("No vehicle is currently being registered.");
            }

            return m_VehicleUnderRegistration.GetSpecificVehicleQuestions();
        }

        public void CommitVehicleRegistration(string[] i_VehicleProperties)
        {
            List<string> errorsList = new List<string>();

            if(float.TryParse(i_VehicleProperties[(int)ePropertyType.EnergySourcePercentage], out float energy))
            {
                try
                {
                    m_VehicleUnderRegistration.InitializeEnergySource(energy);
                }
                catch(Exception exception)
                {
                    errorsList.Add(exception.Message);
                }
            }
            else
            {
                errorsList.Add("Invalid energy percentage format. Must be a number.");
            }

            if(float.TryParse(i_VehicleProperties[(int)ePropertyType.CurrentAirPressure], out float air))
            {
                string wheelMaker = i_VehicleProperties[(int)ePropertyType.TierModel];

                try
                {
                    m_VehicleUnderRegistration.InitializeWheels(wheelMaker, air);
                }
                catch(Exception exception)
                {
                    errorsList.Add(exception.Message);
                }
            }
            else
            {
                errorsList.Add("Invalid air pressure format. Must be a number.");
            }

            try
            {
                m_VehicleUnderRegistration.InitializeSpecificVehicleProperties(i_VehicleProperties);
            }
            catch(Exception exception)
            {
                errorsList.Add(exception.Message);
            }

            if(errorsList.Count > 0)
            {
                m_VehicleUnderRegistration = null;
                throw new ArgumentException(string.Join("\n", errorsList));
            }

            string ownerName = i_VehicleProperties[(int)ePropertyType.OwnerName];
            string ownerPhone = i_VehicleProperties[(int)ePropertyType.OwnerPhoneNumber];
            VehicleOwner owner = new VehicleOwner(ownerName, ownerPhone, m_VehicleUnderRegistration);

            r_Vehicles.Add(m_VehicleUnderRegistration.LicenseId, owner);
            m_VehicleUnderRegistration = null;
        }
    }
}