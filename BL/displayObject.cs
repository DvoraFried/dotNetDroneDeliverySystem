//using DalObject;
using System.Runtime.CompilerServices;
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DisplayStatoin(int idS)
        {
            lock (DalObj)
            {
                if (!DalObj.returnStationArray().ToList().Any(station => station.Id == idS)) { throw new ObjectDoesntExistsInListException("station"); }
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~ station data ~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine(ConvertToBL.ConvertToStationBL(DalObj.returnStation(idS)).ToString());
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DisplayDrone(int idD)
        {
            lock (DalObj)
            {
                if (!DronesListBL.Any(drone => drone.Id == idD)) { throw new ObjectDoesntExistsInListException("drone"); }
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~ drone data ~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine(DronesListBL.First(drone => drone.Id == idD).ToString());
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DisplayCustomer(int idC)
        {
            lock (DalObj)
            {
                if (!DalObj.returnCustomerArray().ToList().Any(customer => customer.Id == idC)) { throw new ObjectDoesntExistsInListException("customer"); }
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~ customer data ~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine(ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(idC)).ToString());
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DisplayParcel(int idP)
        {
            lock (DalObj)
            {
                if (!DalObj.returnParcelArray().ToList().Any(parcel => parcel.Id == idP)) { throw new ObjectDoesntExistsInListException("parcel"); }
                BO.Parcel parcel = ConvertToBL.ConvertToParcelBL(DalObj.returnParcel(idP));
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~ parcel data ~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine(parcel.ToString());
                if (parcel.DroneIdBL != null) { Console.WriteLine("In Drone: " + parcel.DroneIdBL.ToString()); }
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DisplayStatoinList()
        {
            lock (DalObj)
            {
                foreach (DO.Station station in DalObj.returnStationArray())
                {
                    Console.WriteLine(new StationToList(ConvertToBL.ConvertToStationBL(station)).ToString());
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DisplayDroneList()
        {
            lock (DalObj)
            {
                foreach (BO.Drone drone in DronesListBL)
                {
                    Console.WriteLine(new DroneToList(DalObj, drone).ToString());
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DisplayCustomerList()
        {
            lock (DalObj)
            {
                foreach (DO.Customer customer in DalObj.returnCustomerArray())
                {
                    Console.WriteLine(new CustomerToList(DalObj, ConvertToBL.ConvertToCustomrtBL(customer)).ToString());
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DisplayParcelList()
        {
            lock (DalObj)
            {
                foreach (DO.Parcel parcel in DalObj.returnParcelArray())
                {
                    Console.WriteLine(new ParcelToList(DalObj, ConvertToBL.ConvertToParcelBL(parcel)).ToString());
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DisplayParcelsThatHaveNotYetBeenAssociatedWithADrone()
        {
            lock (DalObj)
            {
                foreach (BO.Parcel parcel in ConvertToBL.ConvertToParcelArrayBL(DalObj.returnParcelArray().ToList()))
                {
                    if (parcel.DroneIdBL == null) { Console.WriteLine(new ParcelToList(DalObj, parcel).ToString()); }
                }
            }
        }

    }
}
