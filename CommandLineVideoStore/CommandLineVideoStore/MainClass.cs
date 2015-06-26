using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CommandLineVideoStore
{
    using System.Reflection.Emit;

    public class MainClass
    {
        private readonly TextReader _in;
        private readonly TextWriter _out;

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
            var movies = new Dictionary<int, Movie>();
            using (FileStream fs = File.Open(@"movies.cvs", FileMode.Open, FileAccess.Read))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader reader = new StreamReader(bs))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] movieData = line.Split(';');
                    var movie = new Movie(int.Parse(movieData[0]), movieData[1], movieData[2]);
                    movies.Add(movie.Key, movie);

                    _out.WriteLine(movie.Key + ": " + movie.Name);
                }
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
                var rental = new Rental(movies[int.Parse(rentalData[0])], int.Parse(rentalData[1]));

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