﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHomework.API.Dtos
{
    public class EnrolledStudentDto
    {
        public string Username { get; set; }    
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}