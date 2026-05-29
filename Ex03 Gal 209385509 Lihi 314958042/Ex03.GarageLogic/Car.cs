using System.Collections.Generic;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        protected eCarColor m_CarColor;
        protected eNumberOfCarDoors m_NumberOfDoors;

        protected Car()
        {
            Wheels = new Wheel[5];
        }
    }
}