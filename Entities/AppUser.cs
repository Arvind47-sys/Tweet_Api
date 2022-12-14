using System;
using System.ComponentModel.DataAnnotations;

namespace Tweet_Api.Entities
{
    public class AppUser
    {
        [Key]
        public int LoginId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public long ContactNumber { get; set; }
    }
}