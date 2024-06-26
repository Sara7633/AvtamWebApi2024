﻿using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class UserService : IUserService
    {
        public readonly IUserRepository userRepository;
        public readonly IConfiguration _configuration;
        public readonly IPasswordHashHelper passwordHashHelper;
        public UserService(IUserRepository userRepository, IConfiguration configuration,IPasswordHashHelper passwordHashHelper)
        {
            this.userRepository = userRepository;
            _configuration = configuration;
            this.passwordHashHelper = passwordHashHelper;
        }

        public async Task<User> Register(User user)
        {
            user.Salt=passwordHashHelper.GenerateSalt(6);
            user.Password = passwordHashHelper.HashPassword(user.Password,user.Salt,1000,15);
            return await userRepository.Register(user);
        }
        public async Task<User> Login(User user)
        {
            //var bytes = passwordHashHelper.GetHash(user.Password, user.Salt);
            //passwordHashHelper.CompareHash(user.Password,bytes,user.Salt);
            User u = await userRepository.Login(user);
            return u;
        }

        public async Task<User> Update(int id, User user)
        {
            return await userRepository.Update(id, user);
        }

        public int checkPassword(string password)
        {
            if (password.Length == 0)
                return 0;
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;
        }

        public async Task<User> GetUserById(int id)
        {
            return await userRepository.GetUserById(id);
        }

        public string generateJwtToken(User user)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("key").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                   // new Claim("roleId", 7.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
