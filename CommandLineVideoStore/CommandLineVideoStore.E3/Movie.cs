namespace CommandLineVideoStore
{
    public class Movie
    {
        private readonly int key;
        private readonly string name;
        private readonly string category;

        public Movie(int key, string name, string category)
        {
            this.key = key;
            this.name = name;
            this.category = category;
        }

        public int Key
        {
            get
            {
                return this.key;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string Category
        {
            get
            {
                return this.category;
            }
        }
    }
}