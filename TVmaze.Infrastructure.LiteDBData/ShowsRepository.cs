using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVmaze.Domain.Interfaces;
using TVmaze.Domain.Models;

namespace TVmaze.Infrastructure.LiteDBData
{
    public class ShowsRepository : IShowsRepository
    {
        private readonly string connectionString = @"TVmaze.db";

        public void AddShow(Show show)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var showUpdatesCollection = db.GetCollection<Show>();
                showUpdatesCollection.Upsert(show);
            }
        }

        public IQueryable<Show> GetShows(int page, int pagesize)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var showUpdatesCollection = db.GetCollection<Show>();
                return showUpdatesCollection.Find(Query.All(), pagesize * page, pagesize).AsQueryable<Show>();
            }
        }
    }
}
