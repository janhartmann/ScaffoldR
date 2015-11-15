using System;

namespace ScaffoldR.Core.Transactions
{
    /// <summary>
    /// Marks the class as being a transactional call. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TransactionalAttribute : Attribute
    {

    }
}