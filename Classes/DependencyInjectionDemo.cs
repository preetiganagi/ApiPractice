using System;
using ApiPractice.Interfaces;

namespace ApiPractice.Classes
{
   
    public class DependencyInjectionDemo : IOperationTransient, IOperationScoped, IOperationSingleton, IOperationSingletonInstance
    {
        Guid _guid;
        public DependencyInjectionDemo() : this(Guid.NewGuid())
        {

        }

        public DependencyInjectionDemo(Guid guid)
        {
            _guid = guid;
        }

        public Guid OperationId => _guid;
    }
    
}
