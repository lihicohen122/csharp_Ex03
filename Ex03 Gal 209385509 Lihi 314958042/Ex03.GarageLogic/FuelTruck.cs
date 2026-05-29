using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    internal class FuelTruck : Vehicle
    {
        private bool m_CanDeliverColdCargo;
        private float m_CargoVolume;

        public FuelTruck(string i_LicenseID, string i_ModelName)
        {
            m_LicenseID = i_LicenseID;
            m_ModelName = i_ModelName;
            m_EnergySource = new Engine(eFuelType.Soler, 125f);
            Wheels = new Wheel[14];
        }

        public override void initializeSpecificVehicleProperties(string[] i_VehicleProperties)
        {
            throw new NotImplementedException();
        }
    }
}
