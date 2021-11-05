using Core.Results.Abstract;
using Entities.Dto;
using System.Collections.Generic;

namespace Business.Abstract
{
  public interface ISlidesService
    {
        IDataResult<IList<SlidesDto>> GetAll();
        IDataResult<SlidesDto> GetById(int Id);
        IResult Add(SlidesDto data);
        IResult Update(SlidesDto data);
        IResult Delete(int Id);
    }
}
