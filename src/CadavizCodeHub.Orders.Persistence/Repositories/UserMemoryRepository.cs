using CadavizCodeHub.Orders.Domain.Entities;
using CadavizCodeHub.Orders.Domain.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace CadavizCodeHub.Orders.Persistence.Repositories
{
    internal class UserMemoryRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<Guid, User> _users = new ConcurrentDictionary<Guid, User>();

        public User? GetUserById(Guid Id, CancellationToken token)
        {
            return _users.TryGetValue(Id, out var user) ? user : null;
        }

        public IEnumerable<User> GetAllUsers(CancellationToken token)
        {
            return _users.Values;
        }

        public User? AddUser(User user, CancellationToken token)
        {
            _users.TryAdd(user.Id, user);

            return user;
        }

        public bool DeleteUser(Guid id, CancellationToken token)
        {
            return _users.TryRemove(id, out _);
        }
    }
}