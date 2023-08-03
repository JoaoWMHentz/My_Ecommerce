using Helpers.connection;
using Helpers.converter;
using System;
using System.Collections.Generic;
using System.Data;
using Helpers.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using OpenEcommerceDLL.user;

namespace EcommerceDLL.user
{
    public class UserAuthenticate
    {

        public static bool AuthenticateUser(UserLogin login, User user, Connection con)
        {

            return HashPassword.VerifyPass(login.Password, user.Password);
        }
    }
}
