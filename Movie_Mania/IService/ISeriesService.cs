using Movie_Mania.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Mania.IService
{
    public interface ISeriesService
    {
        bool AddSeries(SeriesResources series);
        IEnumerable<SeriesResources> GetAllSeries();
        IEnumerable<SeriesResources> GetSeriesByCountry(string countryName);
        IEnumerable<string> GetCountry();

        SeriesResources Getgenredetails();

        SeriesResources GetSeriesById(int id);
        SeriesResources GetSeriesByCode( string slug);

        void UpdateSeries(SeriesResources series);
        void DeleteSeries(int id);
        IEnumerable<SeriesResources> Search();
        void AddCommentForSeries(int seriesId, string user, string post,string slug);
        void AddOrUpdateSeriesRating(int seriesId, int rating,string user,string slug);
        decimal GetSeriesRating(string slug);
        IEnumerable<string> GetGenres();
        IEnumerable<SeriesResources> GetSeriesByGenre(string genreName);


    }
}
