using System;

namespace Ex03.GarageLogic
{
    public class ValueRangeException : Exception
    {
        private readonly float r_MinValue;
        private readonly float r_MaxValue;

        public ValueRangeException(string i_ErrorSubject, float i_MinValue, float i_MaxValue)
            : base($"Invalid value for {i_ErrorSubject}. The value must be between {i_MinValue} and {i_MaxValue}.")
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
        }
    }
}