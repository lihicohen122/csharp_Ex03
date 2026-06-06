using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    internal class FuelTruck : Vehicle
    {
        private const int k_CanDeliverColdCargoIndex = 8;
        private const int k_CargoVolumeIndex = 9;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const float k_MaxFuelAmount = 125f;
        private const int k_NumOfWheels = 14;
        private const float k_MaxAirPressure = 28f;
        private bool m_CanDeliverColdCargo;
        private float m_CargoVolume;

        protected override EnergySource CreateEnergySource()
        {
            return new Engine(k_FuelType, k_MaxFuelAmount);
        }

        protected override int NumOfWheels
        {
            get { return k_NumOfWheels; }
        }

        protected override float MaxAirPressure
        {
            get { return k_MaxAirPressure; }
        }

        protected override string GetSpecificVehicleDetails()
        {
            StringBuilder details = new StringBuilder();
            
            details.AppendLine($"Refrigerated Cargo: {(m_CanDeliverColdCargo ? "Yes" : "No")}");
            details.AppendLine($"Cargo Volume: {m_CargoVolume:F2} Cubic Meters");
            
            return details.ToString();
        }
        
        public FuelTruck(string i_LicenseID, string i_ModelName)
        {
            m_LicenseID = i_LicenseID;
            m_ModelName = i_ModelName;
        }

        public override void InitializeSpecificVehicleProperties(string[] i_VehicleProperties)
        {
            List<string> errorsList = new List<string>();

            if(i_VehicleProperties.Length != k_ExpectedPropertiesCount)
            {
                throw new ArgumentException($"Invalid number of properties. Expected: {k_ExpectedPropertiesCount}");
            }

            if(!bool.TryParse(i_VehicleProperties[k_CanDeliverColdCargoIndex], out m_CanDeliverColdCargo))
            {
                errorsList.Add("Invalid value for can deliver cold cargo. Expected 'True' or 'False'.");
            }

            if(!float.TryParse(i_VehicleProperties[k_CargoVolumeIndex], out m_CargoVolume) || m_CargoVolume <= 0)
            {
                errorsList.Add("Invalid value for cargo volume. Expected a positive real number.");
            }

            if(errorsList.Count > 0)
            {
                throw new ArgumentException(string.Join("\n", errorsList));
            }
        }

        public override List<string> GetSpecificVehicleQuestions()
        {
            List<string> questions = new List<string>();

            questions.Add("Does the truck deliver refrigerated cargo? (True/False): ");
            questions.Add("What is the cargo volume (in cubic meters)? ");

            return questions;
        }
    }
}