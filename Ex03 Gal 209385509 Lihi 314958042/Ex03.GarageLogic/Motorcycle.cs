using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        private const int k_LicenseTypeIndex = 8;
        private const int k_EngineVolumeIndex = 9;
        private const int k_NumOfWheels = 2;
        private const float k_MaxAirPressure = 30f;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

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
            
            details.AppendLine($"License Type: {m_LicenseType}");
            details.AppendLine($"Engine Volume: {m_EngineVolume} cc");
            
            return details.ToString();
        }
        
        public override void InitializeSpecificVehicleProperties(string[] i_VehicleProperties)
        {
            List<string> errorsList = new List<string>();

            if(i_VehicleProperties.Length != k_ExpectedPropertiesCount)
            {
                throw new ArgumentException($"Invalid number of properties. Expected: {k_ExpectedPropertiesCount}");
            }

            if(!Enum.TryParse(i_VehicleProperties[k_LicenseTypeIndex], out m_LicenseType))
            {
                errorsList.Add($"Invalid license type. Expected one of: {string.Join(", ", Enum.GetNames(typeof(eLicenseType)))}");
            }

            if(!int.TryParse(i_VehicleProperties[k_EngineVolumeIndex], out m_EngineVolume) || m_EngineVolume < 0)
            {
                errorsList.Add("Invalid engine volume. Expected a positive integer.");
            }

            if(errorsList.Count > 0)
            {
                throw new ArgumentException(string.Join("\n", errorsList));
            }
        }

        public override List<string> GetSpecificVehicleQuestions()
        {
            List<string> questions = new List<string>();

            questions.Add($"What is the license type ({string.Join(", ", Enum.GetNames(typeof(eLicenseType)))})? ");
            questions.Add("What is the engine volume? ");

            return questions;
        }
    }
}