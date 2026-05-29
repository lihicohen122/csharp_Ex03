using System;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class Battery : EnergySource
    {
        private readonly float m_MaxBatteryHoursCapacity;
        private float m_RemainingBatteryHoursCapacity;

        public Battery(float i_MaxBatteryHoursCapacity)
        {
            m_MaxBatteryHoursCapacity = i_MaxBatteryHoursCapacity;
            m_RemainingBatteryHoursCapacity = 0; // Subject to change (?)
        }
        public override float EnergySourcePercentage
        {
            get { return (m_RemainingBatteryHoursCapacity / m_MaxBatteryHoursCapacity) * 100; }
        }

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