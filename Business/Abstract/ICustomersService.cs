using Core.Results.Abstract;
using Entities.Dto;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICustomersService
    {
        IDataResult<IList<CustomersDto>> GetAll();
        IDataResult<CustomersUpdateDto> GetById(int Id);
        IDataResult<CustomersDto> Login(string EMail,string Telefon,string Sifre);
        IResult Add(CustomersUpdateDto data);
        IResult Update(CustomersUpdateDto data);
        IResult Delete(int Id);
    }
}
