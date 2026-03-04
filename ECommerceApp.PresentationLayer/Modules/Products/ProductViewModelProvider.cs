using AutoMapper;
using ECommerceApp.BusinessLayer.Modules.Products.Interface;
using ECommerceApp.PresentationLayer.Modules.Products.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Products.ViewModels;

namespace ECommerceApp.PresentationLayer.Modules.Products
{
    public class ProductViewModelProvider : IProductViewModelProvider
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductViewModelProvider(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductListViewModel>> GetAllAsync()
        {
            var productList = await _productService.GetAllAsync();
            return _mapper.Map<IReadOnlyList<ProductListViewModel>>(productList);
        }

        public async Task<ProductEditViewModel?> GetByIdAsync(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return null;

            return _mapper.Map<ProductEditViewModel>(product);
        }

        public async Task AddAsync(ProductCreateViewModel productVm)
        {
            var product = _mapper.Map<ECommerceApp.Domain.Entities.Product>(productVm);
            await _productService.AddAsync(product);
        }

        public async Task UpdateAsync(ProductEditViewModel productVm)
        {
            var product = _mapper.Map<ECommerceApp.Domain.Entities.Product>(productVm);
            await _productService.UpdateAsync(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }

            return await _productService.DeleteAsync(product);
        }
    }
}
