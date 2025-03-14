using CadavizCodeHub.Core.Domain.Entities;
using System;

namespace CadavizCodeHub.Tests.Shared.TestClasses.Domain
{
    public class TestEntityBase : EntityBase
    {
        public TestEntityBase(): base() { }

        public TestEntityBase(Guid id)
        {
            Id = id;
        }
    }
}
