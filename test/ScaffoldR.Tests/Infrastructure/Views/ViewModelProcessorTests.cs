using ScaffoldR.Core.Views;
using ScaffoldR.Infrastructure.Views;
using ScaffoldR.Tests.Infrastructure.CompositionRoot;
using ScaffoldR.Tests.Infrastructure.Views.Fakes;
using SimpleInjector;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Views
{
    [Collection("Simple Injector Tests")]
    public class ViewModelProcessorTests
    {
        private readonly CompositionRootFixture _fixture;

        public ViewModelProcessorTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void RegistersIProcessViewModels_UsingViewModelProcessor_AsSingleton()
        {
            var instance = _fixture.Container.GetInstance<IProcessViewModels>();
            var registration = _fixture.Container.GetRegistration(typeof(IProcessViewModels));

            Assert.NotNull(instance);
            Assert.IsType<ViewModelProcessor>(instance);
            Assert.Equal(Lifestyle.Singleton, registration.Lifestyle);
        }

        [Fact]
        public void Execute_InvokesViewModelHandler_UsingContainerForResolution()
        {
            var models = _fixture.Container.GetInstance<IProcessViewModels>();
            var viewModel = models.Create<FakeViewModel>();

            Assert.Equal("faked", viewModel.ReturnType);
        }

        [Fact]
        public void Execute_InvokesViewModelWithArgumentHandler_UsingContainerForResolution()
        {
            var models = _fixture.Container.GetInstance<IProcessViewModels>();
            var viewModel = models.Create<FakeViewModel, string>("faked");

            Assert.Equal("faked", viewModel.ReturnType);
        }
    }
}
