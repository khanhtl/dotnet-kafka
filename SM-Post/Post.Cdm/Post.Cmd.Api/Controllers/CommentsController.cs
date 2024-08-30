using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Cmd.Api.Dtos;
using Post.Common.Dtos;

namespace Post.Cmd.Api.Controllers;

[Route("api/v1/posts/{postId:guid}/[controller]")]
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
    public async Task<ActionResult> AddCommentAsync(Guid postId, AddCommentCommand command)
    {
        try
        {
            command.Id = postId;
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
                Id = postId,
                Message = SAFE_ERROR_MESSAGE,
            });
        }
    }
    [HttpPut("{commentId}")]
    public async Task<ActionResult>EditCommentAsync(Guid postId, Guid commentId, EditCommentCommand command)
    {
        try
        {
            command.Id = postId;
            command.CommentId = commentId;
            await _commandDispatcher.SendAsync(command);
            return Ok(new BaseResponse
            {
                Message = "Edit comment request completed successfully!"
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
            const string SAFE_ERROR_MESSAGE = "Error while processing request to edit comment to a post";
            _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);
            return StatusCode(StatusCodes.Status500InternalServerError, new PostResponse
            {
                Id = postId,
                Message = SAFE_ERROR_MESSAGE,
            });
        }
    }
    [HttpDelete("{commentId}")]
    public async Task<ActionResult> RemoveCommentAsync(Guid postId, Guid commentId, RemoveCommentCommand command)
    {
        try
        {
            command.Id = postId;
            command.CommentId = commentId;
            await _commandDispatcher.SendAsync(command);
            return Ok(new BaseResponse
            {
                Message = "Remove comment request completed successfully!"
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
            const string SAFE_ERROR_MESSAGE = "Error while processing request to remove comment to a post";
            _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);
            return StatusCode(StatusCodes.Status500InternalServerError, new PostResponse
            {
                Id = postId,
                Message = SAFE_ERROR_MESSAGE,
            });
        }
    }
}
