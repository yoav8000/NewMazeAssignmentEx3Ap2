using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using Newtonsoft.Json.Linq;



namespace NewMazeAssignmentEx3Ap2.Models
{
    public class Model : IModel
    {
        private DFSMazeGenerator mazeGenerator;
        private Dictionary<string, Solution<Position>> mazeSolutions;
        private SearchAlgorithmFactory<Position> algorithmFactory;
        private Dictionary<string, Maze> singlePlayerMazes;
        private Dictionary<string, Maze> multiPlayerMazes;
        // private Dictionary<string, MazeGame> joinableMazes;
        // private IController icontroller;
        //  private Dictionary<string, MazeGame> activeMultiPlayerMazes;
        //  private Dictionary<Player, MazeGame> playersAndGames;



        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="icontroller">The icontroller.</param>


        public Model()
        {
            this.mazeGenerator = new DFSMazeGenerator();
            this.algorithmFactory = new SearchAlgorithmFactory<Position>();
            this.singlePlayerMazes = new Dictionary<string, Maze>();
            this.mazeSolutions = new Dictionary<string, Solution<Position>>();
            this.multiPlayerMazes = new Dictionary<string, Maze>();
            //       this.joinableMazes = new Dictionary<string, MazeGame>();
            //      this.activeMultiPlayerMazes = new Dictionary<string, MazeGame>();
            //     this.playersAndGames = new Dictionary<Player, MazeGame>();

        }

        /// <summary>
        /// Gets or sets the i controller.
        /// </summary>
        /// <value>
        /// The i controller.
        /// </value>
        /// 
        /*
        public IController IController
        {
            get
            {
                return this.icontroller;
            }
            set
            {
                this.icontroller = value;
            }
        }
        */


        /// <summary>
        /// Gets the multi player mazes.
        /// </summary>
        /// <value>
        /// The multi player mazes.
        /// </value>
        public Dictionary<string, Maze> MultiPlayerMazes
        {
            get
            {
                return this.multiPlayerMazes;
            }
        }

        /// <summary>
        /// Gets the joinable mazes.
        /// </summary>
        /// <value>
        /// The joinable mazes.
        /// </value>
        /// 
        /*
        public Dictionary<string, MazeGame> JoinableMazes
        {
            get
            {
                return this.joinableMazes;
            }
        }
        */
        /// <summary>
        /// Gets the active multi player mazes.
        /// </summary>
        /// <value>
        /// The active multi player mazes.
        /// </value>
        /*
        public Dictionary<string, MazeGame> ActiveMultiPlayerMazes
        {
            get
            {
                return this.activeMultiPlayerMazes;
            }
        }
        */


        /// <summary>
        /// Gets the single player mazes.
        /// </summary>
        /// <value>
        /// The single player mazes.
        /// </value>
        public Dictionary<string, Maze> SinglePlayerMazes
        {
            get
            {
                return this.singlePlayerMazes;
            }
        }

        /// <summary>
        /// Gets the maze solutions.
        /// </summary>
        /// <value>
        /// The maze solutions.
        /// </value>
        public Dictionary<string, Solution<Position>> MazeSolutions
        {
            get
            {
                return this.mazeSolutions;
            }
        }

        /// <summary>
        /// Gets the players and games.
        /// </summary>
        /// <value>
        /// The players and games.
        /// </value>
        /*
        public Dictionary<Player, MazeGame> PlayersAndGames
        {
            get
            {
                return this.playersAndGames;
            }
        }
        */
        /// <summary>
        /// Gets the maze generator.
        /// </summary>
        /// <value>
        /// The maze generator.
        /// </value>
        public DFSMazeGenerator MazeGenerator
        {
            get
            {
                return this.mazeGenerator;
            }
        }


        /// <summary>
        /// Gets the algorithm factory.
        /// </summary>
        /// <value>
        /// The algorithm factory.
        /// </value>
        public SearchAlgorithmFactory<Position> AlgorithmFactory
        {
            get
            {
                return this.algorithmFactory;
            }
        }




        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        private Maze GenerateMaze(string name, int rows, int cols)
        {
            Maze maze = this.mazeGenerator.Generate(rows, cols);
            return maze;
        }


        /// <summary>
        /// Generatetes the single player maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">there is another maze with the same name</exception>
        public Maze GenerateteSinglePlayerMaze(string name, int rows, int cols)
        {

            if (NameExistsInDictionary(singlePlayerMazes, name))
            {
                throw new Exception("there is another maze with the same name");
            }
            Maze maze = this.GenerateMaze(name, rows, cols);
            maze.Name = name;
            singlePlayerMazes[name] = maze;
            return maze;
        }

        /// <summary>
        /// Generates the multi player maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public string GenerateMultiPlayerMaze(string name, int rows, int cols)
        {

            bool flag = false;
            /*
            if (joinableMazes.ContainsKey(name))
            {
                return "Error: there is a maze with the same name";
            }
            */
            Maze maze = null;
            int playersCapacity = 2;
            while (flag == false)
            {
                maze = this.GenerateMaze(name, rows, cols);
                if (maze.InitialPos.Row != maze.GoalPos.Row || maze.InitialPos.Col != maze.GoalPos.Col)
                {
                    flag = true;
                }
            }
            maze.Name = name;
            MultiPlayerMazes[name] = maze;
            /*
            MazeGame game = new MazeGame(name, maze, playersCapacity);
            JoinableMazes[name] = game;
            player.NeedToWait = true;
           
            game.AddPlayer(player);//the player needs to wait for another player to join the game.

            player.MazeName = game.MazeName;
    */
            return maze.ToJSON();

        }


        /// <summary>
        /// Names the exists in dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="mazeName">Name of the maze.</param>
        /// <returns></returns>
        private bool NameExistsInDictionary(Dictionary<string, Maze> dictionary, string mazeName)
        {
            return dictionary.ContainsKey(mazeName);
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public string SolveMaze(string mazeName, string algorithm)
        {

            if (!NameExistsInDictionary(SinglePlayerMazes, mazeName))
            {
                return ($"Error: there is no  maze with this name {mazeName}");
            }

            if (MazeExistsInSolutions(mazeName))
            {
                SolutionAdapter solutionAdapter1 = new SolutionAdapter(MazeSolutions[mazeName], mazeName);
                return solutionAdapter1.ToJson();
            }

            MazeAdapter mazeAdapter = new MazeAdapter(SinglePlayerMazes[mazeName]);
            ISearcher<Position> searchAlgorithm = GetAlgorithmAccordingToIndicator(algorithm);
            Solution<Position> solution = searchAlgorithm.Search(mazeAdapter);
            SolutionAdapter solutionAdapter = new SolutionAdapter(solution, mazeName);
            MazeSolutions[mazeName] = solution;
            return solutionAdapter.ToJson();
        }


        private bool MazeExistsInSolutions(string mazeName)
        {
            return MazeSolutions.ContainsKey(mazeName);
        }


        public ISearcher<Position> GetAlgorithmAccordingToIndicator(string algorithmIndicator)
        {
            return AlgorithmFactory.GetSearchAlgorithm(algorithmIndicator);
        }

        public List<string> GetNamesOfJoinableMazes()
        {
            throw new NotImplementedException();
        }

        /*
        public List<string> GetNamesOfJoinableMazes()
        {

       //     List<string> mazesList = new List<string>(JoinableMazes.Keys.ToList());
         //   return mazesList;

        }
        */


        /// <summary>
        /// Joins the maze.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        /// 
        /*
        public Maze JoinMaze(string mazeName, Player player)
        {

            if (!joinableMazes.ContainsKey(mazeName))
            {
                throw new Exception($"there is no such maze with the name {mazeName}");
            }

            try
            {
                MazeGame game = JoinableMazes[mazeName];
                game.AddPlayer(player);
                player.MazeName = game.MazeName;

                if (game.GameCapacity == game.Players.Count)
                {
                    ActiveMultiPlayerMazes[mazeName] = game;
                    joinableMazes.Remove(mazeName);
                    ReleasePlayerFromWaitingMode(game);
                    //game.NotifyOtherPlayers(game.Maze.ToJSON(), player);
                    //game.NotifyAllPlayers("The Game Has Started");
                }
                return game.Maze;

            }
            catch (Exception exception)
            {
                return null;
            }

        }
        

        private void ReleasePlayerFromWaitingMode(MazeGame game)//////////////////////////added a change.
        {
            foreach (Player p in game.Players)
            {

                PlayersAndGames[p] = game;
                p.NeedToWait = false;
            }

        }
        */

        /// <summary>
        /// Plays the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        /* 
        public string Play(string[] args, Player player)
        {

            string direction = args[0];
            if (player.MazeName == null)
            {
                return "Error: you are not a part of an active game";

            }
            string mazeName = player.MazeName;
            if (!ActiveMultiPlayerMazes.ContainsKey(mazeName))
            {
                return $"Error: there is no such maze with the name {mazeName}";
            }
            if (PlayersAndGames[player] == null)
            {
                return ("Error: you are not a part of a game at this point ");
            }
            MazeGame game = ActiveMultiPlayerMazes[mazeName];
            JObject jobject = new JObject();
            jobject["Name"] = game.MazeName;
            jobject["Direction"] = direction;
            game.NotifyOtherPlayers(jobject.ToString(), player);
            return null;

        }
        */
        /// <summary>
        /// Closes the specified maze name.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        /// 
        /*
        public string Close(string mazeName, Player player)
        {

            if (!ActiveMultiPlayerMazes.ContainsKey(mazeName))
            {
                return $"Error: there is no such maze to close named {mazeName}";
            }
            MazeGame game = ActiveMultiPlayerMazes[mazeName];//getting the game.

            game.NotifyOtherPlayers("The Game Was Closed", player);//for me.

            RemovePlayersFromPlayersAndGames(mazeName); //getting the players of the dictionary of the players and games
            game.CloseAllClients();
            activeMultiPlayerMazes.Remove(mazeName);


            return "";

        }
        */
        /// <summary>
        /// Removes the players from players and games.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        /*
        private void RemovePlayersFromPlayersAndGames(string mazeName)
        {
            MazeGame game = ActiveMultiPlayerMazes[mazeName];
            foreach (Player p in game.Players)
            {
                PlayersAndGames.Remove(p);
            }
        }
        */
    }
}