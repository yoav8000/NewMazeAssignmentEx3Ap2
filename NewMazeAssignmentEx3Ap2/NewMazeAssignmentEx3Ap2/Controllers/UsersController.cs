using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NewMazeAssignmentEx3Ap2.Models;
using NewMazeAssignmentEx3Ap2.Models.UserModel;
using System.Security.Cryptography;
using System.Text;

namespace NewMazeAssignmentEx3Ap2.Controllers
{
    public class UsersController : ApiController
    {
        private NewMazeAssignmentEx3Ap2Context db = new NewMazeAssignmentEx3Ap2Context();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return Ok("user wasn't in db");
            }

            return Ok(user);
        }



        // GET: api/Users/{userName}/{password}
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(string userName, string password)
        {

            SHA1 sha = SHA1.Create();
            byte[] buffer = Encoding.ASCII.GetBytes(password);
            byte[] hash = sha.ComputeHash(buffer);
            string encryptedPassword = Convert.ToBase64String(hash);

            User user = await db.Users.FindAsync(password);
            if (user == null)
            {
                return Ok("user wasn't in db");
            }

            if (user.Password != encryptedPassword)
            {
                return Ok("incorrect password");
            }
            else
            {

                return Ok(user);
            }
        }



        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Name)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            SHA1 sha = SHA1.Create();
            byte[] buffer = Encoding.ASCII.GetBytes(user.Password);
            byte[] hash = sha.ComputeHash(buffer);
            string encryptedPassword = Convert.ToBase64String(hash);
            user.Password = encryptedPassword;

            db.Users.Add(user);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.Name }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.Name == id) > 0;
        }
    }
}