﻿using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTOs
{
    public class EmployeeSignInModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
    }

    public class EmployeeSignInResponse
    {
        public string UserId { get; set; } = "";
        public string Token { get; set; } = "";
        public string Role { get; set; } = "";
    }

    public class EmployeeSignUpModel
    {

        [Required, EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
        [Required]
        public string? LastName { get; set; } = null!;
        [Required]
        public string? FirstName { get; set; } = null!;
        public DateTime? DoB { get; set; }
        [Required]
        public DateTime HireDate { get; set; }
        public string? Address { get; set; } = null!;
        public string? Phone { get; set; } = null!;
    }
}
