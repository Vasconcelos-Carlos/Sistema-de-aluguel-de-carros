using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Entities;


namespace Interfaces.Services
{
    class RentalService
    {
        public double PricePerHour { get; private set; }
        public double PricePerDay { get; private set; }

        private BrazilTaxService _brazilTaxService = new BrazilTaxService(); 
        public RentalService(double pricePerHour, double pricePerDay)
        {
            PricePerHour = pricePerHour;
            PricePerDay = pricePerDay;
        }

        public void ProcessInvoice(CarRental carRebtal)
        {
            TimeSpan duration = carRebtal.Finish.Subtract(carRebtal.Start);

            double basicPaymant = 0.0;
            if (duration.TotalHours <= 12)
            {
                basicPaymant = PricePerHour * Math.Ceiling(duration.TotalHours);

            }
            else
            {
                basicPaymant = PricePerDay * Math.Ceiling(duration.TotalDays);
            
            }

            double tax = _brazilTaxService.Tax(basicPaymant);

            carRebtal.Invoice = new Invoice(basicPaymant, tax);
        }
    }
}
