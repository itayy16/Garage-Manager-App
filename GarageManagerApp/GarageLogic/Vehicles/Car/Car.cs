using System.Text;

namespace GarageLogic
{
    internal abstract class Car : Vehicle
    {
        protected eCarColor m_CarColor;
        protected eNumOfDoors m_NumOfDoors;
        internal static readonly int sr_NumOfTires = 4;
        internal static readonly float sr_MaxTirePressureCar = 32f;

        internal eNumOfDoors NumOfDoors
        {
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value; }
        }
        internal eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public override string ToString()
        {
            StringBuilder carString = new StringBuilder(base.ToString());
            carString.AppendLine(string.Format(@"Number of Doors: {0}
Car Color: {1}", m_NumOfDoors, m_CarColor));

            return carString.ToString();
        }
    }
}
