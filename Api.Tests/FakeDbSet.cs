using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;

namespace Api.Tests
{
    public class FakeDbSet<T> : Mock<DbSet<T>> where T : class
    {
        public void SetData(IEnumerable<T> data)
        {
            var mockDataQueryable = data.AsQueryable();

            As<IQueryable<T>>().Setup(x => x.Provider).Returns(mockDataQueryable.Provider);
            As<IQueryable<T>>().Setup(x => x.Expression).Returns(mockDataQueryable.Expression);
            As<IQueryable<T>>().Setup(x => x.ElementType).Returns(mockDataQueryable.ElementType);
            As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(mockDataQueryable.GetEnumerator());
        }
    }
}
