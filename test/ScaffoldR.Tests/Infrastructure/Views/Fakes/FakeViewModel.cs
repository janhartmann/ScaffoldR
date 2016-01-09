using ScaffoldR.Core.Views;

namespace ScaffoldR.Tests.Infrastructure.Views.Fakes
{
    public class FakeViewModel : IViewModel
    {
        public string ReturnType { get; set; }
    }

    public class HandleFakeViewModel : IHandleViewModel<FakeViewModel>
    {
        public FakeViewModel Handle()
        {
            return new FakeViewModel {ReturnType = "faked"};
        }
    }

    public class HandleFakeViewModelWithArgument : IHandleViewModel<FakeViewModel, string>
    {
        public FakeViewModel Handle(string input)
        {
            return new FakeViewModel { ReturnType = input };
        }
    }
}
