namespace Ex03.GarageLogic
{
    public class VehicleOwner
    {
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhoneNumber;
        private readonly Vehicle r_Vehicle;

        public VehicleOwner(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            r_OwnerName = i_OwnerName;
            r_OwnerPhoneNumber = i_OwnerPhoneNumber;
            r_Vehicle = i_Vehicle;
        }

        public Vehicle Vehicle
        {
            get { return r_Vehicle; }
        }

        public string OwnerName
        {
            get { return r_OwnerName; }
        }

        public string OwnerPhoneNumber
        {
            get { return r_OwnerPhoneNumber; }
        }
    }
}