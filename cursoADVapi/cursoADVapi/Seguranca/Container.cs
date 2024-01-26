using Ninject;
using System;

namespace cursoADVapi.Seguranca
{
    public class Container
    {
        private static IKernel kernel;

        public static void Setup()
        {
            kernel = new StandardKernel();
        }

        public static void Bind<TInterface, TImplementation>()
        {
            ValidateContainer();
            kernel.Bind<TInterface>().To(typeof(TImplementation));
        }

        private static void ValidateContainer()
        {
            if (kernel == null) throw new InvalidOperationException("O container não foi inicializado. Execute o método DependencyInjectionContainer.Setup() antes de utilizar o container");
        }

        public static T Get<T>()
        {
            ValidateContainer();
            return kernel.Get<T>();
        }
    }
}
