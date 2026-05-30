using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class VehicleOwner
    {
        private readonly string m_OwnerName;
        private readonly string m_OwnerPhoneNumber;
        private readonly Vehicle m_Vehicle;

        public VehicleOwner(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_Vehicle = i_Vehicle;
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
        }
    }
}