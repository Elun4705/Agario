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
/// The following code creates a game object, which contains a the food position, colot, ID and mass.
/// Moreover this is a super class of the food and player class.
/// </summary>

using Newtonsoft.Json;
using System.Drawing;
using System.Numerics;

namespace Models
{
    public abstract class GameObjects
    {
        [JsonProperty(PropertyName ="X")]
        public float X { get; set; }

        [JsonProperty(PropertyName ="Y")]
        public float Y { get; set; }

        [JsonProperty("Position")]
        public Vector2 positionCenter = new Vector2();

        [JsonProperty(PropertyName = "ARGBColor")]
        public int ARGBColor { get; set; }

        [JsonProperty(PropertyName = "Mass")]
        public float Mass { get; set; }

        [JsonProperty("Radius")]
        public float radius { 
            get { return (float)Math.Sqrt(Mass / Math.PI); }
            set { }
        }

        [JsonProperty("Diameter")]
        public float diameter;

        [JsonProperty(PropertyName = "ID")]
        public long ID { get; set; }



        [System.Text.Json.Serialization.JsonConstructor]
        public GameObjects(float X, float Y, int ARGBColor, long iD, float mass)
        {
            this.X = X;
            this.Y = Y;
            this.ARGBColor = ARGBColor;
            ID = iD;
            this.Mass = mass;
        }

    }
}