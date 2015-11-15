namespace ScaffoldR.Core.Views
{
    /// <summary>
    /// Processes and creates view models.
    /// </summary>
    public interface IProcessViewModels
    {
        /// <summary>
        /// Creates the <typeparamref name="TViewModel"/>.
        /// </summary>
        /// <returns>The view model</returns>
        TViewModel Create<TViewModel>() where TViewModel : IViewModel;

        /// <summary>
        /// Create the <typeparamref name="TViewModel"/> with an argument of type <typeparamref name="TInput"/> 
        /// </summary>
        /// <typeparam name="TViewModel">The view model which should be constructed</typeparam>
        /// <typeparam name="TInput">The type of argument for the view model</typeparam>
        /// <param name="input">The argument for the view model</param>
        /// <returns>The view model</returns>
        TViewModel Create<TViewModel, TInput>(TInput input) where TViewModel : IViewModel;
    }
}
