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

        private ITaxService _taxService; 
        public RentalService(double pricePerHour, double pricePerDay, ITaxService taxService)
        {
            PricePerHour = pricePerHour;
            PricePerDay = pricePerDay;
            _taxService = taxService;
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

            double tax = _taxService.Tax(basicPaymant);

            carRebtal.Invoice = new Invoice(basicPaymant, tax);
        }
    }
}
