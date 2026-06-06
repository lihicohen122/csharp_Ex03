using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private const int k_CarColorIndex = 8;
        private const int k_NumberOfDoorsIndex = 9;
        private const int k_NumOfWheels = 5;
        private const float k_MaxAirPressure = 31f;
        private eCarColor m_CarColor;
        private eNumberOfCarDoors m_NumberOfDoors;

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
            
            details.AppendLine($"Car Color: {m_CarColor}");
            details.AppendLine($"Number of Doors: {m_NumberOfDoors}");
            
            return details.ToString();
        }

        public override List<string> GetSpecificVehicleQuestions()
        {
            List<string> questions = new List<string>();

            questions.Add($"What is the car's color ({string.Join(", ", Enum.GetNames(typeof(eCarColor)))})? ");
            questions.Add($"How many doors does the car have ({string.Join(", ", Enum.GetNames(typeof(eNumberOfCarDoors)))})? ");

            return questions;
        }
        
        public override void InitializeSpecificVehicleProperties(string[] i_VehicleProperties)
        {
            List<string> errorsList = new List<string>();

            if(i_VehicleProperties.Length != k_ExpectedPropertiesCount)
            {
                throw new ArgumentException($"Invalid number of properties. Expected: {k_ExpectedPropertiesCount}");
            }

            if(!Enum.TryParse(i_VehicleProperties[k_CarColorIndex], out m_CarColor))
            {
                errorsList.Add($"Invalid car color. Expected one of: {string.Join(", ", Enum.GetNames(typeof(eCarColor)))}");
            }

            if(!Enum.TryParse(i_VehicleProperties[k_NumberOfDoorsIndex], out m_NumberOfDoors))
            {
                errorsList.Add($"Invalid number of doors. Expected one of: {string.Join(", ", Enum.GetNames(typeof(eNumberOfCarDoors)))}");
            }

            if(errorsList.Count > 0)
            {
                throw new ArgumentException(string.Join("\n", errorsList));
            }
        }
    }
}