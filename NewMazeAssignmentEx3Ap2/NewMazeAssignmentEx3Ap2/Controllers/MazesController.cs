

using NewMazeAssignmentEx3Ap2.Models;
using NewMazeAssignmentEx3Ap2.Models.MazeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewMazeAssignmentEx3Ap2.Controllers
{
    public class MazesController : ApiController
    {
     //   private IModelManager mazeManager = new ModelManager();
        private static IModel model = new Model();


        // GET: api/Mazes
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpGet]
        // GET: api/Mazes/{mazeName}/{rows}/{cols}
        public string Get(string mazeName, int rows, int cols)
        {
            model.GenerateteSinglePlayerMaze(mazeName, rows, cols);
            return (string)model.SinglePlayerMazes[mazeName].ToJSON();


        //    mazeManager.GenerateSinglePlayerMaze(mazeName, rows, cols);
    //        return (string)mazeManager.GetSinglePlayerMazeByName(mazeName).ToJSON();
        }

        [HttpGet]
        // GET: api/Mazes/{mazeName}/{searchAlgorithm}
        public string Get(string mazeName,string searchAlgorithm)
        {
         
            return model.SolveMaze(mazeName, searchAlgorithm);

        }



        // GET: api/Mazes/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Mazes
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Mazes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Mazes/5
        public void Delete(int id)
        {
        }
    }
}
