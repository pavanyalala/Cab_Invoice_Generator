using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    public class InvoiceGenerator
    {
        readonly RideType rideType;
        private readonly double COST_PER_MIN;
        private readonly double COST_PER_KM;
        private readonly double MINIMUM_FARE;

        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
            try
            {
                if (rideType.Equals(RideType.NORMAL))
                {
                    this.rideType = rideType;
                    this.COST_PER_KM = 10;
                    this.COST_PER_MIN = 1;
                    this.MINIMUM_FARE = 5;
                }
            }
            catch (CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
            }
        }


        public double CalculateFare(double distance, double time)
        {
            double fare = 0;
            try
            {
                fare = (distance * COST_PER_KM) + (time * COST_PER_MIN);

            }
            catch (CabInvoiceException)
            {
                if (rideType.Equals(null))
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
                }
                if (distance <= 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
                }
                if (time <= 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_TIME, "Invalid Time");
                }
            }
            return fare >= MINIMUM_FARE ? fare : MINIMUM_FARE;
        }
        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            try
            {
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "No rides found");
                }
                foreach (Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
            }
            catch (CabInvoiceException)
            {
                Console.WriteLine("");
            }
            return new InvoiceSummary(rides.Length, totalFare);
        }
        public void AddRides(int userId, Ride[] rides)
        {
            RideRepository rideRepository = new RideRepository();
            try
            {
                rideRepository.AddRides(userId, rides);
            }
            catch (CabInvoiceException)
            {
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides are Null");
                }
            }
        }
        public InvoiceSummary GetInvoiceSummary(int userId)
        {
            RideRepository rideRepository = new RideRepository();
            try
            {
                return this.CalculateFare(rideRepository.GetRides(userId));
            }
            catch (CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_USER, "Invalid UserId");
            }
        }
    }
}
