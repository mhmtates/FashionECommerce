using Core.Results.Abstract;
using Entities.Dto;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IProductImagesService
    {
        IDataResult<IList<ProductImagesDto>> GetAll(int id);
        ProductImagesDto GetByIdFirst(int id);
        IResult Add(ProductImagesDto data);
        IResult Update(ProductImagesDto data);
        IResult Delete(int Id);
    }
}
