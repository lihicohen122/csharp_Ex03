using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class Engine : EnergySource
    {
        private readonly eFuelType m_FuelType;
        private float m_CurrentAmountOfFuel;
        private readonly float m_MaxAmountOfFuel;
        
        public void addFuelIfPossible(float i_AmountToAdd, eFuelType i_FuelType)
        {
            if(m_MaxAmountOfFuel < m_CurrentAmountOfFuel + i_AmountToAdd)
            {
                
            }
            else if(m_FuelType != i_FuelType)
            {
                
            }
            else
            {
                m_CurrentAmountOfFuel += i_AmountToAdd;
            }
        }
    }
}