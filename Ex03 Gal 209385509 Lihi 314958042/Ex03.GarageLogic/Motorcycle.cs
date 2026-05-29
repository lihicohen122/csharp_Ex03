using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        protected eLicenseType m_LicenseType;
        protected int m_EngineVolume;

        protected Motorcycle()
        {
            Wheels = new Wheel[2];
        }
    }
}