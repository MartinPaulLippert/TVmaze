using System.Linq;
using TVmaze.Domain.Models;

namespace TVmaze.Domain.Interfaces
{
    public interface IShowsRepository
    {
        void AddShow(Show show);
        IQueryable<Show> GetShows(int page, int pagesize);
    }
}
