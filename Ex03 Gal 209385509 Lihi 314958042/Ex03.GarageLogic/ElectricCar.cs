using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    internal class ElectricCar : Car
    {
        public ElectricCar(string i_LicenseID, string i_ModelName)
        {
            m_LicenseID = i_LicenseID;
            m_ModelName = i_ModelName;
        }

        protected override EnergySource CreateEnergySource()
        {
            return new Battery(4.6f);
        }
    }
}