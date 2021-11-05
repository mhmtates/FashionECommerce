using AutoMapper;
using Business.Abstract;
using Core.Results.Abstract;
using Core.Results.ComplexTypes;
using Core.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class OrderInformationsManager: IOrderInformationsService
    {
        private readonly IUnitOfWorks works;
        private readonly IMapper mapper;

        public OrderInformationsManager(IMapper _mapper, IUnitOfWorks _work)
        {
            works = _work;
            mapper = _mapper;
        }

        public IResult Add(OrderInformationsDto data)
        {
            try
            {
                works.OrderInformationsRepository.Add(mapper.Map<OrderInformations>(data));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Kayıt Başarılı.");
            }
            catch (Exception e)
            {
                string mesa = e.Message;
                return new Result(ResultStatus.Error, "Kayıt Başarısız.");
            }
        }

        public IResult Delete(int Id)
        {
            try
            {
                works.OrderInformationsRepository.Delete(works.OrderInformationsRepository.GetByIdFirst(x => x.Id == Id));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Silme Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Silme Başarısız");
            }
        }
        public IDataResult<IList<OrderInformationsDto>> GetAll()
        {
            IList<OrderInformationsDto> data = new List<OrderInformationsDto>();
            foreach (var item in works.OrderInformationsRepository.GetAll())
            {
                data.Add(mapper.Map<OrderInformationsDto>(item));
            }
            if (data.Count > 0)
            {
                return new DataResult<IList<OrderInformationsDto>>(ResultStatus.Success, data.Count + " Kayıt Listelendi", data);
            }
            else
            {
                return new DataResult<IList<OrderInformationsDto>>(ResultStatus.Info, "Kayıt Bulunamadı.", null);
            }
        }

        public IDataResult<OrderInformationsDto> GetById(int Id)
        {
            var data = works.OrderInformationsRepository.GetByIdFirst(x => x.Id == Id);
            if (data != null)
            {
                return new DataResult<OrderInformationsDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<OrderInformationsDto>(data));
            }
            else
            {
                return new DataResult<OrderInformationsDto>(ResultStatus.Info, "Kayıt Bulunamadı", null);
            }
        }

        public IResult Update(OrderInformationsDto data)
        {
            try
            {
                works.OrderInformationsRepository.Update(mapper.Map<OrderInformations>(data));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Güncelleme Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Güncelleme Başarısız");
            }
        }
    }
}
