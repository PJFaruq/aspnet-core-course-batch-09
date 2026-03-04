using ECommerceApp.PresentationLayer.Modules.Products.ViewModels;

namespace ECommerceApp.PresentationLayer.Modules.Products.Interfaces
{
    public interface IProductViewModelProvider
    {
        Task<ProductEditViewModel?> GetByIdAsync(int id);
        Task<IReadOnlyList<ProductListViewModel>> GetAllAsync();
        Task AddAsync(ProductCreateViewModel productVm);
        Task UpdateAsync(ProductEditViewModel productVm);
        Task<bool> DeleteAsync(int id);
    }
}
