using CadavizCodeHub.Framework.Domain;
using System;

namespace CadavizCodeHub.TestFramework.TestClasses.Domain
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
