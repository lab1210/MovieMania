using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie_Mania.Models;


namespace Movie_Mania.IService
{
    public interface IRegisterService
    {
        Registers AddUser(Registers register);
        IEnumerable<Registers> AdminList();

    }
}
