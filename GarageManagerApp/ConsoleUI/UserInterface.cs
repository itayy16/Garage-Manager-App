using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GarageLogic;

namespace ConsoleUI
{
    internal class UserInterface
    {
        GarageManager m_Garage;

        internal UserInterface()
        {
            m_Garage = new GarageManager();
        }

        internal void StartApplication()
        {
            while (true)
            {
                PrintMainPage();
                getActionFromUser();
                Console.WriteLine();
                Console.WriteLine("press any key to get back to Main menu");
                Console.ReadLine();
            }
        }

        private void PrintMainPage()
        {
            Console.Clear();
            string msg = string.Format(@"------------------------
    Garage Manager
------------------------
what is your action:
1. Insert a vehicle to the garage
2. Show all license numbers of the cars in the garage
3. Change a vehicle state
4. inflate tire to maximum
5. Fuel a vehicle
6. Charge an electrical vehicle
7. Show full data of a vehicle

enter the number of your choice (1 - 7)");
            Console.WriteLine(msg);
        }

        /// <summary>
        /// This method recieves a valid choice and make the proper action coorsponding to the choice
        /// </summary>
        private void getActionFromUser()
        {
            eActionChoice choice = (eActionChoice)getValidChoice(1,7);
            if (m_Garage.ListIsEmpty() && choice > eActionChoice.DisplayLicenseList)
            {
                Console.WriteLine("You should add new vehicle before choose this option");
            } 
            else
            {
                switch (choice)
                {
                    case eActionChoice.InsertVehicle:
                        insertVehicle();
                        break;
                    case eActionChoice.DisplayLicenseList:
                        displayLicenseList();
                        break;
                    case eActionChoice.ChangeVehicleState:
                        changeVehicleState();
                        break;
                    case eActionChoice.InflateTire:
                        inflateTireToMax();
                        break;
                    case eActionChoice.FuelVehicle:
                        fuelVehicle();
                        break;
                    case eActionChoice.ChargeVehicle:
                        chargeVehicle();
                        break;
                    case eActionChoice.DisplayVehicleDetails:
                        displayVehicleDetails();
                        break;
                    default: return;
                }
            }
        }

        /// <summary>
        /// let the user insert a new vehicle to the garage according to details given by the user
        /// </summary>
        private void insertVehicle()
        {
            Console.Clear();
            string licenceNumber = getLicenseNumberFromUser();

            if (m_Garage.IsVehicleExists(licenceNumber))
            {
                Console.WriteLine("The vehicle already exists, change to in repair mode");
            }
            else // CREATE NEW 
            {
                OwnerDataFromUser ownerData = new OwnerDataFromUser();
                ownerData.OwnerName = getOwnerNameFromuser();
                ownerData.OwnerPhoneNum = getPhoneNumberFromUser();

                VehicleDataFromUser vehicleData = new VehicleDataFromUser();
                vehicleData.LicenseNumber = licenceNumber;
                int vehicleChoice = getVehicleTypeFromUser();
                vehicleData.VehicleType = (eVehicleType)vehicleChoice;

                // is electric relevant only for car or motorcycle
                if (vehicleChoice == 1 || vehicleChoice == 2)
                {
                    vehicleData.isElectric = isElectricFromUser();
                }

                vehicleData.ModelName = getModelNameFromUser();
                float currentEnergy = getAvailableEnergyFromUser(vehicleData);
                float currentTirePressure = getTirePressureFromUser(vehicleData);

                m_Garage.OwnerData = ownerData;
                m_Garage.VehicleData = vehicleData;

                switch (vehicleChoice)
                {
                    case 1: // CAR
                        CarDataFromUser carData = new CarDataFromUser();
                        carData.NumOfDoors = getNumOfDoorsFromUser();
                        carData.CarColor = getCarColorFromUser();
                        carData.AvailableEnergy = currentEnergy;
                        carData.CurrentTirePressure = currentTirePressure;
                        m_Garage.CarData = carData;
                        break;

                    case 2: // MOTORCYCLE
                        MotorCycleDataFromUser motorCycleData = new MotorCycleDataFromUser();
                        motorCycleData.EngineCapacity = getEngineCapacityFromUser();
                        motorCycleData.LicenseType = getLicenseTypeFromUser();
                        motorCycleData.AvailableEnergy = currentEnergy;
                        motorCycleData.CurrentTirePressure = currentTirePressure;
                        m_Garage.MotorCycleData = motorCycleData;
                        break;

                    case 3: // TRUCK
                        TruckDataFromUser truckData = new TruckDataFromUser();
                        truckData.CargoCapacity = getCargoCapacityFromUser();
                        truckData.HazardMaterials = isCarryHazardMaterials();
                        truckData.AvailableEnergy = currentEnergy;
                        truckData.CurrentTirePressure = currentTirePressure;
                        m_Garage.TruckData = truckData;
                        break;
                }
            }

            m_Garage.AddVehicle(licenceNumber);
        }

        /// <summary>
        /// prints to the user a list to all license list of vehicles in the garage
        /// user has the option to choose a flag and print only specific vehicles according to their state
        /// </summary>
        private void displayLicenseList()
        {
            Console.Clear();
            string msg = string.Format(@"Display License List
what is your action:
1. Show all license list
2. Show all license list of vehicles in repair
3. Show all license list of vehicles that has been repaired
4. Show all license list of vehicles that has been paid
enter the number of your choice");
            Console.WriteLine(msg);
            int choice = getValidChoice(1, 4);
            List<string> listToPrint = m_Garage.GetLicenseNumbers(choice - 1);
            Console.Clear();

            if (listToPrint.Count == 0)
            {
                Console.WriteLine("There is no vehicles in that state");
            } else
            {
                printList(listToPrint);
            }
        }

        /// <summary>
        /// asks the user for a license number and the new vehicle state he wants to change 
        /// </summary>
        private void changeVehicleState()
        {
            string licenseNum = getExistingLicenseNumberFromUser();
            eVehicleState state = (eVehicleState)getNewStateFromUser();
            m_Garage.ChangeVehicleState(licenseNum, state);
            Console.WriteLine("The change has been done");
        }

        /// <summary>
        /// asks the user for a license number and inflate the tires of that vehicle to the maximum
        /// </summary>
        private void inflateTireToMax()
        {
            string licenseNum = getExistingLicenseNumberFromUser();
            m_Garage.InflateAirToMax(licenseNum);
            Console.WriteLine("The flate has been done");
        }

        /// <summary>
        /// asks the user for a license number and and the fuel type of the vehicle 
        /// and the amount of fuel he wants to add
        /// </summary>
        private void fuelVehicle()
        {
            string licenseNum = getExistingLicenseNumberFromUser();
            if (m_Garage.IsElectricVehicle(licenseNum))
            {
                Console.WriteLine("Can't fuel an electric vehicle");
            }
            else
            {
                eEnergyType fuelType;
                while (true)
                {
                    try
                    {
                        fuelType = (eEnergyType)getFuelTypeFromUser();
                        m_Garage.IsValidEnergy(licenseNum, fuelType);
                        break;
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                        System.Threading.Thread.Sleep(1500);
                        continue;
                    }
                }

                while (true)
                {
                    try
                    {
                        float amountToAdd = getValidFloatFromUser("How much fuel to add");
                        m_Garage.FuelVehicle(licenseNum, fuelType, amountToAdd);
                        break;
                    }
                    catch (GarageLogic.ValueOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                        System.Threading.Thread.Sleep(1500);
                        continue;
                    }
                }

                Console.WriteLine("Your action has been done");
            }
        }

        /// <summary>
        /// asks the user for a license number and and the amount of fuel he wants to add
        /// </summary>
        private void chargeVehicle()
        {
            string licenseNum = getExistingLicenseNumberFromUser();

            if (!m_Garage.IsElectricVehicle(licenseNum))
            {
                Console.WriteLine("Can't charge an fuel vehicle");
            }
            else
            {
                while (true)
                {
                    try
                    {
                        float amountToAdd = getValidFloatFromUser("How much minutes of charge to add?");
                        m_Garage.ChargeVehicle(licenseNum, amountToAdd);
                        break;
                    }
                    catch (GarageLogic.ValueOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                        System.Threading.Thread.Sleep(1500);
                        continue;
                    }
                }

                Console.WriteLine("Your action has been done");
            }
        }

        /// <summary>
        /// asks the user for a license number and prints a detailed information about the vehicle
        /// </summary>
        private void displayVehicleDetails()
        {
            string licenseNum = getExistingLicenseNumberFromUser();
            string vehicleDetails = m_Garage.GetVehicleInfo(licenseNum);
            StringBuilder msg = new StringBuilder(string.Format(@"---------------------------------
    Vehicle information report 
---------------------------------
"));
            msg.AppendLine(vehicleDetails);
            Console.WriteLine(msg);
        }

        /// <summary>
        /// return a valid float from user and print a first message to console
        /// </summary>
        /// <param name="i_FirstMessage"></param>
        /// <returns></returns>
        private float getValidFloatFromUser(string i_FirstMessage)
        {
            Console.WriteLine(i_FirstMessage);
            string valueToCheck = Console.ReadLine();
            float retVal;

            while (!float.TryParse(valueToCheck, out retVal) || retVal < 0)
            {
                Console.WriteLine("Invalid input, enter again");
                valueToCheck = Console.ReadLine();
            }

            return retVal;
        }

        private int getFuelTypeFromUser()
        {
            Console.Clear();
            string msg = string.Format((@"Which fuel type to fuel?
1. SOLER
2. OCTAN 95
3. OCTAN 96
4. OCTAN 98"));
            Console.WriteLine(msg);
            int retVal = getValidChoice(1, 4);

            return retVal;
        }


        /// <summary>
        /// Reads a valid input of choice from the user
        /// </summary>
        /// <returns> ActionChoice </returns>
        private int getValidChoice(int i_MinValue, int i_MaxValue)
        {
            string userChoice = Console.ReadLine();
            bool isValidLength = userChoice.Length == 1;
            int valueOfNum;
            int.TryParse(userChoice, out valueOfNum);
            bool isValidRange = valueOfNum >= i_MinValue && valueOfNum <= i_MaxValue;

            while (!isValidLength || !isValidRange)
            {
                Console.WriteLine("Invalid choice, enter a valid number");
                userChoice = Console.ReadLine();
                int.TryParse(userChoice, out valueOfNum);
                isValidLength = userChoice.Length == 1;
                isValidRange = valueOfNum >= i_MinValue && valueOfNum <= i_MaxValue;
            }

            return valueOfNum;
        }

        private string getExistingLicenseNumberFromUser()
        {
            string licenseNum = getLicenseNumberFromUser();
            while (!m_Garage.IsVehicleExists(licenseNum))
            {
                Console.WriteLine("The Vehicle does not exists in the garage");
                licenseNum = getLicenseNumberFromUser();
            }
            return licenseNum;
        }

        /// <summary>
        /// check if the current truck carry hazardous materials
        /// </summary>
        /// <returns></returns>
        private bool isCarryHazardMaterials()
        {
            Console.Clear();
            string msg = string.Format((@"Is the truck carry hazardous materials?
1. Yes
2. No"));
            Console.WriteLine(msg);
            int retVal = getValidChoice(1, 2);

            return retVal == 1;
        }

        private void printList(List<string> i_List)
        {
            StringBuilder msg = new StringBuilder();
            foreach (string item in i_List)
            {
                msg.AppendLine(item);
            }
            Console.WriteLine(msg);
        }

        /// <summary>
        /// gets cargo capacity from user, make sure that is positive number
        /// </summary>
        /// <returns></returns>
        private float getCargoCapacityFromUser()
        {
            Console.Clear();
            float retVal = getValidFloatFromUser("What is cargo capacity of the truck?");

            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private eLicenseType getLicenseTypeFromUser()
        {
            Console.Clear();
            string msg = string.Format((@"Which license type the owner have?
1. A
2. A1
3. AA
4. B"));
            Console.WriteLine(msg);
            int retVal = getValidChoice(1, 4);

            return (eLicenseType)retVal;
        }

        /// <summary>
        /// gets engine capacity from user, make sure that is positive number
        /// </summary>
        /// <returns></returns>
        private int getEngineCapacityFromUser()
        {
            Console.Clear();
            Console.WriteLine("What is the engine capacity?");
            string valueToCheck = Console.ReadLine();
            int retVal;

            while(!int.TryParse(valueToCheck, out retVal) || retVal < 0)
            {
                Console.WriteLine("Invalid input, enter again");
                valueToCheck = Console.ReadLine();
            }

            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private eCarColor getCarColorFromUser()
        {
            Console.Clear();
            string msg = string.Format((@"What is the color of the car?
1. Red
2. White
3. Black
4. Silver"));
            Console.WriteLine(msg);
            int retVal = getValidChoice(1, 4);

            return (eCarColor)retVal;
        }

        /// <summary>
        /// check how many doors the current car have
        /// </summary>
        /// <returns></returns>
        private eNumOfDoors getNumOfDoorsFromUser()
        {
            Console.Clear();
            string msg = string.Format((@"How many doors the car have?
1. Two
2. Three
3. Four
4. Five"));
            Console.WriteLine(msg);
            int retVal = getValidChoice(1, 4);

            return (eNumOfDoors)(retVal + 1);
        }
        
        /// <summary>
        /// check if the current vehicle is electric
        /// </summary>
        /// <returns></returns>
        private bool isElectricFromUser()
        {
            Console.Clear();
            string msg = string.Format((@"Is the vehicle electric?
1. Yes
2. No"));
            Console.WriteLine(msg);
            int retVal = getValidChoice(1, 2);

            return retVal == 1;
        }

        /// <summary>
        /// gets tire pressure from user, check special cases
        /// </summary>
        /// <param name="i_VehicleData"></param>
        /// <returns></returns>
        private float getTirePressureFromUser(VehicleDataFromUser i_VehicleData)
        {
            Console.Clear();
            float retVal = getValidFloatFromUser("Insert available amount of tire pressure: ");

            while (!i_VehicleData.IsValidPressure(retVal))
            {
                retVal = getValidFloatFromUser("Invalid tire pressure, enter again");
            }

            return retVal;
        }

        /// <summary>
        ///  gets available amount energy from user, check special cases
        /// </summary>
        /// <param name="i_VehicleData"></param>
        /// <returns></returns>
        private float getAvailableEnergyFromUser(VehicleDataFromUser i_VehicleData)
        {
            Console.Clear();
            float retVal = getValidFloatFromUser("Insert available amount of energy: ");
            
            while (!i_VehicleData.IsValidEnergy(retVal))
            {
                retVal = getValidFloatFromUser("Invalid energy amount, enter again");
            }

            return retVal;
        }

        private string getModelNameFromUser()
        {
            Console.Clear();
            Console.WriteLine("Insert vehicle model: ");
            string retVal = Console.ReadLine();

            while (retVal.Length == 0)
            {
                Console.WriteLine("Model name cant be empty");
                retVal = Console.ReadLine();
            }

            return retVal;
        }

        private int getVehicleTypeFromUser()
        {
            Console.Clear();
            string msg = string.Format((@"what is type of the vehicle:
1. Car
2. Motor cycle
3. Truck"));
            Console.WriteLine(msg);
            int retVal = getValidChoice(1, 3);

            return retVal;
        }

        private string getLicenseNumberFromUser()
        {
            Console.Clear();
            Console.WriteLine("Insert vehicle license number: ");
            string licenseNumber = Console.ReadLine();

            return licenseNumber;
        }

        private string getPhoneNumberFromUser()
        {
            Console.Clear();
            Console.WriteLine("Insert owner's phone number: ");
            string phoneNumber = Console.ReadLine(); 
            
            while(phoneNumber.Length == 0 || !phoneNumber.All(char.IsDigit))
            {
                Console.WriteLine("Invalid input, enter numbers only");
                phoneNumber = Console.ReadLine();
            }

            return phoneNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string getOwnerNameFromuser()
        {
            Console.WriteLine("Insert owner's name: ");
            string ownerName = Console.ReadLine();

            return ownerName;
        }
        private int getNewStateFromUser()
        {
            Console.Clear();
            string msg = string.Format((@"change state of vehicle to:
1. In repair
2. Repaired
3. Paid"));
            Console.WriteLine(msg);
            int retVal = getValidChoice(1, 3);

            return retVal;
        }

    }
}
