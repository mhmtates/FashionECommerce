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
    public class VariantsManager : IVariantsService
    {
        private readonly IUnitOfWorks works;
        private readonly IMapper mapper;

        public VariantsManager(IUnitOfWorks _works, IMapper _mapper)
        {
            works = _works;
            mapper = _mapper;
        }
        public IResult Add(VariantsDto variantsDto)
        {
            try
            {
                works.VariantsRepository.Add(mapper.Map<Variants>(variantsDto));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Kayıt Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Kayıt Başarısız");
            }
        }
        public IResult Delete(int id)
        {
            try
            {
                works.VariantsRepository.Delete(works.VariantsRepository.GetByIdFirst(x => x.Id == id));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Silme Başarılı");

            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Silme Başarısız");
            }
        }
        public IDataResult<IList<VariantsDto>> GetAll(int id)
        {
            IList<VariantsDto> data = new List<VariantsDto>();
            foreach (var item in works.VariantsRepository.GetAll(x => x.ProductsId == id))
            {
                data.Add(mapper.Map<VariantsDto>(item));

            }
            if (data.Count > 0)
            {
                return new DataResult<IList<VariantsDto>>(ResultStatus.Success, data.Count + " Kayıt Listelendi", data);
            }
            else
            {
                return new DataResult<IList<VariantsDto>>(ResultStatus.Info, "Kayıt Bulunamadı", new List<VariantsDto>());
            }
        }
        public VariantsDto GetById(int id)
        {
            return mapper.Map<VariantsDto>(works.VariantsRepository.GetByIdFirst(x => x.Id == id));
        }
        public IResult Update(VariantsDto variantsDto)
        {
            try
            {
                works.VariantsRepository.Update(mapper.Map<Variants>(variantsDto));
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
