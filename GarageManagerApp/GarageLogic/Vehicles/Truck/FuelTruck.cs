using System.Text;

namespace GarageLogic
{
    internal class FuelTruck : Truck
    {
        internal static readonly float sr_MaxFuel = 120f;
        private const eEnergyType m_EnergyType = eEnergyType.Soler;

        internal override eEnergyType GetEnergyType()
        {
            return m_EnergyType;
        }

        internal override float EnergyAmount()
        {
            return m_EnergyPrecent * sr_MaxFuel;
        }

        internal override float MaxEnergyAmount()
        {
            return sr_MaxFuel;
        }

        internal override void AddEnergy(float i_EnergyToAdd)
        {
            if (i_EnergyToAdd + EnergyAmount() > sr_MaxFuel)
            {
                throw new ValueOutOfRangeException(k_MinValToAdd, sr_MaxFuel - EnergyAmount());
            }
            else
            {
                m_EnergyPrecent = (i_EnergyToAdd + EnergyAmount()) / sr_MaxFuel;
            }
        }
        public override string ToString()
        {
            StringBuilder fuelTruckString = new StringBuilder(base.ToString());
            fuelTruckString.AppendLine(string.Format(@"Fuel Type: {0}
Fuel Tank: {1:0.00}%", m_EnergyType, m_EnergyPrecent * 100));

            return fuelTruckString.ToString();
        }
    }
}
