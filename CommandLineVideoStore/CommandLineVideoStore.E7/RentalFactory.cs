namespace CommandLineVideoStore
{
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
            var rental = new Rental(movie, daysRented);
            return rental;
        }
    }
}