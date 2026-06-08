using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private Wheel[] m_Wheels;
        private EnergySource m_EnergySource;
        private eVehicleState m_VehicleState;
        protected const int k_ExpectedPropertiesCount = 10;
        protected string m_ModelName;
        protected string m_LicenseID;
        
        private string getVehicleTypeAsString()
        {
            string className = this.GetType().Name;
            StringBuilder spacedName = new StringBuilder();

            foreach(char currentLetter in className)
            {
                if(char.IsUpper(currentLetter) && spacedName.Length > 0)
                {
                    spacedName.Append(' ');
                }

                spacedName.Append(currentLetter);
            }

            return spacedName.ToString();
        }
        
        private string getVehicleDetailsBody()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("\n--- WHEELS INFORMATION ---");
            if(m_Wheels != null)
            {
                for(int i = 0; i < m_Wheels.Length; ++i)
                {
                    result.AppendLine($"Wheel {i + 1}:");
                    result.AppendLine($"  Manufacturer: {m_Wheels[i].ManufacturerName}");
                    result.AppendLine($"  Current Air Pressure: {m_Wheels[i].CurrentAirPressure:F2} / {m_Wheels[i].MaxAirPressure:F2} PSI");
                }
            }

            result.AppendLine("\n--- ENERGY SOURCE INFORMATION ---");
            if(m_EnergySource != null)
            {
                result.AppendLine($"Energy Level: {m_EnergySource.EnergySourcePercentage:F2}%");
                result.AppendLine(m_EnergySource.GetSpecificEnergySourceDetails());
            }

            string specificVehicleDetails = GetSpecificVehicleDetails();
            
            if(!string.IsNullOrEmpty(specificVehicleDetails))
            {
                result.AppendLine("\n--- SPECIFIC VEHICLE INFORMATION ---");
                result.Append(specificVehicleDetails);
            }

            return result.ToString();
        }

        protected Vehicle()
        {
            m_VehicleState = eVehicleState.InRepair;
        }
        
        protected abstract int NumOfWheels
        {
            get;
        }

        protected abstract float MaxAirPressure
        {
            get;
        }

        protected abstract EnergySource CreateEnergySource();
        
        protected abstract string GetSpecificVehicleDetails();

        public abstract void InitializeSpecificVehicleProperties(string[] i_VehicleProperties);

        public void InitializeWheels(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            m_Wheels = new Wheel[NumOfWheels];
            for(int i = 0; i < NumOfWheels; ++i)
            {
                m_Wheels[i] = new Wheel(i_ManufacturerName, MaxAirPressure);
                m_Wheels[i].CurrentAirPressure = i_CurrentAirPressure;
            }
        }

        public void InitializeEnergySource(float i_CurrentEnergyPercentage)
        {
            m_EnergySource = CreateEnergySource();
            m_EnergySource.EnergySourcePercentage = i_CurrentEnergyPercentage;
        }

        public void InflateAllWheels()
        {
            if(m_Wheels != null)
            {
                foreach(Wheel wheel in m_Wheels)
                {
                    wheel.InflateToMax();
                }
            }
        }

        public EnergySource EnergySource
        {
            get { return m_EnergySource; }
        }

        public string LicenseId
        {
            get { return m_LicenseID; }
        }

        public eVehicleState VehicleState
        {
            get { return m_VehicleState; }
            set { m_VehicleState = value; }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            
            result.AppendLine("=== VEHICLE INFORMATION ===");
            result.AppendLine($"Vehicle Type: {getVehicleTypeAsString()}");
            result.AppendLine($"License Plate: {m_LicenseID}");
            result.AppendLine($"Model Name: {m_ModelName}");
            result.AppendLine($"Vehicle State: {m_VehicleState}");
            result.Append(getVehicleDetailsBody());
            
            return result.ToString();
        }
        
        public abstract List<string> GetSpecificVehicleQuestions();
    }
}