using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchAlgorithmsLib;
using MazeLib;

namespace NewMazeAssignmentEx3Ap2.Models.MazeManager
{

    public interface IModelManager
    {
        IEnumerable<Maze> GetAllMazes();
        Maze GetSinglePlayerMazeByName(string mazeName);
        void GenerateSinglePlayerMaze(string name, int rows, int cols);
        void DeleteMaze(string mazeName);


    }
}