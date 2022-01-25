using DalObject;
using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Exceptions;

namespace BL
{
    public partial class BL : BlApi.IBL
    {
        public void DisplayStatoin(int idS)
        {
            if (!DalObj.returnStationArray().ToList().Any(station => station.Id == idS)) { throw new ObjectDoesntExistsInListException("station"); }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~ station data ~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine(ConvertToBL.ConvertToStationBL(DalObj.returnStation(idS)).ToString());
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

        }
        public void DisplayDrone(int idD)
        {
            if (!DronesListBL.Any(drone => drone.getIdBL() == idD)) { throw new ObjectDoesntExistsInListException("drone"); }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~ drone data ~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine(DronesListBL.First(drone => drone.getIdBL() == idD).ToString());
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        public void DisplayCustomer(int idC)
        {
            if (!DalObj.returnCustomerArray().ToList().Any(customer => customer.Id == idC)) { throw new ObjectDoesntExistsInListException("customer"); }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~ customer data ~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine(ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(idC)).ToString());
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        public void DisplayParcel(int idP)
        {
            if (!DalObj.returnParcelArray().ToList().Any(parcel => parcel.Id == idP)) { throw new ObjectDoesntExistsInListException("parcel"); }
            BO.Parcel parcel = ConvertToBL.ConvertToParcelBL(DalObj.returnParcel(idP));
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~ parcel data ~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine(parcel.ToString());
            if (parcel.DroneIdBL != null) { Console.WriteLine("In Drone: "+parcel.DroneIdBL.ToString()); }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        public void DisplayStatoinList()
        {
            foreach (Station station in DalObj.returnStationArray())
            {
                Console.WriteLine(new StationToList(ConvertToBL.ConvertToStationBL(station)).ToString());
            }
        }
        public void DisplayDroneList()
        {
            foreach(BO.Drone drone in DronesListBL)
            {
                Console.WriteLine(new DroneToList(DalObj, drone).ToString());
            }
        }
        public void DisplayCustomerList()
        {
            foreach (Customer customer in DalObj.returnCustomerArray())
            {
                Console.WriteLine(new CustomerToList(DalObj,ConvertToBL.ConvertToCustomrtBL(customer)).ToString());
            }
        }
        public void DisplayParcelList()
        {
            foreach (DO.Parcel parcel in DalObj.returnParcelArray())
            {
                Console.WriteLine(new ParcelToList(DalObj, ConvertToBL.ConvertToParcelBL(parcel)).ToString());
            }
        }
        public void DisplayParcelsThatHaveNotYetBeenAssociatedWithADrone()
        {
            foreach (BO.Parcel parcel in ConvertToBL.ConvertToParcelArrayBL(DalObj.returnParcelArray().ToList()))
            {
                if (parcel.DroneIdBL == null) { Console.WriteLine(new ParcelToList(DalObj, parcel).ToString()); }
            }
        }

    }
}
