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
        protected eCarColor m_CarColor;
        protected eNumberOfCarDoors m_NumberOfDoors;

        public override void initializeSpecificVehicleProperties(string[] i_VehicleProperties)
        {
            if (i_VehicleProperties.Length != k_ExpectedPropertiesCount)
            {
                throw new ArgumentException($"Invalid number of properties. Expected: {k_ExpectedPropertiesCount}");
            }
            if (!Enum.TryParse(i_VehicleProperties[k_CarColorIndex], out m_CarColor))
            {
                throw new ArgumentException($"Invalid car color. Expected one of: {string.Join(", ", Enum.GetNames(typeof(eCarColor)))}");
            }
            if (!Enum.TryParse(i_VehicleProperties[k_NumberOfDoorsIndex], out m_NumberOfDoors))
            {
                throw new ArgumentException($"Invalid number of doors. Expected one of: {string.Join(", ", Enum.GetValues(typeof(eNumberOfCarDoors)))}");
            }
        }

        protected override int NumOfWheels
        {
            get { return 5; }
        }

        protected override float MaxAirPressure
        {
            get { return 31f; }
        }

        protected override string GetSpecificVehicleDetails()
        {
            StringBuilder details = new StringBuilder();
            details.AppendLine($"Car Color: {m_CarColor}");
            details.AppendLine($"Number of Doors: {m_NumberOfDoors}");
            return details.ToString();
        }

        public override Dictionary<string, string> GetSpecificVehicleQuestions()
        {
            Dictionary<string, string> questions = new Dictionary<string, string>();
            questions.Add("CarColor", $"Please enter car color ({string.Join(", ", Enum.GetNames(typeof(eCarColor)))}):");
            questions.Add("NumberOfDoors", $"Please enter number of doors ({string.Join(", ", Enum.GetNames(typeof(eNumberOfCarDoors)))}):");
            return questions;
        }
    }
}