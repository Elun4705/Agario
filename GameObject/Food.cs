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
/// The following code creates a food object, which contains a the food position, colot, ID and mass.
/// Moreover this is a child class of the GameObject class.
/// </summary>

using Newtonsoft.Json;
using System.Drawing;
using System.Numerics;

namespace Models
{
    public class Food : GameObjects
    {
        [System.Text.Json.Serialization.JsonConstructor]
        public Food(float X, float Y ,int ARGBColor, long ID, float mass) : base(X, Y, ARGBColor, ID, mass)
        {

            this.ID = ID;
            this.ARGBColor = ARGBColor;
            this.X= X;
            this.Y= Y;
            this.Mass = Mass;
        }


    }
}