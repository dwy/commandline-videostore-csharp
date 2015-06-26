namespace CommandLineVideoStore
{
    public abstract class Rental
    {
        private readonly Movie movie;
        protected readonly int daysRented;

        protected Rental(Movie movie, int daysRented)
        {
            this.movie = movie;
            this.daysRented = daysRented;
        }

        public abstract decimal GetAmount();

        public virtual int GetFrequentRenterPoints()
        {
            return 1;
        }

        public string GetMovieName()
        {
            return movie.Name;
        }
    }
}