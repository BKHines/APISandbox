using ApiSandbox.DataAccess;
using ApiSandbox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSandbox.Repositories
{
    public class ShowsRepository
    {
        private ShowsDataAccess showDA;

        public ShowsRepository()
        {
            showDA = new ShowsDataAccess();
        }

        public Episode GetEpisodeById(string id)
        {
            return showDA.GetEpisodeById(id);
        }

        public Episode GetEpisodeByShowSeasonAndEpisode(string id, int season, int episode)
        {
            return showDA.GetEpisodeByShowSeasonAndEpisode(id, season, episode);
        }

        public Episode[] GetEpisodes()
        {
            return showDA.GetEpisodes();
        }

        public Episode[] GetEpisodesByShow(string showname)
        {
            return showDA.GetEpisodesByShow(showname);
        }

        public Episode[] GetEpisodesByShowAndSeason(string showname, int season)
        {
            return showDA.GetEpisodesByShowAndSeason(showname, season);
        }

        public Episode[] GetEpisodesByShowAndRange(string showname, DateTime startdate, DateTime enddate)
        {
            return showDA.GetEpisodesByShowAndRange(showname, startdate, enddate);
        }

        public string AddEpisode(string showname, int season, int episode, int overall, string episodename, string airdate, bool watched)
        {
            var newEp = new {
                showname = showname,
                season = season,
                episode = episode,
                overall = overall,
                episodename = episodename,
                airdate = airdate,
                watched = watched
                };

            return showDA.AddEpisode(newEp);
        }

        public bool DeleteEpisode(string id)
        {
            return showDA.DeleteEpisode(id);
        }

        public bool AddNote(string id, string note)
        {
            return showDA.AddNote(id, note);
        }

        public bool DeleteNote(string id, string note)
        {
            return showDA.DeleteNode(id, note);
        }

        public bool SetEpisodeWatchedFlag(string id, bool watched)
        {
            return showDA.SetEpisodeWatchedFlag(id, watched);
        }

        public void ResetData()
        {
            showDA.Reset();
        }
    }
}
