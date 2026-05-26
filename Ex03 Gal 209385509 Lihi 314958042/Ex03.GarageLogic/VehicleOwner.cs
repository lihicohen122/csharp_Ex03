using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class VehicleOwner
    {
        private readonly string m_OwnerName;
        private readonly string m_OwnerPhoneNumber;
        private readonly Vehicle m_Vehicle;
        private eCarState m_CarState;

        public VehicleOwner(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_CarState = eCarState.InRepair;
        }
    }
}