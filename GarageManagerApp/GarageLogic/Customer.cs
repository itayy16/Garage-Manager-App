using System.Deployment.Internal;

namespace GarageLogic
{
    internal class Customer
    {
        private string m_CustomerName;
        private string m_CustomerPhoneNum;
        private eVehicleState m_VehicleState;

        internal Customer(string i_Name, string i_PhoneNum, eVehicleState i_State)
        {
            m_CustomerName = i_Name;
            m_CustomerPhoneNum = i_PhoneNum;
            m_VehicleState = i_State;
        }
        internal string CustomerName
        {
            get { return m_CustomerName; }
        }

        internal string CustomerPhoneNum
        {
            get { return m_CustomerPhoneNum; }
        }

        internal eVehicleState VehicleState
        {
            get { return m_VehicleState; }
            set { m_VehicleState = value; }
        }

        public override string ToString()
        {
            string vehicleState = (m_VehicleState == eVehicleState.InRepair) ? "In Repair" : m_VehicleState.ToString();
            string retVal = string.Format(@"Customer Name: {0}
Customer Phone Number: {1}
Vehicle state: {2}", m_CustomerName, m_CustomerPhoneNum, vehicleState);

            return retVal;
        }
    }
}
