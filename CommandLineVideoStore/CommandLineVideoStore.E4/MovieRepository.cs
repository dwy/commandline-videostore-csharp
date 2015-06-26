namespace CommandLineVideoStore
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class MovieRepository
    {
        private readonly Dictionary<int, Movie> movies;

        public MovieRepository()
        {
            movies = new Dictionary<int, Movie>();

            using (FileStream fs = File.Open(@"movies.cvs", FileMode.Open, FileAccess.Read))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader reader = new StreamReader(bs))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] movieData = line.Split(';');
                    var movie = new Movie(int.Parse(movieData[0]), movieData[1], movieData[2]);
                    this.movies.Add(movie.Key, movie);
                }
            }
        }

        public List<Movie> GetAll()
        {
            return this.movies.Values.ToList();
        }

        public Movie GetByKey(int key)
        {
            return this.movies[key];
        }
    }
}