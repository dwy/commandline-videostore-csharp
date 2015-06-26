namespace CommandLineVideoStore
{
    public class RegularRental : Rental
    {
        public RegularRental(Movie movie, int daysRented):base(movie, daysRented)
        {
            
        }

        public override decimal GetAmount()
        {
            decimal thisAmount = 2;
            if (this.daysRented > 2)
            {
                thisAmount += (this.daysRented - 2) * 1.5m;
            }
            return thisAmount;
        }
    }
}