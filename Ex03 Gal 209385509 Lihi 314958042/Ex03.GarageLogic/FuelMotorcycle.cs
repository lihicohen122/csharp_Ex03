using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle
    {
        private const eFuelType k_FuelType = eFuelType.Octan98;
        private const float k_MaxFuelAmount = 5.6f;
        
        protected override EnergySource CreateEnergySource()
        {
            return new Engine(k_FuelType, k_MaxFuelAmount);
        }
        
        public FuelMotorcycle(string i_LicenseID, string i_ModelName)
        {
            m_LicenseID = i_LicenseID;
            m_ModelName = i_ModelName;
        }
    }
}