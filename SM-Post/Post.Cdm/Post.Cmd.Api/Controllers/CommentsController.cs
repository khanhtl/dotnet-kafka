using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Cmd.Api.Dtos;
using Post.Common.Dtos;

namespace Post.Cmd.Api.Controllers;

[Route("api/v1/posts/{id:guid}/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ILogger<CommentsController> _logger;
    private readonly ICommandDispatcher _commandDispatcher;

    public CommentsController(ILogger<CommentsController> logger, ICommandDispatcher commandDispatcher)
    {
        _logger = logger;
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<ActionResult> AddCommentAsync(Guid id, AddCommentCommand command)
    {
        try
        {
            command.Id = id;
            await _commandDispatcher.SendAsync(command);
            return Ok(new BaseResponse
            {
                Message = "Add comment request completed successfully!"
            });
        }
        catch (InvalidOperationException ex)
        {
            _logger.Log(LogLevel.Warning, ex, "Client made a bad request");
            return BadRequest(new BaseResponse
            {
                Message = ex.Message,
            });
        }
        catch (AggregateNotFoundException ex)
        {
            _logger.Log(LogLevel.Warning, ex, "Could not retreive aggregate, client passed an incorrect Id targeting the aggregate!");
            return BadRequest(new BaseResponse
            {
                Message = ex.Message,
            });
        }
        catch (Exception ex)
        {
            const string SAFE_ERROR_MESSAGE = "Error while processing request to add comment to a post";
            _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);
            return StatusCode(StatusCodes.Status500InternalServerError, new PostResponse
            {
                Id = id,
                Message = SAFE_ERROR_MESSAGE,
            });
        }
    }
}
