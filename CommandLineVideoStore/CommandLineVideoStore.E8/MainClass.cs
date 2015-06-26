using System;
using System.Globalization;
using System.IO;

namespace CommandLineVideoStore
{
    using System.Collections.Generic;

    public class MainClass
    {
        private readonly MovieRepository movieRepository;

        private readonly MyConsole console;

        public static void Main()
        {
            new MainClass(Console.In, Console.Out).Run();
            Console.ReadLine();
        }

        public MainClass(TextReader @in, TextWriter @out)
        {
            movieRepository = new MovieRepository();
            var rentalFactory = new RentalFactory(movieRepository);
            console = new MyConsole(@in, @out, rentalFactory);
        }

        public void Run()
        {
            console.PrintMovies(movieRepository.GetAll());

            string customerName = console.InputCustomerName();

            List<Rental> rentals = console.InputRentals();
            var rentalRecord = new RentalRecord(customerName, rentals);
            
            console.PrintRentalRecord(rentalRecord);
            console.PrintFooter(rentalRecord);
        }
    }
}