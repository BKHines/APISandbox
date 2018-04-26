using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSandbox.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSandbox.Controllers
{
    [Produces("application/json")]
    public class ShowsController : Controller
    {
        [HttpGet("api/shows/getepisodes")]
        public IActionResult GetEpisodes()
        {
            var repo = new ShowsRepository();
            var eps = repo.GetEpisodes();
            return Ok(eps);
        }

        [HttpPost("api/shows/retrieveepisodebyid")]
        public IActionResult RetrieveEpisodeById([FromBody] dynamic req)
        {
            string id;

            try
            {
                id = req.id.ToString();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            try
            {
                var repo = new ShowsRepository();
                var ep = repo.GetEpisodeById(id);
                return Ok(ep);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("api/shows/retrieveepisodesbyshow")]
        public IActionResult RetrieveEpisodesByShow([FromBody] dynamic req)
        {
            string showname;

            try
            {
                showname = req.showname.ToString();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            try
            {
                var repo = new ShowsRepository();
                var eps = repo.GetEpisodesByShow(showname);
                return Ok(eps);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("api/shows/retrieveepisodesbyshowandseason")]
        public IActionResult RetrieveEpisodesByShowAndSeason([FromBody] dynamic req)
        {
            string showname;
            int season;

            try
            {
                showname = req.showname.ToString();
                season = req.season;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            try
            {
                var repo = new ShowsRepository();
                var eps = repo.GetEpisodesByShowAndSeason(showname, season);
                return Ok(eps);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("api/shows/retrieveepisodebyshowseasonandepisode")]
        public IActionResult RetrieveEpisodeByShowSeasonAndEpisode([FromBody] dynamic req)
        {
            string showname;
            int season, episode;

            try
            {
                showname = req.showname.ToString();
                season = req.season;
                episode = req.episode;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            try
            {
                var repo = new ShowsRepository();
                var ep = repo.GetEpisodeByShowSeasonAndEpisode(showname, season, episode);
                return Ok(ep);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("api/shows/retrieveepisodesbyshowanddates")]
        public IActionResult RetrieveEpisodesByShowAndDates([FromBody] dynamic req)
        {
            string showname;
            DateTime startdate, enddate;

            try
            {
                showname = req.showname.ToString();
                startdate = req.startdate;
                enddate = req.enddate;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            try
            {
                var repo = new ShowsRepository();
                var eps = repo.GetEpisodesByShowAndRange(showname, startdate, enddate);
                return Ok(eps);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("api/shows/addepisode")]
        public IActionResult AddEpisode([FromBody] dynamic req)
        {
            string showname, episodename, airdate;
            int season, episode, overall;
            bool watched;

            try
            {
                showname = req.showname.ToString();
                episodename = req.episodename.ToString();
                airdate = req.airdate.ToString();
                season = req.season;
                episode = req.episode;
                overall = req.overall;
                watched = req.watched;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            try
            {
                var repo = new ShowsRepository();
                var epId = repo.AddEpisode(showname, season, episode, overall, episodename, airdate, watched);
                return Ok(epId);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("api/shows/deleteepisode")]
        public IActionResult DeleteEpisode([FromBody] dynamic req)
        {
            string id;

            try
            {
                id = req.id.ToString();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            try
            {
                var repo = new ShowsRepository();
                var epDeleted = repo.DeleteEpisode(id);
                return Ok(epDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("api/shows/addnote")]
        public IActionResult AddNote([FromBody] dynamic req)
        {
            string id, note;

            try
            {
                id = req.id.ToString();
                note = req.note.ToString();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            try
            {
                var repo = new ShowsRepository();
                var noteAdded = repo.AddNote(id, note);
                return Ok(noteAdded);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("api/shows/deletenote")]
        public IActionResult DeleteNote([FromBody] dynamic req)
        {
            string id, note;

            try
            {
                id = req.id.ToString();
                note = req.note.ToString();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            try
            {
                var repo = new ShowsRepository();
                var noteDeleted = repo.DeleteNote(id, note);
                return Ok(noteDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("api/shows/setepisodewatchedflag")]
        public IActionResult SetEpisodeWatchedFlag([FromBody] dynamic req)
        {
            string id;
            Boolean watched;

            try
            {
                id = req.id.ToString();
                watched = req.watched;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            try
            {
                var repo = new ShowsRepository();
                var episodeUpdated = repo.SetEpisodeWatchedFlag(id, watched);
                return Ok(episodeUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("api/shows/resetdata")]
        public IActionResult ResetData()
        {
            try
            {
                var repo = new ShowsRepository();
                repo.ResetData();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
