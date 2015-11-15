namespace ScaffoldR.Core.Views
{
    /// <summary>
    /// Handles the <typeparamref name="TViewModel"/>.
    /// </summary>
    /// <typeparam name="TViewModel">The view model which should be handled.</typeparam>
    public interface IHandleViewModel<TViewModel> where TViewModel : IViewModel
    {
        /// <summary>
        /// Creates a <typeparamref name="TViewModel"/>.
        /// </summary>
        /// <returns>An instance of the <typeparamref name="TViewModel"/>.</returns>
        TViewModel Handle();
    }

    /// <summary>
    /// Handles the <typeparamref name="TViewModel"/> with the argument of <typeparamref name="TInput"/>
    /// </summary>
    /// <typeparam name="TInput">The argument for the view model</typeparam>
    /// <typeparam name="TViewModel">The view model which should be handled.</typeparam>
    public interface IHandleViewModel<TViewModel, TInput> where TViewModel : IViewModel
    {
        /// <summary>
        /// Creates a <typeparamref name="TViewModel"/>.
        /// </summary>
        /// <returns>An instance of the <typeparamref name="TViewModel"/>.</returns>
        TViewModel Handle(TInput input);
    }
}
