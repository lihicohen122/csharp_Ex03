using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    internal class FuelCar : Car
    {
        public FuelCar(string i_LicenseID, string i_ModelName)
        {
            m_LicenseID = i_LicenseID;
            m_ModelName = i_ModelName;
        }

        protected override EnergySource CreateEnergySource()
        {
            return new Engine(eFuelType.Octan95, 51f);
        }
    }
}