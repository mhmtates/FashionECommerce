using Core.Results.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderInformationsService
    {
        IDataResult<IList<OrderInformationsDto>> GetAll();
        IDataResult<OrderInformationsDto> GetById(int Id);
        IResult Add(OrderInformationsDto data);
        IResult Update(OrderInformationsDto data);
        IResult Delete(int Id);
    }
}
