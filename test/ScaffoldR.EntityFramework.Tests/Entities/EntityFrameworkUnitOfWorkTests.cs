using Moq;
using ScaffoldR.EntityFramework.Entities;
using ScaffoldR.EntityFramework.Tests.Fakes;
using ScaffoldR.EntityFramework.Tests.Helpers;
using Xunit;

namespace ScaffoldR.EntityFramework.Tests.Entities
{
    public class EntityFrameworkUnitOfWorkTests
    {
        [Fact]
        public void Execute_InvokesSaveChanges_OnDbContext()
        {
            var context = EntityFrameworkMockHelper.GetMockContext<FakeDbContext>();
            context.Setup(x => x.SaveChanges()).Returns(1);

            var unitOfWork = new EntityFrameworkUnitOfWork(() => context.Object);
            unitOfWork.Execute();

            context.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
