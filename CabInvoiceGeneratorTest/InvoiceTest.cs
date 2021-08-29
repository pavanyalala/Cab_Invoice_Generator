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
    }
}