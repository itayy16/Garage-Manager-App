namespace GarageLogic
{
    public class OwnerDataFromUser
    {
        private string r_OwnerName;
        private string r_OwnerPhoneNum;
        
        public string OwnerName
        {
            get { return r_OwnerName; }
            set { r_OwnerName = value; }
        }

        public string OwnerPhoneNum
        {
            get { return r_OwnerPhoneNum; }
            set { r_OwnerPhoneNum = value; }
        }
    }
}
