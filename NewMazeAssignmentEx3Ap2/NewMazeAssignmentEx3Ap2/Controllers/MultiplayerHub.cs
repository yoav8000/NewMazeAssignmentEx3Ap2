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

    [HubName("multiPlayerHub")]
    public class MultiplayerHub : Hub
    {
        //maps mazeName into Players.
        private static ConcurrentDictionary<string, Players> mazesToPlayersDic = new ConcurrentDictionary<string, Players>();
        private static IModel mazeModel = new Model();


        public void StartCommand(string mazeName, int rows, int cols)
        {
            Players players = new Players();
            players.PlayerId = this.Context.ConnectionId;
            string maze = mazeModel.GenerateMultiPlayerMaze(mazeName, rows, cols);
            mazesToPlayersDic[mazeName] = players;

        }
        public void JoinCommand(string mazeName)
        {
            Players players = mazesToPlayersDic[mazeName];
            players.OpponentId = this.Context.ConnectionId;
            Maze maze = mazeModel.JoinMaze(mazeName);
            this.Clients.Client(players.PlayerId).getMaze((string)maze.ToJSON());
            this.Clients.Client(players.OpponentId).getMaze((string)maze.ToJSON());

        }

        public void ListCommand()
        {
            List<string> mazesList = mazeModel.GetNamesOfJoinableMazes();
            string playerId = this.Context.ConnectionId;
            this.Clients.Client(playerId).updateJoinablMazes(mazesList);


        }


        public void PlayCommand(string direction)
        {




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