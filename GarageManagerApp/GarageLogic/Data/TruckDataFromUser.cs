namespace GarageLogic
{
    public class TruckDataFromUser
    {

        private float m_AvailableEnergy;
        private float m_CurrentTirePressure;
        private bool m_HazardMaterials;
        private float m_CargoCapacity;

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
        public float CargoCapacity
        {
            get { return m_CargoCapacity; }
            set { m_CargoCapacity = value; }
        }
        public bool HazardMaterials
        {
            get { return m_HazardMaterials; }
            set { m_HazardMaterials = value; }
        }
    }
}
