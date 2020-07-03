namespace GarageLogic
{
    public class VehicleDataFromUser
    {
        private string m_LicenseNumber;
        private eVehicleType m_VehicleType;
        private string m_ModelName;
        private bool m_isElectric;

        private const float k_MaxTirePressureCar = 32f;
        private const float k_MaxFuelCar = 60f;
        private const float k_MaxChargeCar = 130f;

        private const float k_MaxTirePressureMotorCycle = 30f;
        private const float k_MaxFuelMotorCycle = 7f;
        private const float k_MaxChargeMotorCycle = 80f;

        private const float k_MaxTirePressureTruck = 28f;
        private const float k_MaxFuelTruck = 120f;
        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        public eVehicleType VehicleType
        {
            get { return m_VehicleType; }
            set { m_VehicleType = value; }
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public bool isElectric
        {
            get { return m_isElectric; }
            set { m_isElectric = value; }
        }
        /// <summary>
        /// Check if the air pressure as given by user is valid
        /// </summary>
        /// <param name="i_CurrentAirPressure"></param>
        /// <returns></returns>
        public bool IsValidPressure(float i_CurrentAirPressure)
        {
            bool retVal = i_CurrentAirPressure >= 0;

            switch (m_VehicleType)
            {
                case eVehicleType.Car: // CAR
                    retVal &= i_CurrentAirPressure <= k_MaxTirePressureCar;
                    break;
                case eVehicleType.MotorCycle: // MOTORCYCLE
                    retVal &= i_CurrentAirPressure <= k_MaxTirePressureMotorCycle;
                    break;
                case eVehicleType.Truck: // TRUCK
                    retVal &= i_CurrentAirPressure <= k_MaxTirePressureTruck;
                    break;
            }

            return retVal;
        }

        /// <summary>
        /// Check if the energy amount as given by user is valid
        /// </summary>
        /// <param name="i_CurrentEnergy"></param>
        /// <returns></returns>
        public bool IsValidEnergy(float i_CurrentEnergy)
        {
            bool retVal = i_CurrentEnergy >= 0;
            float valueToCheck;

            switch (m_VehicleType)
            {
                case eVehicleType.Car: // CAR
                    valueToCheck = m_isElectric ? k_MaxChargeCar : k_MaxFuelCar;
                    retVal &= i_CurrentEnergy <= valueToCheck;
                    break;
                case eVehicleType.MotorCycle: // MOTORCYCLE
                    valueToCheck = m_isElectric ? k_MaxChargeMotorCycle : k_MaxFuelMotorCycle;
                    retVal &= i_CurrentEnergy <= valueToCheck;
                    break;
                case eVehicleType.Truck: // TRUCK
                    retVal &= i_CurrentEnergy <= k_MaxFuelTruck;
                    break;
            }

            return retVal;
        }
    }
}
