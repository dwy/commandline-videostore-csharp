namespace CommandLineVideoStore
{
    public class NewReleaseRental : Rental
    {
        public NewReleaseRental(Movie movie, int daysRented)
            : base(movie, daysRented)
        {
            
        }

        public override int GetFrequentRenterPoints()
        {
            // add bonus for a two day new release rental
            if (daysRented > 1)
            {
                return 2;
            }
            return 1;
        }

        public override decimal GetAmount()
        {
            return this.daysRented * 3;
        }
    }
}