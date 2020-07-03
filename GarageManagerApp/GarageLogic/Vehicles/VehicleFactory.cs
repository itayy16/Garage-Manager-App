namespace GarageLogic
{
    internal class VehicleFactory
    {
        internal static Vehicle CreateVehicle(eVehicleType i_VehicleType, bool i_IsElectric)
        {
            Vehicle retVehicle = null;
            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    if (i_IsElectric)
                    {
                        retVehicle = new ElectricCar();
                    }
                    else
                    {
                        retVehicle = new FuelCar();
                    }
                    break;

                case eVehicleType.MotorCycle:
                    if (i_IsElectric)
                    {
                        retVehicle = new ElectricMotorCycle();
                    }
                    else
                    {
                        retVehicle = new FuelMotorCycle();
                    }
                    break;

                case eVehicleType.Truck:
                    retVehicle = new FuelTruck();
                    break;
            }

            return retVehicle;
        }
    }
}
