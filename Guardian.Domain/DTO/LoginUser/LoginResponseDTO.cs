﻿using Guardian.Domain.Models;

namespace Guardian.Domain.DTO.LoginUser
{
    public class LoginResponseDTO
    {
        public LoginResponseDTO(UserDTO user, string token)
        {
            User = user;
            Token = token;
            //Role = role;
        }

        public UserDTO User { get; set; }
        public string Token { get; set; } //Token is used to authenticate or rather than validade de identity of that user
    }
}
