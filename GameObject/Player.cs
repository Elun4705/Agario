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
/// The following code creates a player object, which contains a the food position, colot, ID and mass.
/// Moreover this is a child class of the GameObject class.
/// </summary>

using Newtonsoft.Json;
using System.Drawing;
using System.Numerics;
using System.Threading;

namespace Models
{
    public class Player : GameObjects
    {
        Random generator = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="ARGBColor"></param>
        /// <param name="iD"></param>
        /// <param name="mass"></param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Player(float X, float Y, int ARGBColor, long iD, float mass) : base(X, Y, ARGBColor, iD, mass)
        {
            this.X = X; 
            this.Y = Y;
            this.ID = iD;
            this.ARGBColor = ARGBColor;
            this.Mass = mass;
        }
        

        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
    }
}