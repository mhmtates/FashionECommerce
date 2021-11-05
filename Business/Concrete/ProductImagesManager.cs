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
    public class ProductImagesManager : IProductImagesService
    {
        // Automapper ile dönüştürülen Dataları Repository'e gönderip yapılan işlem sonucunu kullanıcıya bildiren Sınıflarım.

        // Repository ile Kullanıcı arasındaki iletişimi Sağlayan Sınıf..

        private readonly IUnitOfWorks works;
        private readonly IMapper mapper;
        public ProductImagesManager(IMapper _mapper, IUnitOfWorks _works)
        {
            works = _works;
            mapper = _mapper;
        }

        public IResult Add(ProductImagesDto data)
        {
            try
            {
                works.ProductImagesRepository.Add(mapper.Map<ProductImages>(data));
                works.SaveChanges();
                // Başarılı Log Kaydı Tutulacak.
                return new Result(ResultStatus.Success, "Kayıt Başarılı");
            }
            catch (Exception)
            {
                // Başarısız Log Kaydı Tutulacak.
                return new Result(ResultStatus.Error, "Kayıt Başarısız.");
            }
        }

        public IResult Delete(int Id)
        {
            try
            {
                works.ProductImagesRepository.Delete(works.ProductImagesRepository.GetByIdFirst(x => x.Id == Id));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Silme Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Silme Başarısız.");
            }
        }
        public IDataResult<IList<ProductImagesDto>> GetAll(int id)
        {
            IList<ProductImagesDto> data = new List<ProductImagesDto>();
            foreach (var item in works.ProductImagesRepository.GetAll(x=> x.ProductsId == id))
            {
                data.Add(mapper.Map<ProductImagesDto>(item));
            }
            if (data.Count > 0)
            {
                return new DataResult<IList<ProductImagesDto>>(ResultStatus.Success,data.Count + " Kayıt Listelendi",data);
            }
            else
            {
                return new DataResult<IList<ProductImagesDto>>(ResultStatus.Info,"Kayıt Bulunamadı", null);
            }
        }

        public ProductImagesDto GetByIdFirst(int id)
        {
            return mapper.Map<ProductImagesDto>(works.ProductImagesRepository.GetByIdFirst(x => x.Id == id));
        }

        public IResult Update(ProductImagesDto data)
        {
            try
            {
                works.ProductImagesRepository.Update(mapper.Map<ProductImages>(data));
                works.SaveChanges();
                // Başarılı Log Kaydı Tutulacak.
                return new Result(ResultStatus.Success, "Güncelleme Başarılı");
            }
            catch (Exception)
            {
                // Başarısız Log Kaydı Tutulacak.
                return new Result(ResultStatus.Error, "Güncelleme Başarısız.");
            }
        }
    }
}
