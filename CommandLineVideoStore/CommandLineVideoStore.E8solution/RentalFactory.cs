namespace CommandLineVideoStore
{
    using System.IO;

    public class RentalFactory
    {
        private readonly MovieRepository movieRepository;

        public RentalFactory(MovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public Rental CreateFrom(string[] rentalData)
        {
            Movie movie = movieRepository.GetByKey(int.Parse(rentalData[0]));
            int daysRented = int.Parse(rentalData[1]);
            switch (movie.Category)
            {
                case "REGULAR":
                    return new RegularRental(movie, daysRented);
                case "CHILDRENS":
                    return new ChildrensRental(movie, daysRented);
                case "NEW_RELEASE":
                    return new NewReleaseRental(movie, daysRented);
            }
            throw new InvalidDataException("unknown category: " + movie.Category);
        }
    }
}