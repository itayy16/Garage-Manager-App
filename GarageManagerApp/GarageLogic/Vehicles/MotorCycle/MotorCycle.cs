using System.Text;

namespace GarageLogic
{
    internal abstract class MotorCycle : Vehicle
    {
        protected eLicenseType m_LicenseType;
        protected int m_EngineCapacity;
        internal static readonly int sr_NumOfTires = 2;
        internal static readonly float sr_MaxTirePressureCar = 30f;

        internal eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }
        internal int EngineCapacity
        {
            get { return m_EngineCapacity; }
            set { m_EngineCapacity = value; }
        }

        public override string ToString()
        {
            StringBuilder MotorCycleString = new StringBuilder(base.ToString());
            MotorCycleString.AppendLine(string.Format(@"Engine capacity: {0}
License Type: {1}", m_EngineCapacity, m_LicenseType));

            return MotorCycleString.ToString();
        }
    }
}
