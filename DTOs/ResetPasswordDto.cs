﻿namespace Tweet_Api.DTOs
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}