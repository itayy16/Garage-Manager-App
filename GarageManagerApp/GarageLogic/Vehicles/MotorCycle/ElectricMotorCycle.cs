using System.Text;

namespace GarageLogic
{
    internal class ElectricMotorCycle : MotorCycle
    {
        private const float k_MaxChargeMotorCycle = 80f;
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
            return m_EnergyPrecent * k_MaxChargeMotorCycle;
        }

        internal override float MaxEnergyAmount()
        {
            return k_MaxChargeMotorCycle;
        }

        /// <summary>
        /// Add energy
        /// </summary>
        /// <param name="i_EnergyToAdd"></param>
        internal override void AddEnergy(float i_EnergyToAdd)
        {
            if (i_EnergyToAdd + EnergyAmount() > k_MaxChargeMotorCycle)
            {
                throw new ValueOutOfRangeException(0, k_MaxChargeMotorCycle - EnergyAmount());
            }
            else
            {
                m_EnergyPrecent = (i_EnergyToAdd + EnergyAmount()) / k_MaxChargeMotorCycle;
            }
        }

        public override string ToString()
        {
            StringBuilder electricMotorCycleString = new StringBuilder(base.ToString());
            electricMotorCycleString.AppendLine(string.Format(@"Battery: {0:0.00}%", m_EnergyPrecent*100));

            return electricMotorCycleString.ToString();
        }

    }
}
