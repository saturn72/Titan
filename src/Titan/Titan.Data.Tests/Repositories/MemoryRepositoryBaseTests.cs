using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Saturn72.Core.Domain;
using Shouldly;
using Titan.Data.Repositories;

namespace Titan.Data.Tests.Repositories
{
    public class MemoryRepositoryBaseTests
    {
        [Test]
        public void MemoryRepositoryBase_Add()
        {
            var repo = new TestObjectMemoryRepository();
            var to1 = new TestObject {Name = "1"};
            var to2 = new TestObject {Name = "2"};

            repo.Add(to1);
            to1.Id.ShouldBe(1);
            repo.Add(to2);
            to2.Id.ShouldBe(2);

            repo.RepositoryData.Count().ShouldBe(2);
            repo.RepositoryData.ShouldContain(to1);
            repo.RepositoryData.ShouldContain(to2);
        }

        [Test]
        public void MemoryRepositoryBase_Table()
        {
            var repo = new TestObjectMemoryRepository();
            var to1 = new TestObject {Id = 1, Name = "1"};
            var to2 = new TestObject {Id = 2, Name = "2"};

            var beforeAddingCount = repo.Collection.Count();

            repo.RepositoryData.Add(to1);
            repo.RepositoryData.Add(to2);

            repo.Collection.Count().ShouldBe(beforeAddingCount + 2);
            repo.Collection.ShouldContain(to1);
            repo.Collection.ShouldContain(to2);
        }


        internal class TestObjectMemoryRepository : MemoryRepositoryBase<TestObject>
        {
            internal new ICollection<TestObject> RepositoryData
            {
                get { return base.GetTableAsCollection(); }
            }
        }

        internal class TestObject : DomainModelBase
        {
            public string Name { get; set; }
        }
    }
}