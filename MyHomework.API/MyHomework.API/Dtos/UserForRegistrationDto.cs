﻿using System.ComponentModel.DataAnnotations;

namespace MyHomework.API.Dtos
{
    public class UserForRegistrationDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }   
    }
}
