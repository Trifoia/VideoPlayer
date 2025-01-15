using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Trifoia.Module.VideoPlayer.Repository;
using Oqtane.Controllers;
using System.Net;
using System.Threading.Tasks;
using System;

namespace Trifoia.Module.VideoPlayer.Controllers;

[Route(ControllerRoutes.ApiRoute)]
public class VideoPlayerController : ModuleControllerBase
{
    private readonly VideoPlayerRepository _VideoPlayerRepository;

    public VideoPlayerController(VideoPlayerRepository VideoPlayerRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
    {
        _VideoPlayerRepository = VideoPlayerRepository;
    }

    // GET: api/<controller>?moduleid=x
    [HttpGet]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult<IEnumerable<Models.VideoPlayer>>> Get(){
        try{
            var data = _VideoPlayerRepository.GetVideoPlayers();
            return Ok(data);
        }
        catch(Exception ex){
            var errorMessage = $"Repository Error Get Attempt VideoPlayer";
            _logger.Log(LogLevel.Error, this, LogFunction.Read, errorMessage);
            return StatusCode(500);
        }
    }

    // GET api/<controller>/5
    [HttpGet("{id}")]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult<Models.VideoPlayer>> Get(int id)
    {
        try {
            var data = _VideoPlayerRepository.GetVideoPlayer(id);
            return Ok(data);
        }
        catch (Exception ex)       { 
            _logger.Log(LogLevel.Error, this, LogFunction.Read, "Failed VideoPlayer Get Attempt {id}", id);
            return StatusCode(500);
        }
    }

    // POST api/<controller>
    [HttpPost]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult<Models.VideoPlayer>> Post([FromBody] Models.VideoPlayer item)
    {
        if (ModelState.IsValid)
        {
            try{
                item = _VideoPlayerRepository.AddVideoPlayer(item);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "VideoPlayer Added {VideoPlayer}", item);
            }
            catch (Exception ex) {
                _logger.Log(LogLevel.Error, this, LogFunction.Read, "Failed VideoPlayer Add Attempt {item} Message {Message} ", item, ex.Message);
                return StatusCode(500);
            }
        }
        else
        {
            _logger.Log(LogLevel.Error, this, LogFunction.Create, "Invaid VideoPlayer Post Attempt {item}", item);
            return BadRequest();
        }
        return Ok(item);
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult<Models.VideoPlayer>> Put(int id, [FromBody] Models.VideoPlayer item)
    {
        if (ModelState.IsValid && _VideoPlayerRepository.GetVideoPlayer(item.VideoPlayerId, false) != null)
        {
            item = _VideoPlayerRepository.UpdateVideoPlayer(item);
            _logger.Log(LogLevel.Information, this, LogFunction.Update, "VideoPlayer Updated {item}", item);
            return Ok(item);
        }
        else
        {
            _logger.Log(LogLevel.Error, this, LogFunction.Update, "Unauthorized VideoPlayer Put Attempt {item}", item);
            return BadRequest();
        }
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult> Delete(int id)
    {
        var data = _VideoPlayerRepository.GetVideoPlayer(id);
        if (data is null)
        {
            _logger.Log(LogLevel.Error, this, LogFunction.Delete, "Failed VideoPlayer Delete Attempt {VideoPlayerId}", id);
            return NotFound();
        }

        _VideoPlayerRepository.DeleteVideoPlayer(id);
        _logger.Log(LogLevel.Information, this, LogFunction.Delete, "VideoPlayer Deleted {VideoPlayerId}", id);
        return Ok();
    
    }
}