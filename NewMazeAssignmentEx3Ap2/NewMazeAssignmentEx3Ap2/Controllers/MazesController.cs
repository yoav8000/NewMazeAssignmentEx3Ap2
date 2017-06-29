

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
    /// <summary>
    /// single player controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class MazesController : ApiController
    {
        //   private IModelManager mazeManager = new ModelManager();
        private static IModel model = new Model();


        // GET: api/Mazes
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        /// <summary>
        /// Gets the specified maze name.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        [HttpGet]
        // GET: api/Mazes/{mazeName}/{rows}/{cols}
        public string Get(string mazeName, int rows, int cols)
        {
            model.GenerateteSinglePlayerMaze(mazeName, rows, cols);
            return (string)model.SinglePlayerMazes[mazeName].ToJSON();


            //    mazeManager.GenerateSinglePlayerMaze(mazeName, rows, cols);
            //        return (string)mazeManager.GetSinglePlayerMazeByName(mazeName).ToJSON();
        }

        /// <summary>
        /// Gets the specified maze name.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="searchAlgorithm">The search algorithm.</param>
        /// <returns></returns>
        [HttpGet]
        // GET: api/Mazes/{mazeName}/{searchAlgorithm}
        public string Get(string mazeName, string searchAlgorithm)
        {

            return model.SolveMaze(mazeName, searchAlgorithm);

        }



        // GET: api/Mazes/5
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Mazes
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Mazes/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Mazes/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
        }
    }
}
