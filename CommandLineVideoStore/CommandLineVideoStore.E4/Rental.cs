namespace CommandLineVideoStore
{
    public class Rental
    {
        private readonly Movie movie;
        private readonly int daysRented;

        public Rental(Movie movie, int daysRented)
        {
            this.movie = movie;
            this.daysRented = daysRented;
        }

        public decimal GetAmount()
        {
            decimal thisAmount = 0;

            //determine amounts for rental
            switch (movie.Category)
            {
                case "REGULAR":
                    thisAmount += 2;
                    if (daysRented > 2)
                    {
                        thisAmount += (daysRented - 2) * 1.5m;
                    }
                    break;
                case "NEW_RELEASE":
                    thisAmount += daysRented * 3;
                    break;
                case "CHILDRENS":
                    thisAmount += 1.5m;
                    if (daysRented > 3)
                    {
                        thisAmount += (daysRented - 3) * 1.5m;
                    }
                    break;
            }
            return thisAmount;
        }

        public int GetFrequentRenterPoints()
        {
            int frequentRenter1 = 1;
            // add bonus for a two day new release rental
            if (movie.Category.Equals("NEW_RELEASE") && daysRented > 1)
            {
                frequentRenter1++;
            }
            return frequentRenter1;
        }

        public string GetMovieName()
        {
            return movie.Name;
        }
    }
}