﻿using AutoMapper;
using DynamicsAdapter.Web.PersonSearch;
using DynamicsAdapter.Web.SearchAgency.Models;
using Fams3Adapter.Dynamics.SearchApiRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using System.Threading.Tasks;

namespace DynamicsAdapter.Web.SearchAgency
{
    [Route("[controller]")]
    [ApiController]
    public class AgencyRequestController : ControllerBase
    {
        private readonly ILogger<PersonSearchController> _logger;
        private readonly ISearchApiRequestService _searchApiRequestService;
        private readonly IMapper _mapper;

        public AgencyRequestController(
                ISearchApiRequestService searchApiRequestService,
                ILogger<PersonSearchController> logger,
                IMapper mapper
                )
        {

            _searchApiRequestService = searchApiRequestService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CreateSearchRequest/{key}")]
        [OpenApiTag("Agency Search Reqeust API")]
        public async Task<IActionResult> CreateSearchRequest(string key, [FromBody]SearchRequestOrdered searchRequestOrdered)
        {

            return Ok();
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("UpdateSearchRequest/{key}")]
        [OpenApiTag("Agency Search Reqeust API")]
        public async Task<IActionResult> UpdateSearchRequest(string key, [FromBody]SearchRequestOrdered personCompletedEvent)
        {
            return Ok();
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CancelSearchRequest/{key}")]
        [OpenApiTag("Agency Search Reqeust API")]
        public async Task<IActionResult> CancelSearchRequest(string key, [FromBody]SearchRequestOrdered personCompletedEvent)
        {
            return Ok();
        }
    }
}