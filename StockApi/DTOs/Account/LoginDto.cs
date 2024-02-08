﻿namespace StockApi.DTOs.Account
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; }=string.Empty;
    }
}
