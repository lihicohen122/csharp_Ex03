using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected const int k_ExpectedPropertiesCount = 10;
        protected string m_ModelName;
        protected string m_LicenseID;
        protected Wheel[] m_Wheels;
        protected EnergySource m_EnergySource;
        protected eVehicleState m_VehicleState;

        protected Vehicle()
        {
            m_VehicleState = eVehicleState.InRepair;
        }

        protected abstract EnergySource CreateEnergySource();

        public abstract void initializeSpecificVehicleProperties(string[] i_VehicleProperties);

        public void InitializeWheels(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            m_Wheels = new Wheel[NumOfWheels];
            for (int i = 0; i < NumOfWheels; ++i)
            {
                m_Wheels[i] = new Wheel(i_ManufacturerName, i_CurrentAirPressure, MaxAirPressure);
            }
        }

        public void InitializeEnergySource(float i_CurrentEnergyPercentage)
        {
            m_EnergySource = CreateEnergySource();
            m_EnergySource.EnergySourcePercentage = i_CurrentEnergyPercentage;
        }

        public void InflateAllWheels()
        {
            if(Wheels != null)
            {
                foreach(Wheel wheel in m_Wheels)
                {
                    wheel.InflateToMax();
                }
            }
        }

        protected abstract int NumOfWheels
        {
            get;
        }

        protected abstract float MaxAirPressure
        {
            get;
        }

        public EnergySource EnergySource
        {
            get { return m_EnergySource; }
        }

        public Wheel[] Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        public eVehicleState VehicleState
        {
            get { return m_VehicleState; }
            set { m_VehicleState = value; }
        }
    }
}