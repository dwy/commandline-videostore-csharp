namespace CommandLineVideoStore
{
    using System.Collections.Generic;

    public class RentalRecord
    {
        private readonly string customerName;
        private readonly List<Rental> rentals;

        public RentalRecord(string customerName, List<Rental> rentals)
        {
            this.customerName = customerName;
            this.rentals = rentals;
        }

        public string CustomerName
        {
            get
            {
                return this.customerName;
            }
        }

        public List<Rental> Rentals
        {
            get
            {
                return this.rentals;
            }
        }

        public decimal GetTotalAmount()
        {
            decimal totalAmount = 0;
            foreach (var rental in this.Rentals)
            {
                totalAmount += rental.GetAmount();
            }
            return totalAmount;
        }

        public int GetFrequentRenterPoints()
        {
            int frequentRenterPoints = 0;
            foreach (var rental in this.Rentals)
            {
                // add frequent renter points
                frequentRenterPoints += rental.GetFrequentRenterPoints();
            }
            return frequentRenterPoints;
        }
    }
}