namespace GarageLogic
{
    internal class Tire
    {
        private string m_ManufacturerName = "Michllene";
        private float m_CurrentAirPressure;
        private readonly float m_MaxAirPressure;

        internal Tire(float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxAirPressure = i_MaxAirPressure;
        }

        internal string ManufacturerName
        {
            get { return m_ManufacturerName; }
        }
        internal float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        internal void InflateTire(float i_AddPressure)
        {
            if (m_CurrentAirPressure + i_AddPressure <= m_MaxAirPressure)
            {
                m_CurrentAirPressure += i_AddPressure;
            } 
            else
            {
                throw new System.Exception();
            }
        }

        internal void InflateTireToMax()
        {
            m_CurrentAirPressure = m_MaxAirPressure;
        }

        public override string ToString()
        {
            return string.Format(@"Manufacturer: {0}
Air pressure: {1}",m_ManufacturerName , m_CurrentAirPressure);
        }

    }
}
