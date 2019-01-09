using System;
using System.Collections.Generic;
using System.Text;

namespace TVmaze.Domain.Interfaces
{
    public interface IShowUpdatesRepository
    {
        bool CheckIfShowNeedsUpdating(string showId, int timestamp);
        void AddShowUpdate(string showId, int timestamp);
    }
}
