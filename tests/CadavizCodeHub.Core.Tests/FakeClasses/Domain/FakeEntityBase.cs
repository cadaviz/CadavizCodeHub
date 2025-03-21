using CadavizCodeHub.Core.Domain.Entities;
using System;

namespace CadavizCodeHub.Core.Tests.FakeClasses.Domain
{
    public class FakeEntityBase : EntityBase
    {
        public FakeEntityBase(): base() { }

        public FakeEntityBase(Guid id)
        {
            Id = id;
        }
    }
}
