using System.Text;

namespace GarageLogic
{
    internal abstract class Truck : Vehicle
    {
        protected float m_CargoCapacity;
        protected bool m_HasHazardMaterials;
        internal static readonly int sr_NumOfTires = 16;
        internal static readonly float sr_MaxTirePressureCar = 28f;

        internal float CargoCapacity
        {
            get { return m_CargoCapacity; }
            set { m_CargoCapacity = value; }
        }
        internal bool HasHazardMaterials
        {
            get { return m_HasHazardMaterials; }
            set { m_HasHazardMaterials = value; }
        }
        public override string ToString()
        {
            StringBuilder truckString = new StringBuilder(base.ToString());
            string hazardMat = m_HasHazardMaterials ? "Yes" : "No";
            truckString.AppendLine(string.Format(@"Cargo Capacity: {0}
Driving Hazard Materials: {1}", m_CargoCapacity, hazardMat));

            return truckString.ToString();
        }
    }
}
