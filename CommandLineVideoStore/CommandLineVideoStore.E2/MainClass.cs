using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CommandLineVideoStore
{
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
                string[] rental = input.Split(' ');
                Movie movie = movies[int.Parse(rental[0])];
                decimal thisAmount = 0;

                int daysRented = int.Parse(rental[1]);
                //determine amounts for rental
                switch (movie.Category)
                {
                    case "REGULAR":
                        thisAmount += 2;
                        if (daysRented > 2)
                            thisAmount += (daysRented - 2)*1.5m;
                        break;
                    case "NEW_RELEASE":
                        thisAmount += daysRented*3;
                        break;
                    case "CHILDRENS":
                        thisAmount += 1.5m;
                        if (daysRented > 3)
                            thisAmount += (daysRented - 3)*1.5m;
                        break;
                }

                // add frequent renter points
                frequentRenterPoints++;
                // add bonus for a two day new release rental
                if (movie.Category.Equals("NEW_RELEASE") && daysRented > 1)
                {
                    frequentRenterPoints++;
                }
                // show figures for this rental
                result += "\t" + movie.Name + "\t" + thisAmount.ToString("0.0", CultureInfo.InvariantCulture) + "\n";
                totalAmount += thisAmount;
            }

            // add footer lines
            result += "You owed " + totalAmount.ToString("0.0", CultureInfo.InvariantCulture) + "\n";
            result += "You earned " + frequentRenterPoints + " frequent renter points\n";

            _out.Write(result);
        }
    }
}