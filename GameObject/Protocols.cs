using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Protocols
    {
        /// <summary>
        ///   <para>
        ///     When a player asks to join a game, the server will create a player
        ///     object for them and return the ID.  This lets the client know
        ///     that the "game is afoot" (has begun).
        ///   </para>
        ///   <para>
        ///     Command statement directly followed by a long:
        ///   </para>
        ///   <para>
        ///     Example: {Command Player Object}5
        ///   </para>
        /// </summary>
        /// 
        public const string CMD_Player_Object  = "{Command Player Object}";// 我们玩家ID

        /// <summary>
        ///   <para>
        ///     Sent when ever a single (or multiple) food needs to be communicated from
        ///     server to client.  
        ///   </para>
        ///   <para>
        ///     Command statement directly followed by a JSON-ized list of food objects.  In the
        ///     case of the initial food, there will be many foods in the array. After the initial food array
        ///     is sent, the arrays will likely only consist of one food object.
        ///   </para>
        ///   <para>
        ///     Example:   {Command Food}[{"X":344,"Y":1216,"ARGBColor":-1977629,"ID":3053,"Mass":50}]
        ///   </para>
        /// </summary>
        public const string CMD_Food           = "{Command Food}";

        /// <summary>
        ///   <para>
        ///     Sent when players die.  
        ///   </para>
        ///   <para>
        ///     Command statement directly followed by an array of integers.
        ///     The numbers represent the IDs of players that have been
        ///     consumed by another player and should be removed from the display.
        ///   </para>
        ///   <para>
        ///     Example: {Command Dead Players}[5,10,20,30,16,121,...]
        ///   </para>
        /// </summary>
        public const string CMD_Dead_Players   = "{Command Dead Players}";

        /// <summary>
        ///   <para>
        ///     Sent when food is eaten.
        ///   </para>
        ///   <para>
        ///     Command statement directly followed by a list of longs (representing the IDs
        ///     of food objects that are no longer "alive" (have been eaten)).
        ///     These should be removed from the visual.
        ///   </para>
        ///   <para>
        ///     Example: {Command Eaten Food}[2701,2546,515,1484,2221,240,1378,1124,1906,1949]
        ///   </para>
        /// </summary>
        public const string CMD_Eaten_Food     = "{Command Eaten Food}";

        /// <summary>
        ///   <para>
        ///     Sent at the end of each complete "game loop".  A game loop is when the
        ///     server moves everyone, generates food, delete foods, etc.  The integer
        ///     is the "heartbeat" count.  Over time this grows.  Ideally 30 per second until
        ///     the end of time (or the server shutsdown).
        ///   </para>
        ///   <para>
        ///     Command statement directly followed by an integer.
        ///   </para>
        ///   <para>
        ///     Example: {Command Player Object}5
        ///   </para>
        /// </summary>
        public const string CMD_HeartBeat      = "{Command Heartbeat}";//只要接受到这个指令，update所有画图的东西

        /// <summary>
        ///   <para>
        ///     At each heartbeat of the gameloop, the server will send the location, mass, etc., 
        ///     of each player in the game.        ///     
        ///   </para>
        ///   <para>
        ///     Command statement directly followed by a JSON-(serialized) list of player objects.
        ///   </para>
        ///   <para>
        ///     Example: {Command Players}[{"Name":"player_0,0","X":0,"Y":0,"ARGBColor":-16777088,"ID":3000,"Mass":2999.726},{"Name":"player_0,1000","X":0,"Y":1000,"ARGBColor":-2987746,"ID":3001,"Mass":2999.726},{"Name":"player_0,2000","X":0,"Y":2000,"ARGBColor":-65536,"ID":3002,"Mass":2999.726},{"Name":"player_0,3000","X":0,"Y":3000,"ARGBColor":-16776961,"ID":3003,"Mass":2999.726},{"Name":"player_0,4000","X":0,"Y":4000,"ARGBColor":-16181,"ID":3004,"Mass":2999.726},{"Name":"player_0,5000","X":0,"Y":5000,"ARGBColor":-8388480,"ID":3005,"Mass":3049.7239},{"Name":"player_1000,0","X":1000,"Y":0,"ARGBColor":-16744448,"ID":3006,"Mass":2999.726},{"Name":"player_1000,1000","X":1000,"Y":1000,"ARGBColor":-16711681,"ID":3007,"Mass":2999.726},{"Name":"player_1000,2000","X":1000,"Y":2000,"ARGBColor":-16777216,"ID":3008,"Mass":2999.726},{"Name":"player_1000,3000","X":1000,"Y":3000,"ARGBColor":-657956,"ID":3009,"Mass":3049.7239},{"Name":"player_1000,4000","X":1000,"Y":4000,"ARGBColor":-3308225,"ID":3010,"Mass":2999.726},{"Name":"player_1000,5000","X":1000,"Y":5000,"ARGBColor":-16777088,"ID":3011,"Mass":2999.726},{"Name":"player_2000,0","X":2000,"Y":0,"ARGBColor":-2987746,"ID":3012,"Mass":2999.726},{"Name":"player_2000,1000","X":2000,"Y":1000,"ARGBColor":-65536,"ID":3013,"Mass":2999.726},{"Name":"player_2000,2000","X":2000,"Y":2000,"ARGBColor":-16776961,"ID":3014,"Mass":2999.726},{"Name":"player_2000,3000","X":2000,"Y":3000,"ARGBColor":-16181,"ID":3015,"Mass":3049.7239},{"Name":"player_2000,4000","X":2000,"Y":4000,"ARGBColor":-8388480,"ID":3016,"Mass":2999.726},{"Name":"player_2000,5000","X":2000,"Y":5000,"ARGBColor":-16744448,"ID":3017,"Mass":2999.726},{"Name":"player_3000,0","X":3000,"Y":0,"ARGBColor":-16711681,"ID":3018,"Mass":2999.726},{"Name":"player_3000,1000","X":3000,"Y":1000,"ARGBColor":-16777216,"ID":3019,"Mass":3149.7195},{"Name":"player_3000,2000","X":3000,"Y":2000,"ARGBColor":-657956,"ID":3020,"Mass":3049.7239},{"Name":"player_3000,3000","X":3000,"Y":3000,"ARGBColor":-3308225,"ID":3021,"Mass":2999.726},{"Name":"player_3000,4000","X":3000,"Y":4000,"ARGBColor":-16777088,"ID":3022,"Mass":2999.726},{"Name":"player_3000,5000","X":3000,"Y":5000,"ARGBColor":-2987746,"ID":3023,"Mass":2999.726},{"Name":"player_4000,0","X":4000,"Y":0,"ARGBColor":-65536,"ID":3024,"Mass":2999.726},{"Name":"player_4000,1000","X":4000,"Y":1000,"ARGBColor":-16776961,"ID":3025,"Mass":3049.7239},{"Name":"player_4000,2000","X":4000,"Y":2000,"ARGBColor":-16181,"ID":3026,"Mass":2999.726},{"Name":"player_4000,3000","X":4000,"Y":3000,"ARGBColor":-8388480,"ID":3027,"Mass":2999.726},{"Name":"player_4000,4000","X":4000,"Y":4000,"ARGBColor":-16744448,"ID":3028,"Mass":3049.7239},{"Name":"player_4000,5000","X":4000,"Y":5000,"ARGBColor":-16711681,"ID":3029,"Mass":2999.726},{"Name":"player_5000,0","X":5000,"Y":0,"ARGBColor":-16777216,"ID":3030,"Mass":2999.726},{"Name":"player_5000,1000","X":5000,"Y":1000,"ARGBColor":-657956,"ID":3031,"Mass":2999.726},{"Name":"player_5000,2000","X":5000,"Y":2000,"ARGBColor":-3308225,"ID":3032,"Mass":2999.726},{"Name":"player_5000,3000","X":5000,"Y":3000,"ARGBColor":-16777088,"ID":3033,"Mass":2999.726},{"Name":"player_5000,4000","X":5000,"Y":4000,"ARGBColor":-2987746,"ID":3034,"Mass":2999.726},{"Name":"player_5000,5000","X":5000,"Y":5000,"ARGBColor":-65536,"ID":3035,"Mass":2999.726}]
        ///   </para>
        /// </summary>
        public const string CMD_Update_Players = "{Command Players}";// 包含当前玩家ID

        /// <summary>
        ///   <para>
        ///     Use this string format description to start a game.
        ///     The value represents the name of the player.
        ///   </para>
        ///   <para>
        ///     Warning: Only send this after the connection has been established (e.g., in onConnect)
        ///     and the player is ready to start playing.  If the player dies,
        ///     you can resend this (don't send it if alive) to start another session
        ///     without having to disconnect and reconnect the network.
        ///   </para>
        ///   <para>
        ///     Example of usage: String.Format(Protocols.CMD_Start_Game, "Jim"); 
        ///   </para>
        /// </summary>
        public const string CMD_Start_Game = @"{{name,""{0}""}}";// client to server 

        /// <summary>
        ///   <para>
        ///     The server will recognize the start game protocol command using this regular expression.
        ///   </para>
        ///   <para>
        ///     Warning: the actual name must be in quotes
        ///   </para>
        ///   <para>
        ///     Example of usage: Regex.Match( CMD_Move_Recognizer, Protocols.CMD_Move_Recognizer );
        ///     Example of command generated: {name,"Jim"}
        ///   </para>
        /// </summary>
        public const string CMD_Start_Recognizer  = @"{name,""(.+)""}";


        /// <summary>
        ///   <para>
        ///     Use this string format description to create a valid move command to send to server.
        ///     The values represent the location in world coordinates that you want the player to move toward.
        ///   </para>
        ///   <para>
        ///     Warning: only positive and negative integers are accepted!
        ///   </para>
        ///   <para>
        ///     Example of usage: String.Format(Protocols.CMD_Move, 100, 500);
        ///   </para>
        /// </summary>
        public const string CMD_Move             = @"{{move,{0},{1}}}";// client to server 

        /// <summary>
        ///   <para>
        ///     The server will recognize the move protocol command using this regular expression.
        ///   </para>
        ///   <para>
        ///     Warning: only positive and negative integers are accepted!
        ///     Warning: Only used by the server!
        ///   </para>
        ///   <para>
        ///     Example of usage: Regex.Match( CMD_Move_Recognizer, Protocols.CMD_Move_Recognizer );
        ///   </para>
        /// </summary>
        public const string CMD_Move_Recognizer  = @"{move,(-?\d+),(-?\d+)}";

        /// <summary>
        ///   <para>
        ///     Use this string format description to create a valid split command to send to server.
        ///     The values represent the location in world coordinates that you want the player to split toward.
        ///   </para>
        ///   <para>
        ///     Warning: only positive and negative integers are accepted!
        ///   </para>
        ///   <para>
        ///     Example of usage: String.Format(Protocols.CMD_Split, 100, 500);
        ///   </para>
        /// </summary>
        public const string CMD_Split            = @"{{split,{0},{1}}}";//把自己分成两半

        /// <summary>
        ///   <para>
        ///     The server will recognize the split protocol command using this regular expression.
        ///   </para>
        ///   <para>
        ///     Warning: only positive and negative integers are accepted!
        ///     Warning: Only used by the server!
        ///   </para>
        ///   <para>
        ///     Example of usage: Regex.Match( "CMD_Split_Recognizer, Protocols.CMD_Move_Recognizer );
        ///   </para>
        /// </summary>
        public const string CMD_Split_Recognizer = @"{split,(-?\d+),(-?\d+)}";
    }
}
