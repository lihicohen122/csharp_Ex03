using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicenseID;
        protected float m_EnergyPercentageTracker;
        protected List<Wheel> m_Wheels;
        protected EnergySource m_EnergySource;
    }
}
