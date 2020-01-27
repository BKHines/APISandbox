using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSandbox.Models;
using ApiSandbox.Repositories;
using ApiSandboxAPI.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiSandbox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("NgCORS")]
    [ServiceFilter(typeof(ApiSandboxInterceptor))]
    public class SquaresController : ControllerBase
    {
        private readonly ApiSandboxLogger _logger;

        public SquaresController(ILogger<SquaresController> logger)
        {
            _logger = new ApiSandboxLogger(logger);
        }


        // GET: api/Squares
        [HttpGet]
        public IEnumerable<SquareName> Get()
        {
            var repo = new SquaresRepository();
            return repo.GetSquareNames();
        }

        // POST: api/Squares
        [HttpPost]
        public void Post([FromBody] SquareName[] newnames)
        {
            var repo = new SquaresRepository();
            repo.SaveSquareNames(newnames);
        }
    }
}
