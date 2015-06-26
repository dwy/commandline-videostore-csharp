namespace CommandLineVideoStore
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    public class MyConsole
    {
        private readonly TextReader @in;
        private readonly TextWriter @out;
        private readonly RentalFactory rentalFactory;

        public MyConsole(TextReader @in, TextWriter @out, RentalFactory rentalFactory)
        {
            this.@in = @in;
            this.@out = @out;
            this.rentalFactory = rentalFactory;
        }

        public void PrintMovies(List<Movie> movies)
        {
            foreach (Movie movie in movies)
            {
                @out.WriteLine(movie.Key + ": " + movie.Name);
            }
        }

        public string InputCustomerName()
        {
            @out.Write("Enter customer name: ");
            string customerName = @in.ReadLine();
            return customerName;
        }

        public List<Rental> InputRentals()
        {
            @out.WriteLine("Choose movie by number followed by rental days, just ENTER for bill:");
            var rentals = new List<Rental>();
            while (true)
            {
                string input = @in.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }
                string[] rentalData = input.Split(' ');
                Rental rental = this.rentalFactory.CreateFrom(rentalData);
                rentals.Add(rental);
            }
            return rentals;
        }

        public void PrintRentalRecord(RentalRecord rentalRecord)
        {
            @out.WriteLine("Rental Record for " + rentalRecord.CustomerName);
            foreach (var rental in rentalRecord.Rentals)
            {
                // show figures for this rental
                @out.WriteLine(
                    "\t" + rental.GetMovieName() + "\t" + rental.GetAmount().ToString("0.0", CultureInfo.InvariantCulture));
            }
        }

        public void PrintFooter(RentalRecord rentalRecord)
        {
            // add footer lines
            @out.WriteLine("You owed " + rentalRecord.GetTotalAmount().ToString("0.0", CultureInfo.InvariantCulture));
            @out.WriteLine("You earned " + rentalRecord.GetFrequentRenterPoints() + " frequent renter points");
        }
    }
}