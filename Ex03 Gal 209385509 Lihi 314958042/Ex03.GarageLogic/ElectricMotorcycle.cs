using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle
    {
        public ElectricMotorcycle(string i_LicenseID, string i_ModelName)
        {
            m_LicenseID = i_LicenseID;
            m_ModelName = i_ModelName;
            m_EnergySource = new Battery(3f);
        }
    }
}
