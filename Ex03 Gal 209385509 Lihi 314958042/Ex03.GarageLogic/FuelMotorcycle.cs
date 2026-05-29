using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle
    {
        public FuelMotorcycle(string i_LicenseID, string i_ModelName)
        {
            m_LicenseID = i_LicenseID;
            m_ModelName = i_ModelName;
            m_EnergySource = new Engine(eFuelType.Octan98, 5.6f);
        }
    }
}
