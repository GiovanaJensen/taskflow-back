using System;
using System.Collections.Generic;
using System.Linq;
using Taskflow.Api.Models;
using Taskflow.Api.Repositories.Interfaces;

namespace Taskflow.Api.Repositories.Interfaces
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new()
        {
            new User { Username = "testuser", Password = "P@ssw0rd", Role = "User" },
            new User { Username = "admin", Password = "Admin123!", Role = "Admin" }
        };

        public User? GetByUsername(string username)
        {
            return _users.FirstOrDefault(u => 
                u.Username != null && u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}