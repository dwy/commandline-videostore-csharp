using System;
using System.Globalization;
using System.IO;

namespace CommandLineVideoStore
{
    using System.Collections.Generic;
    using System.Reflection.Emit;

    public class MainClass
    {
        private readonly TextReader _in;
        private readonly TextWriter _out;
        private readonly MovieRepository movieRepository = new MovieRepository();


        public static void Main()
        {
            new MainClass(Console.In, Console.Out).Run();
            Console.ReadLine();
        }

        public MainClass(TextReader @in, TextWriter @out)
        {
            _out = @out;
            _in = @in;
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

            decimal totalAmount = 0;
            int frequentRenterPoints = 0;
            string result = "Rental Record for " + customerName + "\n";
            while (true)
            {
                string input = this._in.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }
                string[] rentalData = input.Split(' ');
                var rental = new Rental(this.movieRepository.GetByKey(int.Parse(rentalData[0])), int.Parse(rentalData[1]));

                // add frequent renter points
                frequentRenterPoints += rental.GetFrequentRenterPoints();

                // show figures for this rental
                result += "\t" + rental.GetMovieName() + "\t" + rental.GetAmount().ToString("0.0", CultureInfo.InvariantCulture) + "\n";
                totalAmount += rental.GetAmount();
            }

            // add footer lines
            result += "You owed " + totalAmount.ToString("0.0", CultureInfo.InvariantCulture) + "\n";
            result += "You earned " + frequentRenterPoints + " frequent renter points\n";

            _out.Write(result);
        }
    }
}