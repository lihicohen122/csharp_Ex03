using System;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class Engine : EnergySource
    {
        private readonly eFuelType m_FuelType;
        private readonly float m_MaxAmountOfFuel;
        private float m_CurrentAmountOfFuel;

        public Engine(eFuelType i_FuelType, float i_MaxAmountOfFuel)
        {
            m_FuelType = i_FuelType;
            m_MaxAmountOfFuel = i_MaxAmountOfFuel;
            m_CurrentAmountOfFuel = 0;
        }
        public override float EnergySourcePercentage
        {
            get { return (m_CurrentAmountOfFuel / m_MaxAmountOfFuel) * 100; }
            set
            {
                if(value < 0 || value > 100)
                {
                    throw new ValueRangeException("Engine percentage must be between 0 and 100");
                }
                else
                {
                    m_CurrentAmountOfFuel = (value * m_MaxAmountOfFuel) / 100;
                }
            }
        }

        public void addFuelIfPossible(float i_AmountToAdd, eFuelType i_FuelType)
        {
            if(m_MaxAmountOfFuel < m_CurrentAmountOfFuel + i_AmountToAdd)
            {
                throw new ValueRangeException("Amount to add exceeds maximum fuel capacity");
            }
            else if(i_AmountToAdd < 0)
            {
                throw new ValueRangeException("Amount of fuel to add cannot be negative");
            }
            else if(m_FuelType != i_FuelType)
            {
                throw new ArgumentException("Incorrect fuel type");
            }
            else
            {
                m_CurrentAmountOfFuel += i_AmountToAdd;
            }
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
        }

        public float MaxAmountOfFuel
        {
            get { return m_MaxAmountOfFuel; }
        }

        public float CurrentAmountOfFuel
        {
            get { return m_CurrentAmountOfFuel; }
        }

        public override string GetSpecificEnergySourceDetails()
        {
            return $"Fuel Type: {m_FuelType}\nCurrent Fuel: {m_CurrentAmountOfFuel:F2} / {m_MaxAmountOfFuel:F2} Liters";
        }
    }
}