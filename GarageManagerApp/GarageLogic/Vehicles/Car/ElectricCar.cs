using System.Text;

namespace GarageLogic
{
    internal class ElectricCar : Car
    {
        internal static readonly float sr_MaxCharge = 130f;
        private const eEnergyType m_EnergyType = eEnergyType.Electric;

        internal override eEnergyType GetEnergyType()
        {
            return m_EnergyType;
        }

        /// <summary>
        /// ovveride from Vehicle
        /// </summary>
        /// <returns></returns>
        internal override float EnergyAmount()
        {
            return m_EnergyPrecent * sr_MaxCharge;
        }

        internal override float MaxEnergyAmount()
        {
            return sr_MaxCharge;
        }

        internal override void AddEnergy(float i_EnergyToAdd)
        {
            if (i_EnergyToAdd + EnergyAmount() > sr_MaxCharge)
            {
                throw new ValueOutOfRangeException(0, sr_MaxCharge - EnergyAmount());
            }
            else
            {
                m_EnergyPrecent = (i_EnergyToAdd + EnergyAmount()) / sr_MaxCharge;
            }
        }
        public override string ToString()
        {
            StringBuilder electricCarString = new StringBuilder(base.ToString());
            electricCarString.AppendLine(string.Format(@"Battery: {0:0.00}%", m_EnergyPrecent * 100));

            return electricCarString.ToString();
        }
    }
}
