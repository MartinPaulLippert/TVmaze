using System.Collections.Generic;
using System.Threading;
using TVmaze.Domain.Models;

namespace TVmaze.Services.Interfaces
{
    public interface ITVmazeService
    {
        Dictionary<string, int> GetShowsUpdate();
        void UpdateShows(CancellationToken cancellationToken);
        IEnumerable<ShowDTO> GetAllShows(int page, int pagesize);

    }
}
