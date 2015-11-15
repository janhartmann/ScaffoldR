using System;
using System.Threading.Tasks;
using ScaffoldR.Core.Tasks;

namespace ScaffoldR.Infrastructure.Tasks
{
    internal sealed class TaskFactory : ITaskFactory
    {
        public void StartTask(Action action)
        {
            Task.Factory.StartNew(action);
        }
    }
}