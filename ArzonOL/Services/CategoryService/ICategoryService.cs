using ArzonOL.Dtos.CategoryDto;
using ArzonOL.Entities;
using ArzonOL.ViewModels.Category;

namespace ArzonOL.Services.CategoryService;

public interface ICategoryService
{
    ValueTask<CategoryDtoView> CreateAsync(CreateOrUpdateCategoryDto model);
    ValueTask<CategoryDtoView> Update(Guid id, CreateOrUpdateCategoryDto model);
    ValueTask<CategoryDtoView> Remove(Guid id);
    ValueTask<List<CategoryDtoView>> GetAll();
    ValueTask<CategoryView> GetByIdAsync(Guid id);
}