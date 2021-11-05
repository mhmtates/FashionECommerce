using Core.Results.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IOrdersService
    {
        IDataResult<IList<OrdersUpdateListDto>> FiveTableGetAll(int Id); // İlişkilendirilmiş 5 Tablo getirecek.
        IDataResult<IList<OrdersDto>> GetAll(string Durumu);
        IDataResult<OrdersUpdateDto> GetById(int Id);

        IDataResult<IList<OrdersDto>> GetByCustomerId(int CustomerId);
        IResult Add(OrdersUpdateDto data);
        IResult Update(OrdersUpdateDto data);
        IResult Delete(int Id);
    }
}
