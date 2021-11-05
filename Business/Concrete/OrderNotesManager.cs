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
    public class OrderNotesManager : IOrderNotesService

    {
        private readonly IUnitOfWorks works;
        private readonly IMapper mapper;

        public OrderNotesManager(IUnitOfWorks _works, IMapper _mapper)
        {
            works = _works;
            mapper = _mapper;
        }



        public IResult Add(OrderNotesDto data)
        {
            try
            {
                works.OrderNotesRepository.Add(mapper.Map<OrderNotes>(data));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Kayıt Başarılı.");

            }
            catch (Exception)
            {

                return new Result(ResultStatus.Error, "Kayıt Başarısız.");
            }
        }

        public IResult Delete(int Id)
        {
            try
            {
                works.OrderNotesRepository.Delete(works.OrderNotesRepository.GetByIdFirst(x => x.Id == Id));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Silme Başarılı.");
            }
            catch (Exception)
            {

                return new Result(ResultStatus.Error, "Silme Başarısız.");
            }
        }

        public IDataResult<IList<OrderNotesDto>> GetAll(int id)
        {
            IList<OrderNotesDto> data = new List<OrderNotesDto>();

            foreach (var item in works.OrderNotesRepository.GetAll())
            {
                data.Add(mapper.Map<OrderNotesDto>(item));
            }

            if (data.Count > 0)
            {
                return new DataResult<IList<OrderNotesDto>>(ResultStatus.Success, data.Count + "Kayıt Listelendi", data);
            }

            else
            {
                return new DataResult<IList<OrderNotesDto>>(ResultStatus.Info, "Kayıt Bulunamadı.", null);

            }

        }

        public IDataResult<OrderNotesDto> GetById(int Id)
        {
            var data = works.OrderNotesRepository.GetByIdFirst(x => x.Id == Id);
            if (data != null)
            {
                return new DataResult<OrderNotesDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<OrderNotesDto>(data));

            }
            else
            {
                return new DataResult<OrderNotesDto>(ResultStatus.Info, "Kayıt Bulunamadı", null);
            }

        }

        public IResult Update(OrderNotesDto data)
        {
            try
            {
                works.OrderNotesRepository.Update(mapper.Map<OrderNotes>(data));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Güncelleme Başarılı.");


            }
            catch (Exception)
            {

                return new Result(ResultStatus.Error, "Güncelleme Başarısız.");
            }
        }
    }
}


