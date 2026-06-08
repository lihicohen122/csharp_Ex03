namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxBatteryHoursCapacity = 3f;
        
        protected override EnergySource CreateEnergySource()
        {
            return new Battery(k_MaxBatteryHoursCapacity);
        }
        
        public ElectricMotorcycle(string i_LicenseID, string i_ModelName)
        {
            m_LicenseID = i_LicenseID;
            m_ModelName = i_ModelName;
        }
    }
}