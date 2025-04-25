using Microsoft.Extensions.Logging;

namespace World
{
    public class World
    {
        readonly int width = 5000;
        readonly int height = 5000;
        private ILogger log;

        private IDictionary<int, string> playerList;
        private IDictionary<int, Food.Food> foodList;

        public World(ILogger logger) 
        {
            foodList = new Dictionary<int, Food.Food>();
            playerList = new Dictionary<int, string>();
            this.log = logger;
        }

    }
}