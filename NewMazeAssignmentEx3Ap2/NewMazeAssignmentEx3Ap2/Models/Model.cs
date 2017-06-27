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
        private Dictionary<string, Maze> joinableMazes;



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
            this.joinableMazes = new Dictionary<string, Maze>();

        }


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

        public Dictionary<string, Maze> JoinableMazes
        {
            get
            {
                return this.joinableMazes;
            }
        }


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
            Maze maze;
            do
            {
                maze = this.mazeGenerator.Generate(rows, cols);
            } while (maze.InitialPos.Row == maze.GoalPos.Row && maze.GoalPos.Col == maze.InitialPos.Col);

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

            if (joinableMazes.ContainsKey(name))
            {
                return "Error: there is a maze with the same name";
            }

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
            JoinableMazes[name] = maze;
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

            List<string> mazesList = new List<string>(JoinableMazes.Keys.ToList());
            return mazesList;

        }



        /// <summary>
        /// Joins the maze.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        /// 

        public Maze JoinMaze(string mazeName)
        {

            if (!joinableMazes.ContainsKey(mazeName))
            {
                throw new Exception($"there is no such maze with the name {mazeName}");
            }

            try
            {
                Maze maze = JoinableMazes[mazeName];
                joinableMazes.Remove(mazeName);
                return maze;

            }
            catch (Exception exception)
            {
                return null;
            }

        }



        /// <summary>
        /// Plays the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        
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
    }
}