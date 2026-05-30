using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ValueRangeException : Exception
    {
        private readonly float m_MinValue;
        private readonly float m_MaxValue;
        public ValueRangeException(string message) : base(message)
        {
        }
    }
}