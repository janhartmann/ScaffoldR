using ScaffoldR.Core.Queries;

namespace ScaffoldR.Tests.Infrastructure.Queries.Fakes
{
    public class FakeQueryWithoutValidator : IQuery<string>
    {
        public string ReturnValue { get; internal set; }
    }

    public class HandleFakeQueryWithoutValidator : IHandleQuery<FakeQueryWithoutValidator, string>
    {
        public string Handle(FakeQueryWithoutValidator query)
        {
            return "faked";
        }
    }
}
