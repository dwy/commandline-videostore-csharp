using System;
using System.Globalization;
using System.IO;

namespace CommandLineVideoStore
{
    using System.Collections.Generic;

    public class MainClass
    {
        private readonly TextReader _in;
        private readonly TextWriter _out;
        private readonly MovieRepository movieRepository;

        private readonly RentalFactory rentalFactory;

        public static void Main()
        {
            new MainClass(Console.In, Console.Out).Run();
            Console.ReadLine();
        }

        public MainClass(TextReader @in, TextWriter @out)
        {
            _out = @out;
            _in = @in;
            this.movieRepository = new MovieRepository();
            this.rentalFactory = new RentalFactory(movieRepository);
        }

        public void Run()
        {
            // read movies from file
            foreach (Movie movie in this.movieRepository.GetAll())
            {
                this._out.WriteLine(movie.Key + ": " + movie.Name);
            }

            _out.Write("Enter customer name: ");
            string customerName = _in.ReadLine();

            _out.WriteLine("Choose movie by number followed by rental days, just ENTER for bill:");

            List<Rental> rentals = this.InputRentals();

            int frequentRenterPoints = this.GetFrequentRenterPoints(rentals);
            string result = "Rental Record for " + customerName + "\n";
            decimal totalAmount = this.GetTotalAmount(rentals);

            foreach (var rental in rentals)
            {
                // show figures for this rental
                result += "\t" + rental.GetMovieName() + "\t" + rental.GetAmount().ToString("0.0", CultureInfo.InvariantCulture) + "\n";
            }

            // add footer lines
            result += "You owed " + totalAmount.ToString("0.0", CultureInfo.InvariantCulture) + "\n";
            result += "You earned " + frequentRenterPoints + " frequent renter points\n";

            _out.Write(result);
        }

        private int GetFrequentRenterPoints(List<Rental> rentals)
        {
            int frequentRenterPoints = 0;
            foreach (var rental in rentals)
            {
                // add frequent renter points
                frequentRenterPoints += rental.GetFrequentRenterPoints();
            }
            return frequentRenterPoints;
        }

        private decimal GetTotalAmount(List<Rental> rentals)
        {
            decimal totalAmount = 0;
            foreach (var rental in rentals)
            {
                totalAmount += rental.GetAmount();
            }
            return totalAmount;
        }

        private List<Rental> InputRentals()
        {
            var rentals = new List<Rental>();
            while (true)
            {
                string input = this._in.ReadLine();
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
    }
}