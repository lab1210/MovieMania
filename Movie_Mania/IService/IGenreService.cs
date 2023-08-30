using Movie_Mania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Mania.IService
{
    public interface IGenreService
    {
        Genre SaveGenre(Genre genre);
        IEnumerable<Genre> GetAll();
        Genre GetGenreByID(int id);
        void DeleteGenre(int id);
    }
}
