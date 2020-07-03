using System;
using System.Collections.Generic;

namespace GarageLogic
{
    public class GarageManager
    {
        private Dictionary<string, Vehicle> m_ListOfVehicle;
        private Dictionary<string, Customer> m_ListOfCustomers;

        private OwnerDataFromUser m_OwnerData;
        private VehicleDataFromUser m_VehicleData;
        private CarDataFromUser m_CarData;
        private MotorCycleDataFromUser m_MotorCycleData;
        private TruckDataFromUser m_TruckData;

        public GarageManager()
        {
            m_ListOfVehicle = new Dictionary<string, Vehicle>();
            m_ListOfCustomers = new Dictionary<string, Customer>();
        }

        public OwnerDataFromUser OwnerData
        {
            set { m_OwnerData = value; }
        }
        public VehicleDataFromUser VehicleData
        {
            set { m_VehicleData = value; }
        }
        public CarDataFromUser CarData
        {
            set { m_CarData = value; }
        }
        public MotorCycleDataFromUser MotorCycleData
        {
            set { m_MotorCycleData = value; }
        }
        public TruckDataFromUser TruckData
        {
            set { m_TruckData = value; }
        }

        /// <summary>
        /// add new vehicle to the vehicles list, if is already exist change to in repair mode
        /// </summary>
        /// <param name="i_LicenseNumber"></param>
        /// <returns>  </returns>
        public void AddVehicle(string i_LicenseNumber)
        {
            if (IsVehicleExists(i_LicenseNumber))
            {
                ChangeVehicleState(i_LicenseNumber, eVehicleState.InRepair);
            } 
            else
            {
                Vehicle vehicle = VehicleFactory.CreateVehicle(m_VehicleData.VehicleType, m_VehicleData.isElectric);
                fillVehicleWithData(vehicle);
                m_ListOfVehicle.Add(i_LicenseNumber, vehicle);
                Customer currentCustomer = new Customer(m_OwnerData.OwnerName, m_OwnerData.OwnerPhoneNum, eVehicleState.InRepair);
                m_ListOfCustomers.Add(i_LicenseNumber, currentCustomer);
                freeDataFromUser();
            }
        }

        /// <summary>
        /// Make sure that all the data from user has been released
        /// </summary>
        private void freeDataFromUser()
        {
            m_OwnerData = null;
            m_VehicleData = null;
            m_CarData = null;
            m_MotorCycleData = null;
            m_TruckData = null;
        }
        /// <summary>
        /// Fill data in the proper vehicle coorisponding to the actual type of the vehicle
        /// </summary>
        /// <param name="i_Vehicle"></param>
        private void fillVehicleWithData(Vehicle i_Vehicle)
        {
            i_Vehicle.LicenseNumber = m_VehicleData.LicenseNumber;
            i_Vehicle.ModelName = m_VehicleData.ModelName;

            if (i_Vehicle is Car)
            {
                ((Car)i_Vehicle).CarColor = m_CarData.CarColor;
                ((Car)i_Vehicle).NumOfDoors = m_CarData.NumOfDoors;
                ((Car)i_Vehicle).SetTires(m_CarData.CurrentTirePressure, Car.sr_MaxTirePressureCar, Car.sr_NumOfTires);

                if (i_Vehicle is FuelCar)
                {
                    i_Vehicle.EnergyPrecent = m_CarData.AvailableEnergy / FuelCar.sr_MaxFuel;
                }
                else
                {
                    i_Vehicle.EnergyPrecent = m_CarData.AvailableEnergy / ElectricCar.sr_MaxCharge;

                }
            }
            else if (i_Vehicle is MotorCycle)
            {
                ((MotorCycle)i_Vehicle).LicenseType = m_MotorCycleData.LicenseType;
                ((MotorCycle)i_Vehicle).EngineCapacity = m_MotorCycleData.EngineCapacity;
                ((MotorCycle)i_Vehicle).SetTires(m_MotorCycleData.CurrentTirePressure, MotorCycle.sr_MaxTirePressureCar, MotorCycle.sr_NumOfTires);

                if (i_Vehicle is FuelMotorCycle)
                {
                    i_Vehicle.EnergyPrecent = m_MotorCycleData.AvailableEnergy / FuelMotorCycle.sr_MaxFuel;
                }
                else
                {
                    i_Vehicle.EnergyPrecent = m_MotorCycleData.AvailableEnergy / ElectricCar.sr_MaxCharge;

                }
            }
            else if (i_Vehicle is Truck)
            {
                ((Truck)i_Vehicle).CargoCapacity = m_TruckData.CargoCapacity;
                ((Truck)i_Vehicle).HasHazardMaterials = m_TruckData.HazardMaterials;
                ((Truck)i_Vehicle).SetTires(m_TruckData.CurrentTirePressure, Truck.sr_MaxTirePressureCar, Truck.sr_NumOfTires);

                if (i_Vehicle is FuelTruck)
                {
                    i_Vehicle.EnergyPrecent = m_TruckData.AvailableEnergy / FuelTruck.sr_MaxFuel;
                }
            }
        }

        /// <summary>
        /// if i_State = 0 - no flag
        /// if i_State = 1 - flag of vehicles in repair 
        /// if i_State = 2 - flag of vehicles that have been repaired
        /// if i_State = 3 - flag of vehicles that have been paid
        /// </summary>
        /// <param name="i_State"></param>
        /// <param name="i_Flag"></param>
        /// <returns></returns>
        public List<string> GetLicenseNumbers(int i_State)
        {
            List<string> retList = new List<string>();
            
            // prints all list
            if (i_State == 0)
            {
                foreach (KeyValuePair<string,Vehicle> item in m_ListOfVehicle)
                {
                    retList.Add(item.Key);
                }
            }
            else 
            {
                // invalid input
                if (i_State < (int)eVehicleState.InRepair || i_State > (int)eVehicleState.Paid)
                {
                    throw new ValueOutOfRangeException((int)eVehicleState.InRepair, (int)eVehicleState.Paid);
                }

                eVehicleState state = (eVehicleState)i_State;

                foreach (KeyValuePair<string, Customer> item in m_ListOfCustomers)
                {
                    if (item.Value.VehicleState == state)
                    {
                        retList.Add(item.Key);
                    }
                }
            } 
            
            return retList;
        }

        /// <summary>
        /// return true value if the vehicle is in the garage,
        /// return false if not
        /// </summary>
        /// <param name="i_LicenseNumber"></param>
        /// <param name="i_State"></param>
        /// <returns></returns>
        public void ChangeVehicleState(string i_LicenseNumber, eVehicleState i_State)
        {
            Customer customer;
            m_ListOfCustomers.TryGetValue(i_LicenseNumber, out customer);
            customer.VehicleState = i_State;
        }

        public void InflateAirToMax(string i_LicenseNumber)
        {
            Vehicle vehicle;
            m_ListOfVehicle.TryGetValue(i_LicenseNumber, out vehicle);
            vehicle.InflateAllTiresToMax();
        }

        public void FuelVehicle(string i_LicenseNumber, eEnergyType i_FuelType, float i_AmountToFuel)
        {
            Vehicle vehicle;
            m_ListOfVehicle.TryGetValue(i_LicenseNumber, out vehicle);
            if (vehicle.GetEnergyType() != i_FuelType)
            {
                throw new ArgumentException("unmatching type of energy");
            } 
            vehicle.AddEnergy(i_AmountToFuel);
        }

        public void ChargeVehicle(string i_LicenseNumber, float i_AmountToCharge)
        {
            Vehicle vehicle;
            m_ListOfVehicle.TryGetValue(i_LicenseNumber, out vehicle);
            vehicle.AddEnergy(i_AmountToCharge);
        }

        public string GetVehicleInfo(string i_LicenseNumber)
        {
            Customer customer;
            m_ListOfCustomers.TryGetValue(i_LicenseNumber, out customer);
            Vehicle vehicle;
            m_ListOfVehicle.TryGetValue(i_LicenseNumber, out vehicle);
            string retVal = string.Format(@"{0}
{1}", customer.ToString(), vehicle.ToString());

            return retVal;
        }

        public void IsValidEnergy(string i_LicenseNumber, eEnergyType i_EnergyType)
        {
            Vehicle vehicle;
            m_ListOfVehicle.TryGetValue(i_LicenseNumber, out vehicle);
            if (vehicle.GetEnergyType() != i_EnergyType)
            {
                throw new ArgumentException("Unmatching type of energy");
            }
        }
        
        public bool IsVehicleExists(string i_LicenceNumber)
        {
            return m_ListOfVehicle.ContainsKey(i_LicenceNumber);
        }

        public bool ListIsEmpty()
        {
            return m_ListOfVehicle.Count == 0;
        }

        public bool IsElectricVehicle(string i_LicenceNumber)
        {
            Vehicle vehicle;
            m_ListOfVehicle.TryGetValue(i_LicenceNumber, out vehicle);

            return vehicle.GetEnergyType() == eEnergyType.Electric;
        }
    }
}
