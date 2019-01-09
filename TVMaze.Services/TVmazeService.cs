using AutoMapper.QueryableExtensions;
using Json.Net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using TVmaze.Domain.Interfaces;
using TVmaze.Domain.Models;
using TVmaze.Services.Interfaces;

namespace TVMaze.Services
{
    public class TVmazeService : ITVmazeService
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string baseUrl = "http://api.tvmaze.com/";
        private readonly string showUpdatesUrl = "updates/shows";
        private readonly string showDetailsUrl = "shows/{0}?embed=cast";
        private readonly int sleepTimeout = 10000;
        private readonly ILogger<TVmazeService> _logger;

        private readonly IShowUpdatesRepository _showUpdatesRepository;
        private readonly IShowsRepository _showsRepository;

        public TVmazeService(ILogger<TVmazeService> logger, IShowUpdatesRepository showUpdatesRepository, IShowsRepository showsRepository)
        {
            _showUpdatesRepository = showUpdatesRepository;
            _showsRepository = showsRepository;
            _logger = logger;
            client.DefaultRequestHeaders.Add("User-Agent", "TVmaze test application");
        }

        public IEnumerable<ShowDTO> GetAllShows(int page, int pagesize)
        {
            var shows = _showsRepository.GetShows(page, pagesize).ProjectTo<ShowDTO>();

            return shows;
        }

        public Dictionary<string, int> GetShowsUpdate()
        {
            var notCompleted = false;
            var results = new Dictionary<string, int>();
            do
            {
                var updatesTask = client.GetAsync(baseUrl + showUpdatesUrl);
                updatesTask.Wait();

                HttpResponseMessage response = updatesTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonString = readTask.Result;

                    results = JsonNet.Deserialize<Dictionary<string, int>>(jsonString);
                }
                else if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    Thread.Sleep(sleepTimeout);
                    notCompleted = true;
                }
                else
                {
                }
            } while (notCompleted);

            return results;
        }

        public void UpdateShows(CancellationToken cancellationToken)
        {
            var showsUpdates = GetShowsUpdate();

            foreach (var showUpdate in showsUpdates)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                if (_showUpdatesRepository.CheckIfShowNeedsUpdating(showUpdate.Key, showUpdate.Value))
                {
                    var show = GetShowDetails(showUpdate.Key);
                    if (show != null)
                    {
                        _showsRepository.AddShow(show);
                        _showUpdatesRepository.AddShowUpdate(showUpdate.Key, showUpdate.Value);
                    }
                    else
                    {
                        _logger.LogWarning("No show found for {0}", showUpdate.Key);
                    }
                }
            }
        }

        private Show GetShowDetails(string key)
        {
            var url = baseUrl + String.Format(showDetailsUrl, key);
            try
            {
                var notCompleted = false;
                Show show = null;
                do
                {
                    var showTask = client.GetAsync(url);
                    showTask.Wait();

                    HttpResponseMessage response = showTask.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var jsonString = readTask.Result;

                        show = JsonNet.Deserialize<Show>(jsonString);
                        notCompleted = false;
                    }
                    else if (response.StatusCode == HttpStatusCode.TooManyRequests)
                    {
                        Thread.Sleep(sleepTimeout);
                        notCompleted = true;
                    }
                    else
                    {
                        _logger.LogWarning("Error getting show details id={0} url=[{1}] Status Code=[{2}] Reason=[{3}]", key, url, response.StatusCode, response.ReasonPhrase);
                        notCompleted = false;
                    }
                } while (notCompleted);

                return show;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting show details id=[{0}] url=[{1}]", key, url);
                return null;
            }
        }
    }
}
