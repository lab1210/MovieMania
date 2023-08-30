using Movie_Mania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Mania.IService
{
    public interface ILoginService
    {
        Logins CheckUser(Logins login);
        void StoreLoginName(string name);

        void StoreRole(string role);

        void clearLoginName();



    }
}
