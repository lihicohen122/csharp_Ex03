using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class Engine : EnergySource
    {
        private readonly eFuelType r_FuelType;
        private readonly float r_MaxAmountOfFuel;
        private float m_CurrentAmountOfFuel;

        public Engine(eFuelType i_FuelType, float i_MaxAmountOfFuel)
        {
            r_FuelType = i_FuelType;
            r_MaxAmountOfFuel = i_MaxAmountOfFuel;
            m_CurrentAmountOfFuel = 0;
        }
        public override float EnergySourcePercentage
        {
            get { return (m_CurrentAmountOfFuel / r_MaxAmountOfFuel) * 100; }
            set
            {
                if(value < 0 || value > 100)
                {
                    throw new ValueRangeException("setting fuel parameter", 0, 100);
                }
                else
                {
                    m_CurrentAmountOfFuel = (value * r_MaxAmountOfFuel) / 100;
                }
            }
        }

        public void addFuelIfPossible(float i_AmountToAdd, eFuelType i_FuelType)
        {
            List<string> errorsList = new List<string>();

            if(i_AmountToAdd < 0 || r_MaxAmountOfFuel < m_CurrentAmountOfFuel + i_AmountToAdd)
            {
                errorsList.Add($"Invalid value for fuel filling. The value must be between 0 and {r_MaxAmountOfFuel}.");
            }

            if(r_FuelType != i_FuelType)
            {
                errorsList.Add($"Incorrect fuel type. Expected {r_FuelType}.");
            }

            if(errorsList.Count > 0)
            {
                throw new ArgumentException(string.Join("\n", errorsList));
            }

            m_CurrentAmountOfFuel += i_AmountToAdd;
        }

        public override string GetSpecificEnergySourceDetails()
        {
            return $"Fuel Type: {r_FuelType}\nCurrent Fuel: {m_CurrentAmountOfFuel:F2} / {r_MaxAmountOfFuel:F2} Liters";
        }
    }
}