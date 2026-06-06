namespace Ex03.GarageLogic
{
    public class Battery : EnergySource
    {
        private readonly float r_MaxBatteryHoursCapacity;
        private float m_RemainingBatteryHoursCapacity;

        public Battery(float i_MaxBatteryHoursCapacity)
        {
            r_MaxBatteryHoursCapacity = i_MaxBatteryHoursCapacity;
            m_RemainingBatteryHoursCapacity = 0;
        }

        public override float EnergySourcePercentage
        {
            get
            {
                return (m_RemainingBatteryHoursCapacity / r_MaxBatteryHoursCapacity) * 100;
            }

            set
            {
                if(value < 0 || value > 100)
                {
                    throw new ValueRangeException("setting battery charge parameter", 0, 100);
                }
                
                m_RemainingBatteryHoursCapacity = (value * r_MaxBatteryHoursCapacity) / 100;
            }
        }

        public void AddHoursToBatteryCapacityIfPossible(float i_NumberOfHoursToAdd)
        {
            if(r_MaxBatteryHoursCapacity < m_RemainingBatteryHoursCapacity + i_NumberOfHoursToAdd)
            {
                throw new ValueRangeException("battery remaining capacity (in hours)", 0, r_MaxBatteryHoursCapacity);
            }
            
            m_RemainingBatteryHoursCapacity += i_NumberOfHoursToAdd;
        }

        public override string GetSpecificEnergySourceDetails()
        {
            return $"Battery Capacity: {m_RemainingBatteryHoursCapacity:F2} / {r_MaxBatteryHoursCapacity:F2} Hours";
        }
    }
}