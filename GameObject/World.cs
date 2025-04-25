// Version 1.1 (9/22/13 11:45 a.m.) Submittion for assigment
// (Emmanuel Luna)

/// <summary>
/// Author:    Emmanuel Luna
/// Partner:   None
/// Date:      4/14/23 8:11 a.m.
/// Course:    CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500 and Emmanuel Luna - This work may not 
///            be copied for use in Academic Coursework.
///
/// I, Emmanuel, certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source.  All 
/// references used in the completion of the assignments are cited 
/// in my README file.
///
/// File Contents
/// The following code creates a world object which keeps track of everything in the world, the food, players, and transalation factors.
/// In addition to the logger to track debugging issues.
/// </summary>

using Microsoft.Extensions.Logging;
using System.Numerics;

namespace Models
{
    public class World
    {
        private ILogger log;

        public int width = 5000;
        public int height = 5000;
        
        public long playerID;
        public string? playerName;

        public IDictionary<long, Player> playerList;
        public IDictionary<long, Food> foodList;

        public long mouseX;
        public long mouseY;

        public long TranslationX;
        public long TranslationY = 0;

        public int heartbeat;

        public bool zoomPlayer = false;
        public float zoomReduction = 0;
        public float previousMass;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public World(ILogger logger) 
        {
            foodList = new Dictionary<long, Food>();
            playerList = new Dictionary<long, Player>();
            this.log = logger;
        }
    }
}