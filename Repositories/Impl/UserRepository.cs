﻿using G5_MovieTicketBookingSystem.Data;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;


        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> EmailExistsAsync(string email)
        {
            throw new NotImplementedException();
        }




        public async Task<User> SignUpAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User object cannot be null.");
            }

            // Validate input (e.g., check for existing user by email or username)
            if (await _dbContext.Users.AnyAsync(u => u.Email == user.Email || u.Username == user.Username))
            {
                throw new InvalidOperationException("User with this email or username already exists.");
            }

            // Hash the password before saving
            if (!string.IsNullOrEmpty(user.Password))// Assuming User has a Password property
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
               
            }

            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                return user; // Return the registered user
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to sign up user.", ex);
            }
        }


        public Task<bool> UsernameExistsAsync(string username)
        {
            throw new NotImplementedException();
        }
       




        public async Task<User> GetUserByEmail(string emailOrUsername)
        {
            if (string.IsNullOrEmpty(emailOrUsername))
            {
                return null; // Or throw an exception, depending on your design
            }

            User user = await _dbContext.Users.FirstOrDefaultAsync(ur => ur.Username == emailOrUsername);
            return user;
        }
    }
}
