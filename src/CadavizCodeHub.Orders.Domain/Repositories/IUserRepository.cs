using CadavizCodeHub.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CadavizCodeHub.Orders.Domain.Repositories
{
    public interface IUserRepository
    {
        User? GetUserById(Guid Id, CancellationToken token);
        IEnumerable<User> GetAllUsers(CancellationToken token);
        User? AddUser(User user, CancellationToken token);
        bool DeleteUser(Guid id, CancellationToken token);
    }
}