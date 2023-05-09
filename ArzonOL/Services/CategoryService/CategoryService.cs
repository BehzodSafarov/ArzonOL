using System;
using ArzonOL.Entities;
using ArzonOL.Exceptions;
using ArzonOL.Dtos.CategoryDto;
using ArzonOL.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using ArzonOL.ViewModels.Category;

namespace ArzonOL.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly ILogger<CategoryService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(
        ILogger<CategoryService> logger,
        IUnitOfWork unitOfWork
    )
    {
        _logger = logger ;
        _unitOfWork = unitOfWork ;
    }
    public async  ValueTask<CategoryDtoView> CreateAsync(CreateOrUpdateCategoryDto model)
    {
        var categoryNames = _unitOfWork.CategoryRepository.GetAll().Select(c => c.Name);

        foreach(var categoryName in categoryNames)
        {
            if(categoryName!.ToLower() == model.Name!.ToLower())
                  throw new BadRequestException("category already exists");
        }
        
        var category = new ProductCategoryEntity
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Description = model.Description,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var createCategory =await  _unitOfWork.CategoryRepository.AddAsync(category);
 
        return createCategory.Adapt<CategoryDtoView>();
    }

    public async ValueTask<List<CategoryDtoView>> GetAll()
    {

        var categories = _unitOfWork.CategoryRepository.GetAll();

        if(categories is null)
            throw new("There are no categories yet");
    
        var categoriestDto =await categories.Select(category => category.Adapt<CategoryDtoView>()).ToListAsync();

        return categoriestDto;           
    }

    public async ValueTask<CategoryView> GetByIdAsync(Guid id)
    {
        var existingCategory = _unitOfWork.CategoryRepository.GetById(id);
        
        if(existingCategory is null)
                throw new BadRequestException("Category with given Id not found.");
                
        var categoryApproachList = await _unitOfWork.CategoryApproachRepository
                                            .GetAll().Where(x => x.ProductCategoryId == id).ToListAsync();
        
        var categoryApproachListView = categoryApproachList
                    .Select(approach => approach.Adapt<CategoryApproachDtoView>()).ToList();

        var categoryDtoView = new CategoryView
        {
            Id = existingCategory.Id,
            Name = existingCategory.Name,
            Description = existingCategory.Description,
            CreatedAt = existingCategory.CreatedAt,
            UpdatedAt = existingCategory.UpdatedAt,
            Approaches = categoryApproachListView
        };

        return categoryDtoView;
    }

    public async ValueTask<CategoryDtoView> Remove(Guid id)
    {
        var existingCategory =  _unitOfWork.CategoryRepository.GetById(id);
                    
        if(existingCategory is null)
                throw new BadRequestException("Category with given Id not found.");
            
        var removeCategory = await _unitOfWork.CategoryRepository.Remove(existingCategory);

        if(removeCategory is null)
                throw new BadHttpRequestException("Removing the quiz failed. Contact support.");
            
        return removeCategory.Adapt<CategoryDtoView>();
    }

    public async ValueTask<CategoryDtoView> Update(Guid id, CreateOrUpdateCategoryDto model)
    {
        var existingCategory =  _unitOfWork.CategoryRepository.GetById(id);
                    
        if(existingCategory is null)
                throw new BadRequestException("Category with given Id not found.");

        var categoryNames = _unitOfWork.CategoryRepository.GetAll().Select(c => c.Name);

        foreach(var categoryName in categoryNames)
        {
            if(categoryName!.ToLower() == model.Name!.ToLower())
                  throw new BadRequestException("category already exists");
        }
        
        existingCategory.Name = model.Name;
        existingCategory.Description = model.Description;
        existingCategory.UpdatedAt = DateTime.Now;

        var updateCategory = await _unitOfWork.CategoryRepository.Update(existingCategory);

        return updateCategory.Adapt<CategoryDtoView>();
    }
}