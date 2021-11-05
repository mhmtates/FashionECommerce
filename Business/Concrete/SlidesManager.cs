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
   public class SlidesManager:ISlidesService
    {
        private readonly IUnitOfWorks works;
        private readonly IMapper mapper;

        public SlidesManager(IUnitOfWorks _works, IMapper _mapper)
        {
            works = _works;
            mapper = _mapper;

        }
        public IResult Add(SlidesDto data)
        {
            try
            {
                works.SlidesRepository.Add(mapper.Map<Slides>(data));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Kayıt Başarılı");

            }
            catch (Exception)
            {

                return new Result(ResultStatus.Error, "Kayıt Başarısız");
            }

        }


        public IResult Delete(int Id)
        {
            try
            {
                works.SlidesRepository.Delete(works.SlidesRepository.GetByIdFirst(x => x.Id == Id));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Silme Başarılı");


            }
            catch (Exception)
            {

                return new Result(ResultStatus.Error, "Silme Başarısız");
            }
        }

        public IDataResult<IList<SlidesDto>> GetAll()
        {
            IList<SlidesDto> data = new List<SlidesDto>();

            foreach (var item in works.SlidesRepository.GetAll())
            {
                data.Add(mapper.Map<SlidesDto>(item));

            }
            if (data.Count > 0)
            {
                return new DataResult<IList<SlidesDto>>(ResultStatus.Success, data.Count + "Kayıt Listelendi", data);
            }
            else
            {

                return new DataResult<IList<SlidesDto>>(ResultStatus.Info, "Kayıt Bulunamadı", null);
            }

        }


        public IDataResult<SlidesDto> GetById(int Id)
        {
            var data = works.SlidesRepository.GetByIdFirst(x => x.Id == Id);
            if (data != null)
            {
                return new DataResult<SlidesDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<SlidesDto>(data));

            }
            else
            {
                return new DataResult<SlidesDto>(ResultStatus.Info, "Kayıt Bulunamadı.", null);

            }


        }

        public IResult Update(SlidesDto data)
        {
            try
            {
                works.SlidesRepository.Update(mapper.Map<Slides>(data));
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
