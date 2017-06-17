using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewMazeAssignmentEx3Ap2.Models.MazeManager
{
    public class ModelManager: IModelManager
    {
        private static IModel mazeModel = new Model();

      

        public void GenerateSinglePlayerMaze(string name, int rows, int cols)
        {
            mazeModel.GenerateteSinglePlayerMaze(name, rows, cols);
        }

        public void DeleteMaze(string mazeName)
        {

            //mazeModel.DeleteMaze(mazeName);
            int x = 2;
        }

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

        
        Maze IModelManager.GetSinglePlayerMazeByName(string mazeName)
        {
            return mazeModel.SinglePlayerMazes[mazeName];
        }

        public string SolveMaze(string mazeName, string searchAlgorithm)
        {
           string result = mazeModel.SolveMaze(mazeName, searchAlgorithm);
            return result;
        }
    }
}