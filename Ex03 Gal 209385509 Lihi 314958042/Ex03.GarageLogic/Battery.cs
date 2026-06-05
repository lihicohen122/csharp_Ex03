using System;
namespace Ex03.GarageLogic
{
    public class Battery : EnergySource
    {
        private readonly float m_MaxBatteryHoursCapacity;
        private float m_RemainingBatteryHoursCapacity;

        public Battery(float i_MaxBatteryHoursCapacity)
        {
            m_MaxBatteryHoursCapacity = i_MaxBatteryHoursCapacity;
            m_RemainingBatteryHoursCapacity = 0;
        }
        public override float EnergySourcePercentage
        {
            get { return (m_RemainingBatteryHoursCapacity / m_MaxBatteryHoursCapacity) * 100; }

            set
            {
                if(value < 0 || value > 100)
                {
                    throw new ValueRangeException("Battery percentage must be between 0 and 100");
                }
                else
                {
                    m_RemainingBatteryHoursCapacity = (value * m_MaxBatteryHoursCapacity) / 100;
                }
            }
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

        public float MaxBatteryHoursCapacity
        {
            get { return m_MaxBatteryHoursCapacity; }
        }

        public float RemainingBatteryHoursCapacity
        {
            get { return m_RemainingBatteryHoursCapacity; }
        }

        public override string GetSpecificEnergySourceDetails()
        {
            return $"Battery Capacity: {m_RemainingBatteryHoursCapacity:F2} / {m_MaxBatteryHoursCapacity:F2} Hours";
        }
    }
}