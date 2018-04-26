using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiSandbox.Models;
using Microsoft.AspNetCore.Server;
using Newtonsoft.Json;

namespace ApiSandbox.DataAccess
{
    public class ShowsDataAccess
    {
        private Episode[] episodes;

        public ShowsDataAccess()
        {
            string shows = string.Empty;
            if (File.Exists(@".\Data\shows.json"))
            {
                shows = File.ReadAllText(@".\Data\shows.json");
            }
            else
            {
                shows = File.ReadAllText(@".\Data\shows-master.json");
                Reset();
            }
            episodes = JsonConvert.DeserializeObject<Episode[]>(shows);
        }

        public void Reset()
        {
            string showsMaster = File.ReadAllText(@".\Data\shows-master.json");
            using (var sw = new StreamWriter(@".\Data\shows.json", false))
            {
                sw.Write(showsMaster);
            }
        }

        public Episode GetEpisodeById(string id)
        {
            return episodes.FirstOrDefault(a => string.Equals(a.id, id, StringComparison.InvariantCultureIgnoreCase));
        }

        public Episode GetEpisodeByShowSeasonAndEpisode(string showname, int season, int episode)
        {
            return episodes.FirstOrDefault(a => string.Equals(a.showname, showname, StringComparison.InvariantCultureIgnoreCase)
                                            && a.season == season    
                                            && a.episode == episode);
        }

        public Episode[] GetEpisodes()
        {
            return episodes;
        }

        public Episode[] GetEpisodesByShow(string showname)
        {
            return episodes.Where(a => string.Equals(a.showname, showname, StringComparison.InvariantCultureIgnoreCase)).ToArray();
        }

        public Episode[] GetEpisodesByShowAndSeason(string showname, int season)
        {
            return episodes.Where(a => string.Equals(a.showname, showname, StringComparison.InvariantCultureIgnoreCase) && a.season == season).ToArray();
        }

        public Episode[] GetEpisodesByShowAndRange(string showname, DateTime startdate, DateTime enddate)
        {
            return episodes.Where(a => string.Equals(a.showname, showname, StringComparison.InvariantCultureIgnoreCase)
                    && DateTime.Parse(a.airdate).Date >= startdate.Date
                    && DateTime.Parse(a.airdate).Date <= enddate.Date).ToArray();
        }

        public string AddEpisode(dynamic episodeinfo)
        {
            var ep = new Episode()
            {
                id = $"{episodeinfo.showname.Replace(" ", "")}_{episodeinfo.overall}",
                showname = episodeinfo.showname,
                episodename = episodeinfo.episodename,
                season = episodeinfo.season,
                overall = episodeinfo.overall,
                episode = episodeinfo.episode,
                airdate = episodeinfo.airdate,
                watched = episodeinfo.watched,
                notes = null
            };

            var eps = episodes.ToList();
            eps.Add(ep);
            episodes = eps.ToArray();
            Save();
            return ep.id;
        }

        public bool DeleteEpisode(string id)
        {
            var origCount = episodes.Count();
            var ep = GetEpisodeById(id);

            if (ep == null)
            {
                return false;
            }
            else
            {
                episodes = episodes.Where(a => !string.Equals(a.id, id, StringComparison.InvariantCultureIgnoreCase)).ToArray();
                Save();
                return origCount != episodes.Count();
            }
        }

        public bool AddNote(string id, string note)
        {
            var ep = GetEpisodeById(id);

            if (ep == null)
            {
                return false;
            }
            else
            {
                if (ep.notes == null)
                {
                    ep.notes = new List<string>().ToArray();
                }
                var origCount = ep.notes.Count();

                var _notes = ep.notes.ToList();
                _notes.Add(note);
                ep.notes = _notes.ToArray();
                Save();
                return origCount != ep.notes.Count();
            }
        }

        public bool DeleteNode(string id, string note)
        {
            var ep = GetEpisodeById(id);

            if (ep == null)
            {
                return false;
            }
            else
            {
                if (ep.notes == null)
                {
                    return false;
                }
                else
                {
                    var origCount = ep.notes.Count();

                    ep.notes = ep.notes.Where(a => !string.Equals(a, note, StringComparison.InvariantCultureIgnoreCase)).ToArray();
                    Save();
                    return origCount != ep.notes.Count();
                }
            }
        }

        public bool SetEpisodeWatchedFlag(string id, bool watched)
        {
            var ep = GetEpisodeById(id);

            if (ep == null)
            {
                return false;
            }
            else
            {
                ep.watched = watched;
                Save();
                return true;
            }
        }

        private void Save()
        {
            string shows = JsonConvert.SerializeObject(episodes);
            using (var sw = new StreamWriter(@".\Data\shows.json", false))
            {
                sw.Write(shows);
            }
        }
    }
}
