using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TrainComponentManagement.Data;
using TrainComponentManagement.Models;
using TrainComponentManagement.Services;

namespace TrainComponentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainComponentsController : ControllerBase
    {
        private readonly ITrainComponentService  _componentService;

        public TrainComponentsController(ITrainComponentService  componentService)
        {
            _componentService = componentService;
        }

        // GET: api/traincomponents - List with Pagination/Search [cite: 9, 16]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainComponentDto>>> GetTrainComponents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            var (components, totalItems) =
                await _componentService.GetAllComponentsAsync(pageNumber, pageSize, searchTerm);

            var paginationMetadata = new
            {
                totalCount = totalItems,
                pageSize = pageSize,
                currentPage = pageNumber,
                totalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(components);
        }

        [HttpGet("{id}")] // Handles GET requests to /api/traincomponents/{id}
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrainComponentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TrainComponentDto>> GetTrainComponent(int id)
        {
            var component = await _componentService.GetComponentByIdAsync(id);

            if (component == null)
            {
                return NotFound($"Component with Id {id} not found.");
            }

            return Ok(component);
        }

        [HttpPost] // Handles POST requests to /api/traincomponents
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrainComponentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TrainComponentDto>> CreateTrainComponent(
            [FromBody] CreateTrainComponentDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdComponent = await _componentService.CreateComponentAsync(createDto);
                return CreatedAtAction(nameof(GetTrainComponent), new { id = createdComponent.Id }, createdComponent);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrainComponentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]                          
        [ProducesResponseType(StatusCodes.Status404NotFound)]                           
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  
        public async Task<ActionResult<TrainComponentDto>> UpdateTrainComponent(int id, [FromBody] UpdateTrainComponentDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                // If DTO validation fails, return 400 Bad Request with details
                return BadRequest(ModelState);
            }
            try
            {
                var updatedComponent = await _componentService.UpdateComponentAsync(id, updateDto);
                return Ok(updatedComponent);
            }
            catch (KeyNotFoundException ex) 
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex) // Catch specific rule violation exception from service
            {
                // If a business rule was violated (e.g., quantity issue), return 400 Bad Request
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex) // Catch any other unexpected errors
            {
                // Return 500 Internal Server Error for generic failures
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpDelete("{id}")] // Handles DELETE requests to /api/traincomponents/{id}
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTrainComponent(int id)
        {
            try
            {
                
                await _componentService.DeleteComponentAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex) 
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
