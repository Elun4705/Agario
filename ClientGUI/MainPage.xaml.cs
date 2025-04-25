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
/// The following code draws the scence, functions, and client received messages.
/// </summary>

using Communications;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Text;
using Microsoft.UI.Xaml.Documents;
using Models;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Text.Json;
using System.Timers;

namespace ClientGUI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainPage : ContentPage
    {
        private World world;
        private readonly ILogger<MainPage> _logger;

        /// <summary>
        /// This is a constructor method, which uses the _logger to log an informational message using the _logger object.
        /// Moreover initializes the timer to get FPS and the PlaySurface
        /// </summary>
        /// <param name="logger"></param>
        public MainPage(ILogger<MainPage> logger)
        {
            _logger = logger;
            world = new World(_logger);
            InitializeComponent();
            PlaySurface.Drawable = new Scene(world, PlaySurface);
            _logger.LogInformation($"{DateTime.Now} - {Environment.CurrentManagedThreadId} - Infor - Chat Client Constructor \n");
            var timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => TimerCalc();
            timer.Start();

        }

        /// <summary>
        /// Networking constructor
        /// </summary>
        private Networking channel = null;

        /// <summary>
        /// Get the hearbeats per seconds which is the same as FPS
        /// </summary>
        void TimerCalc()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                FPS.Text = "FPS: " + world.heartbeat.ToString();
                world.heartbeat = 0;
            });
        }

        /// <summary>
        /// Get the positiojn of the mouse and adds translation, then displays them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void PointerChanged(object sender, PointerEventArgs args) 
        {
            Point mousePosition = (Point)args.GetPosition((View)sender);

            world.mouseX = (long)mousePosition.X + world.TranslationX;
            world.mouseY = (long)mousePosition.Y + world.TranslationY;

            Direction.Text = "Direction: " + world.mouseX + ", " + world.mouseY;
        }

        /// <summary>
        /// When the player dies, and a object button is pressed this method is called to restart a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void RestartGame(object sender, EventArgs args)
        {
            channel.Send(String.Format(Protocols.CMD_Start_Game, PlayerName.Text));
        }

        /// <summary>
        /// When the player presses the mouse pad or mouse click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void CellSplit(Object sender, TappedEventArgs args)
        {
            channel.Send(String.Format(Protocols.CMD_Split, world.mouseX, world.mouseY));
        }

        /// <summary>
        /// When the player connects to the server a new networking object is created, and the view is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void ClientConnectToServer(object sender, EventArgs args)
        {
            if (PlayerName.Text != "" && PlayerName.Text != null)
            {
                _logger.LogInformation($"{DateTime.Now} - {Environment.CurrentManagedThreadId} - Infor - Attempting to connect to server... \n");
                try
                {
                    channel = new Networking(_logger, onConnection, onDisconnect, onMessage, '\n');
                    channel.ID = PlayerName.Text;
                    world.playerName = PlayerName.Text;
                    channel.Connect(ServerName.Text, 11000);
                    // Starts the game and waits for messeges from the server
                    channel.Send(String.Format(Protocols.CMD_Start_Game, PlayerName.Text));
                    channel.AwaitMessagesAsync(true);

                    Console.ReadLine();

                    PlayerLabel.IsVisible = false;
                    PlayerName.IsVisible = false;
                    ServerName.IsVisible = false;
                    ServerNameLabel.IsVisible = false;
                    ErrorLabel.IsVisible = false;
                    PlaySurface.IsVisible = true;
                    CircleCenter.IsVisible = true;
                    Direction.IsVisible = true;
                    FPS.IsVisible = true;
                    SplitCommand.IsVisible = true;
                }
                catch (Exception)
                {
                    // Displays error message if there is a problem connecting to a server
                    ErrorLabel.IsVisible = true;
                }

            }
        }

        /// <summary>
        /// A delegate method which activates any time a connection has been made between the server and the client
        /// </summary>
        /// <param name="channel"></param>
        private void onConnection(Networking channel)
        {
            _logger.LogInformation($"{DateTime.Now} - {Environment.CurrentManagedThreadId} - Infor - User has connected! \n");
            channel.Send($"Command Name {channel.ID}");
        }

        /// <summary>
        /// A delegate method which activates any time there's a disconnection between a client and server
        /// </summary>
        /// <param name="channel"></param>
        private void onDisconnect(Networking channel)
        {
            _logger.LogInformation($"{DateTime.Now} - {Environment.CurrentManagedThreadId} - Infor - User has Disconnected! \n");
            channel.Disconnect();
            ErrorLabel.IsVisible = true;
        }

        /// <summary>
        /// A delegate method which activates any time a message arrives
        /// Adds the message to the chat history in a new line
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        private void onMessage(Networking channel, string message)
        {
            _logger.LogInformation($"{DateTime.Now} - {Environment.CurrentManagedThreadId} - Infor - Command detected! \n");
            
            // Gets a list of food to display and is stored in the worlds list
            if (message.Contains("Command Food"))
            {
                message = message.Replace("{Command Food}", "");
                List<Food> foods = JsonSerializer.Deserialize<List<Food>>(message);
                Debug.WriteLine($"client receive list of food {foods}");
                lock (foods)
                {
                    foreach (Food food in foods) 
                    {
                        this.world.foodList.Add(food.ID, food);
                    }

                }
            }

            // Gets a list of players to display and is stored in the worlds list
            if (message.Contains("Command Players"))
            {
                message = message.Replace("{Command Players}", "");
                List<Player> players = JsonSerializer.Deserialize<List<Player>>(message);
                Debug.WriteLine($"client receive list of food {players}");
                lock (players)
                {
                    foreach (Player player in players)
                    {
                        if (!world.playerList.ContainsKey(player.ID)) 
                        {
                            this.world.playerList.Add(player.ID, player);
                        }
                        if (world.playerList.ContainsKey(player.ID))
                        {
                            this.world.playerList[player.ID] = player;
                        }
                    }

                }
            }

            // Each heartbeat of the game, we get the location of the player and send 
            if (message.Contains("Command Heartbeat"))
            {
                Player mainPlayer;
                lock (world)
                {
                    world.playerList.TryGetValue(world.playerID, out mainPlayer);

                }
                if (mainPlayer != null) 
                {
                    CircleCenter.Text = "Circle Center: " + mainPlayer.X + ", " + mainPlayer.Y;
                }
                world.heartbeat++;

                channel.Send(String.Format(Protocols.CMD_Move, world.mouseX, world.mouseY));

                PlaySurface.Invalidate();
            }
            // Gets the ID of the player
            if (message.Contains("Command Player Object"))
            {
                message = message.Replace("{Command Player Object}", "");
                world.playerID = JsonSerializer.Deserialize<long>(message);

            }

            // If any players are dead, they are removed from the list to be drawn
            if (message.Contains("Command Dead Players")) 
            {
                message = message.Replace("{Command Dead Players}", "");
                List<long> deadPlayers = JsonSerializer.Deserialize<List<long>>(message);
                Debug.WriteLine($"client receive list of food {deadPlayers}");
                lock (world)
                {
                    foreach (long playerID in deadPlayers)
                    {
                        if (playerID == world.playerID)
                        {
                            Restart.IsVisible = true;
                        }
                        if (world.playerList.ContainsKey(playerID))
                        {
                            this.world.playerList.Remove(playerID);
                        }
                    }

                }
            }

            // If any food is eaten, it's removed from the list to be drawn
            if (message.Contains("Command Eaten Food"))
            {
                message = message.Replace("{Command Eaten Food}", "");
                List<long> eatenFood = JsonSerializer.Deserialize<List<long>>(message);
                Debug.WriteLine($"client receive list of food {eatenFood}");
                lock (world)
                {
                    foreach (long foodID in eatenFood)
                    {
                        if (world.foodList.ContainsKey(foodID))
                        {
                            this.world.foodList.Remove(foodID);
                        }
                    }

                }
            }
        }
    }
}