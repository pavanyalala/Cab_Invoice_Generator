using CabInvoiceGenerator;
using NUnit.Framework;

namespace CabInvoiceGeneratorTest
{
    public class Tests
    {
        [Test]
        public void GivenDistanceAndTime_WhenAnalyze_ShouldReturnFare()
        {
            InvoiceGenerator invoice = new InvoiceGenerator(RideType.NORMAL);
            double distance = 20;
            int time = 40;
            double fare = invoice.CalculateFare(distance, time);
            double expected = 240;
            Assert.AreEqual(expected, fare);
        }
        [Test]
        public void GivenMultipleRides_WhenAnalyze_ShouldReturnTotalFaresOfMultipleRides()
        {
            InvoiceGenerator invoice = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(10, 40), new Ride(10, 50) };

            InvoiceSummary summary = new InvoiceSummary(2, 290);
            InvoiceSummary expected = invoice.CalculateFare(rides);
            Assert.AreEqual(summary.totalFare, expected.totalFare);
        }
        [Test]
        public void GivenMultipleRides_WhenAnalyze_ShouldReturnAverageFaresOfMultipleRides()
        {
            InvoiceGenerator invoice = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(20, 20), new Ride(10, 10) };

            InvoiceSummary summary = new InvoiceSummary(2, 330);
            InvoiceSummary expected = invoice.CalculateFare(rides);
            Assert.AreEqual(summary.averageFare, expected.averageFare);
        }
        [Test]
        public void GivenUserId_WhenAnalyze_ShouldReturnFareForUser()
        {
            Ride[] ride1 = { new Ride(12, 15), new Ride(10, 20) };
            Ride[] ride2 = { new Ride(5, 10), new Ride(4, 9) };
            Ride[] ride3 = { new Ride(20, 40), new Ride(14, 40) };

            RideRepository rideRepository = new RideRepository();
            rideRepository.AddRides(1, ride1);
            rideRepository.AddRides(2, ride2);
            rideRepository.AddRides(3, ride3);
            var rideArray = rideRepository.GetRides(1);
            InvoiceGenerator invoice = new InvoiceGenerator(RideType.NORMAL);
            InvoiceSummary summary = new InvoiceSummary(2, 255);
            InvoiceSummary expected = invoice.CalculateFare(rideArray);
            Assert.AreEqual(summary.totalFare, expected.totalFare);
        }


        [Test]
        public void GivenUserId_WhenAnalyze_ShouldReturnAverageFareForUser()
        {
            Ride[] ride1 = { new Ride(12, 15), new Ride(10, 20) };
            Ride[] ride2 = { new Ride(5, 10), new Ride(4, 9) };
            Ride[] ride3 = { new Ride(20, 40), new Ride(14, 40) };

            RideRepository rideRepository = new RideRepository();
            rideRepository.AddRides(1, ride1);
            rideRepository.AddRides(2, ride2);
            rideRepository.AddRides(3, ride3);
            var rideArray = rideRepository.GetRides(1);
            InvoiceGenerator invoice = new InvoiceGenerator(RideType.NORMAL);
            InvoiceSummary summary = new InvoiceSummary(2, 255);
            InvoiceSummary expected = invoice.CalculateFare(rideArray);
            Assert.AreEqual(summary.averageFare, expected.averageFare);
        }
    }
}