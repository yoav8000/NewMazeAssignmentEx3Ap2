using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using NewMazeAssignmentEx3Ap2.Models.UserModel;
namespace NewMazeAssignmentEx3Ap2.Models
{
    public class NewMazeAssignmentEx3Ap2Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public NewMazeAssignmentEx3Ap2Context() : base("name=NewMazeAssignmentEx3Ap2Context")
        {
        }


        public System.Data.Entity.DbSet<User> Users { get; set; }
    }
}
