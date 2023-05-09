using ArzonOL.Dtos.CategoryDto;
using ArzonOL.ViewModels.Category;

namespace ArzonOL.Services.CategoryService;

public interface ICategoryApproachService
{
    ValueTask<CategoryApproachDtoView> CreateAsync(CreateOrUpdateCategoryApproachDto model);
    ValueTask<CategoryApproachDtoView> Update(Guid id, CreateOrUpdateCategoryApproachDto model);
    ValueTask<CategoryApproachDtoView> Remove(Guid id);
    ValueTask<List<CategoryApproachDtoView>> GetAll();
    ValueTask<CategoryApproachView> GetByIdAsync(Guid id);
}