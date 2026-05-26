using System;

namespace Ex03.GarageLogic
{
    public class Battery : EnergySource
    {
        private float m_RemainingBatteryHoursCapacity;
        private readonly float m_MaxBatteryHoursCapacity;

        public void addHoursToBatteryCapacityIfPossible(float i_numberOfHoursToAdd)
        {
            if(m_MaxBatteryHoursCapacity < m_RemainingBatteryHoursCapacity + i_numberOfHoursToAdd)
            {
                
            }
            else
            {
                m_RemainingBatteryHoursCapacity += i_numberOfHoursToAdd;
            }
        }
    }
}