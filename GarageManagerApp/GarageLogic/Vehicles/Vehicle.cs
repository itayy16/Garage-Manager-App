using System.Text;

namespace GarageLogic
{
    internal abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected float m_EnergyPrecent;
        protected Tire[] m_Tires;
        protected const float k_MinValToAdd = 0;

        internal string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        internal string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        internal float EnergyPrecent
        {
            get { return m_EnergyPrecent; }
            set { m_EnergyPrecent = value; }
        }

        internal void SetTires(float i_TirePressure, float i_MaxPressure ,int i_NumOfTires)
        {
            m_Tires = new Tire[i_NumOfTires];

            for (int i = 0; i < i_NumOfTires; i++)
            {
                m_Tires[i] = new Tire(i_TirePressure, i_MaxPressure);
            }
        }

        internal void InflateAllTiresToMax()
        {
            for (int i = 0; i < m_Tires.Length; i++)
            {
                m_Tires[i].InflateTireToMax();
            }
        }

        internal abstract float EnergyAmount();

        internal abstract float MaxEnergyAmount();

        internal abstract void AddEnergy(float i_EnergyToAdd);

        internal abstract eEnergyType GetEnergyType();

        public override string ToString()
        {
            StringBuilder vehicleString = new StringBuilder();

            vehicleString.AppendLine(string.Format(@"License Number: {0}
Model Name: {1}", m_LicenseNumber, m_ModelName));

            for (int i = 0; i < m_Tires.Length; i++)
            {
                vehicleString.AppendLine(string.Format(@"Tire {0}:
{1}", (i + 1) , m_Tires[i].ToString()));
            }

            return vehicleString.ToString();
        }
    }
}
