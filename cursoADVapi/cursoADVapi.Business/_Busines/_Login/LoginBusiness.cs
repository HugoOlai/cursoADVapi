using cursoADVapi.Business._Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursoADVapi.Business._Busines._Login
{
    public class LoginBusiness: ILogin
    {
        public string Login(string username, string password)
        {
            return username + password;
        }

    }
}
