namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        private void fillWheelWithAirIfPossible(float i_AirAmountToFill)
        {
            if(MaxAirPressure >= i_AirAmountToFill + CurrentAirPressure)
            {
                m_CurrentAirPressure += i_AirAmountToFill;
            }
            else
            {
                throw new ValueRangeException("wheel air pressure", 0, r_MaxAirPressure);
            }
        }

        public Wheel(string i_ManufacturerName, float i_MaxAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public string ManufacturerName
        {
            get { return r_ManufacturerName; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set
            {
                if(value < 0 || value > r_MaxAirPressure)
                {
                    throw new ValueRangeException("wheel air pressure", 0, r_MaxAirPressure);
                }

                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        public void InflateToMax()
        {
            fillWheelWithAirIfPossible(MaxAirPressure - CurrentAirPressure);
        }
    }
}