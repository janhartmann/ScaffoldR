using System;

namespace ScaffoldR.Core.Tasks
{
    /// <summary>
    /// Starts a task.
    /// </summary>
    public interface ITaskFactory
    {
        /// <summary>
        /// Starts a task with the specified action.
        /// </summary>
        /// <param name="action">The action to start in the task.</param>
        void StartTask(Action action);
    }
}