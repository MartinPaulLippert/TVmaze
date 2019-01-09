using LiteDB;
using TVmaze.Domain.Interfaces;
using TVmaze.Domain.Models;

namespace TVmaze.Infrastructure.LiteDBData
{
    public class ShowUpdatesRepository : IShowUpdatesRepository
    {
        private readonly string connectionString = @".\TVmaze.db";

        public void AddShowUpdate(string showId, int timestamp)
        {
            ShowUpdate showUpdate = new ShowUpdate { Id = showId, UpdateTimestamp = timestamp };

            using (var db = new LiteDatabase(connectionString))
            {
                var showUpdatesCollection = db.GetCollection<ShowUpdate>();

                showUpdatesCollection.Upsert(showUpdate);
            }
        }

        public bool CheckIfShowNeedsUpdating(string showId, int timestamp)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var showUpdates = db.GetCollection<ShowUpdate>();
                var result = showUpdates.FindOne(x => x.Id == showId);

                if (result != null && result.UpdateTimestamp == timestamp)
                    return false;
            }
            return true;
        }
    }
}
