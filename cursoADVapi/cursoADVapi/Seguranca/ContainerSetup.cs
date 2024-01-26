using System;
using cursoADVapi.Business._Interface;
using cursoADVapi.Business._Busines._Login;

namespace cursoADVapi.Seguranca
{
    public class ContainerSetup
    {
        private static Boolean configured;

        public static void Reconfigure()
        {
            configured = false;
            Configure();
        }

        public static void Configure() {
            if (!configured)
            {
                Container.Setup();
                configured = true;

                Container.Bind<ILogin, LoginBusiness>();

            }
        }
    }
}
