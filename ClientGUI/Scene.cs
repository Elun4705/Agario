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
/// The following code draws the world object within the graphics view that is present for the GUI.
/// </summary>

using Microsoft.Maui.Graphics;
using Models;
using System.Diagnostics;
using Windows.UI.Text;

namespace ClientGUI;

/// <summary>
/// 
/// </summary>
public class Scene : IDrawable
{
    /// <summary>
    ///   world model that we have a reference to.
    /// </summary>
    private readonly World world;
    private readonly GraphicsView gv;
    private float xTranslation;
    private float yTranslation;

    /// <summary>
    ///   Create the IDrawable object and save important
    ///   information
    /// </summary>
    /// <param name="worlds"></param>
    /// <param name="gv"> the graphics view - needed so we can call invalidate. </param>
    public Scene(World worlds, GraphicsView gv)
    {
        this.world = worlds;
        this.gv = gv;
    }

    /// <summary>
    ///   <para>
    ///     Basic Draw Scene Method that is executed when the 
    ///     GUI is invalidated.
    ///   </para>
    ///   <para>
    ///     Change method name to DrawOld when we move to the second part of the lab.
    ///   </para>  
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="dirtyRect"></param>
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {

        Debug.WriteLine($"({Environment.CurrentManagedThreadId}) RePainting the GUI");

        // Fills in the background for the Graphics View
        canvas.FillColor = Colors.Gray;
        canvas.FillRectangle(0, 0, world.width, world.height);

        // Aquires player to the gain translation
        Player mainPlayer;
        world.playerList.TryGetValue(world.playerID, out mainPlayer);
        if (mainPlayer != null) 
        {
            translate(mainPlayer.X, mainPlayer.Y, 250, 250, out xTranslation, out yTranslation);

            // Coordinates of translation to be stored in the world to calculate mouse coordinates
            if (world.TranslationX != (long)xTranslation)
            {
                world.TranslationX = (long)xTranslation;
            }

            if (world.TranslationY != (long)yTranslation)
            {
                world.TranslationY = (long)yTranslation;
            }

            // If zooming takes plays the follwing runs
            if (world.zoomPlayer == false)
            {
                // Draws food and player items based on storedlist coordinates and translation coordinates
                lock (world)
                {
                    foreach (KeyValuePair<long, Food> food in world.foodList)
                    {
                        canvas.FillColor = Color.FromInt(food.Value.ARGBColor);
                        canvas.FillCircle(food.Value.X - xTranslation, food.Value.Y - yTranslation, food.Value.radius);
                    }

                    foreach (var player in world.playerList)
                    {
                        canvas.FillColor = Color.FromInt(player.Value.ARGBColor);
                        canvas.FillCircle(player.Value.X - xTranslation, player.Value.Y - yTranslation, player.Value.radius);
                        canvas.StrokeColor = Colors.Black;
                        if (player.Value.ID == world.playerID)
                        {
                            canvas.DrawString(world.playerName, player.Value.X - xTranslation, player.Value.Y - yTranslation + 20, HorizontalAlignment.Center);
                            world.previousMass = mainPlayer.Mass;
                            if (player.Value.Mass >= 31220)
                            {
                                world.zoomPlayer = true;
                            }
                        }
                    }

                }
            }

            if (world.zoomPlayer == true)
            {
                lock (world)
                {

                    // The same as above however, this changes the radius in accordance to the growth of the player
                    float foodRadius = 0;
                    foreach (KeyValuePair<long, Food> food in world.foodList)
                    {
                        canvas.FillColor = Color.FromInt(food.Value.ARGBColor);
                        canvas.FillCircle(food.Value.X - xTranslation, food.Value.Y - yTranslation, food.Value.radius-2);
                        foodRadius = food.Value.radius - 2;
                    }

                    foreach (var player in world.playerList)
                    {
                        canvas.FillColor = Color.FromInt(player.Value.ARGBColor);
                        float newRadius = player.Value.radius - world.zoomReduction;
                        if (newRadius < foodRadius) 
                        {
                            newRadius = foodRadius;
                        }
                        canvas.FillCircle(player.Value.X - xTranslation, player.Value.Y - yTranslation, newRadius);
                        canvas.StrokeColor = Colors.Black;
                        if (player.Value.ID == world.playerID)
                        {
                            canvas.DrawString(world.playerName, player.Value.X - xTranslation, player.Value.Y - yTranslation + 20, HorizontalAlignment.Center);
                            world.previousMass = mainPlayer.Mass;
                            if (player.Value.Mass >= 31220)
                            {
                                if (player.Value.Mass > world.previousMass)
                                {
                                    world.zoomReduction = world.zoomReduction + 10;
                                }
                            }
                        }
                    }
                }
            }
        }

        world.heartbeat++;
        gv.Invalidate();
    }
    private void translate(float playerX, float playerY, float screenX, float screenY, out float translateX, out float translateY)
    {
        translateX = playerX - screenX;
        translateY = playerY - screenY;
    }

}
