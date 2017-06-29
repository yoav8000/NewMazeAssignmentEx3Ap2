using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewMazeAssignmentEx3Ap2.Models.MazeManager
{
    /// <summary>
    /// the Model Manager
    /// </summary>
    /// <seealso cref="NewMazeAssignmentEx3Ap2.Models.MazeManager.IModelManager" />
    public class ModelManager: IModelManager
    {
        private static IModel mazeModel = new Model();



        /// <summary>
        /// Generates the single player maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public void GenerateSinglePlayerMaze(string name, int rows, int cols)
        {
            mazeModel.GenerateteSinglePlayerMaze(name, rows, cols);
        }

        /// <summary>
        /// Deletes the maze.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        public void DeleteMaze(string mazeName)
        {

            //mazeModel.DeleteMaze(mazeName);
            int x = 2;
        }

        /// <summary>
        /// Gets all mazes.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Maze> GetAllMazes()
        {
            List<Maze> mazeList = new List<Maze>();
            /*
            foreach (KeyValuePair<string, Maze> m in mazeModel.Mazes)
            {
                mazeList.Add(m.Value);

            }*/
            IEnumerable<Maze> mazes = new List<Maze>(mazeList);
            return mazes;
        }


        /// <summary>
        /// Gets the name of the single player maze by.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        /// <returns></returns>
        Maze IModelManager.GetSinglePlayerMazeByName(string mazeName)
        {
            return mazeModel.SinglePlayerMazes[mazeName];
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="searchAlgorithm">The search algorithm.</param>
        /// <returns></returns>
        public string SolveMaze(string mazeName, string searchAlgorithm)
        {
           string result = mazeModel.SolveMaze(mazeName, searchAlgorithm);
            return result;
        }

        /// <summary>
        /// Generates the multi player maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GenerateMultiPlayerMaze(string name, int rows, int cols)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Joins the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string JoinMaze(string name)
        {
            throw new NotImplementedException();
        }
    }
}