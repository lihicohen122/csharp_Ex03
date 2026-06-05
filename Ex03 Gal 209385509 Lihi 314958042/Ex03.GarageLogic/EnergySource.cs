namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        public abstract float EnergySourcePercentage
        {
            get;
            set;
        }

        public abstract string GetSpecificEnergySourceDetails();
    }
}