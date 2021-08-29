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
    }
}