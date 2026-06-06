namespace Ex03.GarageLogic
{
    internal class ElectricCar : Car
    {
        private const float k_MaxBatteryHoursCapacity = 4.6f;
        
        public ElectricCar(string i_LicenseID, string i_ModelName)
        {
            m_LicenseID = i_LicenseID;
            m_ModelName = i_ModelName;
        }

        protected override EnergySource CreateEnergySource()
        {
            return new Battery(k_MaxBatteryHoursCapacity);
        }
    }
}