namespace GarageLogic
{
    public class MotorCycleDataFromUser
    {
        private float m_AvailableEnergy;
        private float m_CurrentTirePressure;
        private int m_EngineCapacity;
        private eLicenseType m_LicenseType;

        public float AvailableEnergy
        {
            get { return m_AvailableEnergy; }
            set { m_AvailableEnergy = value; }
        }

        public float CurrentTirePressure
        {
            get { return m_CurrentTirePressure; }
            set { m_CurrentTirePressure = value; }
        }
        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }
        public int EngineCapacity
        {
            get { return m_EngineCapacity; }
            set { m_EngineCapacity = value; }
        }
    }
}
