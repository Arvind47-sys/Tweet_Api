using System;

namespace Tweet_Api.DTOs
{
    public class UserDetailDto
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string Username { get; set; }

        public long ContactNumber { get; set; }
    }
}