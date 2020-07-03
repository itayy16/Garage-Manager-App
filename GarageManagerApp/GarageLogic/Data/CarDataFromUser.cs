namespace GarageLogic
{
    public class CarDataFromUser
    {
        private float m_AvailableEnergy;
        private float m_CurrentTirePressure;
        private eNumOfDoors m_NumOfDoors;
        private eCarColor m_CarColor;

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
        public eNumOfDoors NumOfDoors
        {
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value; }
        }
        public eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }
    }
}
