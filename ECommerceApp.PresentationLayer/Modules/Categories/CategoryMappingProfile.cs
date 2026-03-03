using AutoMapper;
using ECommerceApp.Domain.Entities;
using ECommerceApp.PresentationLayer.Modules.Categories.ViewModels;

namespace ECommerceApp.PresentationLayer.Modules.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // ViewModel -> Enitity
            CreateMap<CategoryCreateViewModel, Category>();
            CreateMap<CategoryEditViewModel, Category>();

            // Entity -> ViewModel
            CreateMap<Category, CategoryEditViewModel>();
            CreateMap<Category, CategoryListViewModel>();
        }
    }
}
