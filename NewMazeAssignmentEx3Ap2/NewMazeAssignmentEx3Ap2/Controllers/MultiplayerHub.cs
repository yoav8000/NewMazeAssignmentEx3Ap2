using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Concurrent;
using NewMazeAssignmentEx3Ap2.Models;
using MazeLib;

namespace NewMazeAssignmentEx3Ap2.Controllers
{

    /// <summary>
    /// the controller that works on SignalIR and take care community 
    /// between the players
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hub" />
    [HubName("multiPlayerHub")]
    public class MultiplayerHub : Hub
    {
        //maps mazeName into Players.
        private static ConcurrentDictionary<string, Players> mazesToPlayersDic = new ConcurrentDictionary<string, Players>();
        private static IModel mazeModel = new Model();


        /// <summary>
        /// Starts the command.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public void StartCommand(string mazeName, int rows, int cols)
        {
            Players players = new Players();
            players.PlayerId = this.Context.ConnectionId;
            string maze = mazeModel.GenerateMultiPlayerMaze(mazeName, rows, cols);
            mazesToPlayersDic[mazeName] = players;

        }
        /// <summary>
        /// Joins the command.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        public void JoinCommand(string mazeName)
        {
            Players players = mazesToPlayersDic[mazeName];
            players.OpponentId = this.Context.ConnectionId;
            Maze maze = mazeModel.JoinMaze(mazeName);
            this.Clients.Client(players.PlayerId).getMaze((string)maze.ToJSON());
            this.Clients.Client(players.OpponentId).getMaze((string)maze.ToJSON());

        }

        /// <summary>
        /// Lists the command.
        /// </summary>
        public void ListCommand()
        {
            List<string> mazesList = mazeModel.GetNamesOfJoinableMazes();
            string playerId = this.Context.ConnectionId;
            this.Clients.Client(playerId).updateJoinablMazes(mazesList);


        }


        /// <summary>
        /// Plays the command.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="direction">The direction.</param>
        public void PlayCommand(string mazeName, string direction)
        {
            string playerId = this.Context.ConnectionId;
            Players players = mazesToPlayersDic[mazeName];
            string connectionId = null;
            if (playerId == players.PlayerId)
            {
                connectionId = players.OpponentId;
            }
            else
            {
                connectionId = players.PlayerId;
            }
            this.Clients.Client(connectionId).moveOpponent(direction);
        }


    }














    public class Players
    {

        public Players()
        {

        }



        public string PlayerId
        {
            get;
            set;
        }

        public string OpponentId
        {
            get;
            set;
        }






    }







}