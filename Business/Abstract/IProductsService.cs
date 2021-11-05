using Core.Results.Abstract;
using Entities.Dto;
using System.Collections.Generic;
namespace Business.Abstract
{
    public interface IProductsService
    {
       
        IDataResult<IList<ProductsDto>> GetAll();
        IDataResult<IList<ProductsDto>> KategoriyeGoreUrunGetirme(int CategoryId);
        IDataResult<IList<ProductsDto>> GetAllKampanya();
        IDataResult<ProductsUpdateDto> GetById(int Id);
        IResult Add(ProductsUpdateDto data);
        IResult Update(ProductsUpdateDto data);
        IResult Delete(int Id);
        IResult DeleteImages(int Id);
        IDataResult<int> SearchId(string Name, decimal Price, int Stock);
        IDataResult<IList<ProductsDto>> Search(string Keywords);
    }
}
