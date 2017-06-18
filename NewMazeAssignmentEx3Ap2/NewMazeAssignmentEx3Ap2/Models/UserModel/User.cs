using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewMazeAssignmentEx3Ap2.Models.UserModel
{
    public class User
    {
        [Key]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email
        {
            get; set;
        }

        public int Wins
        {
            get;set;
        }

        public int Losses
        {
            get; set;
        }

    }
}