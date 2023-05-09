using ArzonOL.Dtos.CategoryDto;
using ArzonOL.Services.CategoryService;
using ArzonOL.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace ArzonOL.Controllers.Category;

[ApiController]
[Route("api/[controller]")]
public class CategoryController:ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryService _categoryService;
    public CategoryController(
        ILogger<CategoryController> logger,
        ICategoryService categoryService
        )
    {
        _logger = logger ;
        _categoryService = categoryService ;

    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryDtoView>))]
    public async Task<IActionResult> GetCategories()
                        => Ok(await _categoryService.GetAll());

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDtoView))]
    public async Task<IActionResult> PostCategory(CreateOrUpdateCategoryDto dto)
    {
        try
        {
            if(!ModelState.IsValid) return BadRequest(dto);
      
            return Ok(await _categoryService.CreateAsync(dto));
        }
        catch (System.Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryView))]
    public async Task<IActionResult> GetCategory(Guid id)
                         => Ok(await _categoryService.GetByIdAsync(id));

   
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCategory(Guid id)
                         => Ok(await _categoryService.Remove(id));

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDtoView))]
    public async Task<IActionResult> UpdateCategory([FromRoute]Guid id, [FromBody]CreateOrUpdateCategoryDto dto)
    {
        if(!ModelState.IsValid)
                  return BadRequest(dto);
        
        var updateCategory = await _categoryService.Update(id, dto);

        return Ok(updateCategory);
    }
}