namespace CommandLineVideoStore
{
    public class ChildrensRental : Rental
    {
        public ChildrensRental(Movie movie, int daysRented)
            : base(movie, daysRented)
        {
            
        }

        public override decimal GetAmount()
        {
            decimal thisAmount = 1.5m;
            if (this.daysRented > 3)
            {
                thisAmount += (this.daysRented - 3) * 1.5m;
            }
            return thisAmount;
        }
    }
}