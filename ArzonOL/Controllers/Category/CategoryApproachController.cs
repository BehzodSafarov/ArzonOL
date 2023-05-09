using ArzonOL.Dtos.CategoryDto;
using ArzonOL.Services.CategoryService;
using ArzonOL.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace ArzonOL.Controllers.Category;

[ApiController]
[Route("api/[controller]")]
public class CategoryApproachController:ControllerBase
{
    private readonly ILogger<CategoryApproachController> _logger;
    private readonly ICategoryApproachService _categoryApproachService;

    public CategoryApproachController(
        ILogger<CategoryApproachController> logger,
        ICategoryApproachService categoryApproachService
        )
    {
        _logger = logger ;
        _categoryApproachService = categoryApproachService ;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryApproachDtoView>))]
    public async Task<IActionResult> GetCategories()
                        => Ok(await _categoryApproachService.GetAll());

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryApproachDtoView))]
    public async Task<IActionResult> PostCategoryApproach(CreateOrUpdateCategoryApproachDto dto)
    {
        try
        {
            if(!ModelState.IsValid) return BadRequest(dto);
      
            return Ok(await _categoryApproachService.CreateAsync(dto));
        }
        catch (System.Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryView))]
    public async Task<IActionResult> GetCategoryApproach(Guid id)
                         => Ok(await _categoryApproachService.GetByIdAsync(id));

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCategoryApproach(Guid id)
                         => Ok(await _categoryApproachService.Remove(id));

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryApproachDtoView))]
    public async Task<IActionResult> UpdateCategory([FromRoute]Guid id, [FromBody]CreateOrUpdateCategoryApproachDto dto)
    {
        if(!ModelState.IsValid)
                  return BadRequest(dto);
        
        var updateCategoryApproach = await _categoryApproachService.Update(id, dto);

        return Ok(updateCategoryApproach);
    }
    
}