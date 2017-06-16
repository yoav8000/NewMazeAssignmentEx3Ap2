using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace NewMazeAssignmentEx3Ap2.Models
{
    public interface IModel
    {

     //   string GenerateMultiPlayerMaze(string name, int rows, int cols, Player player);
        Maze GenerateteSinglePlayerMaze(string name, int rows, int cols);
        string SolveMaze(string mazeName, string algorithm);
        ISearcher<Position> GetAlgorithmAccordingToIndicator(string algorithmIndicator);
        List<string> GetNamesOfJoinableMazes();
      //  Maze JoinMaze(string maze, Player player);
       // string Play(string[] args, Player player);
       // string Close(string mazeName, Player player);


        /// <summary>
        /// Gets the algorithm factory.
        /// </summary>
        /// <value>
        /// The algorithm factory.
        /// </value>
        SearchAlgorithmFactory<Position> AlgorithmFactory
        {
            get;
        }

        /// <summary>
        /// Gets the maze solutions.
        /// </summary>
        /// <value>
        /// The maze solutions.
        /// </value>
        Dictionary<string, Solution<Position>> MazeSolutions
        {
            get;
        }


        /// <summary>
        /// Gets the single player mazes.
        /// </summary>
        /// <value>
        /// The single player mazes.
        /// </value>
        Dictionary<string, Maze> SinglePlayerMazes
        {
            get;
        }

        /// <summary>
        /// Gets the multi player mazes.
        /// </summary>
        /// <value>
        /// The multi player mazes.
        /// </value>
        Dictionary<string, Maze> MultiPlayerMazes
        {
            get;
        }

        /// <summary>
        /// Gets the joinable mazes.
        /// </summary>
        /// <value>
        /// The joinable mazes.
        /// </value>
        /*
        Dictionary<string, MazeGame> JoinableMazes
        {
            get;
        }
        */

        /// <summary>
        /// Gets the active multi player mazes.
        /// </summary>
        /// <value>
        /// The active multi player mazes.
        /// </value>
        /// 
        /*
        Dictionary<string, MazeGame> ActiveMultiPlayerMazes
        {
            get;
        }
        */
        /// <summary>
        /// Gets the maze generator.
        /// </summary>
        /// <value>
        /// The maze generator.
        /// </value>
        DFSMazeGenerator MazeGenerator
        {
            get;
        }

        /// <summary>
        /// Gets the players and games.
        /// </summary>
        /// <value>
        /// The players and games.
        /// </value>
        /// 
        /*
                Dictionary<Player, MazeGame> PlayersAndGames
                {
                    get;
                }
                */

        /// <summary>
        /// Gets or sets the i controller.
        /// </summary>
        /// <value>
        /// The i controller.
        /// </value>
        /*
        IController IController
        {
            get;
            set;
        }
        */
    }
}