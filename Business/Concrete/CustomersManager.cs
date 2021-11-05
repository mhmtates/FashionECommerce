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
    public class CustomersManager : ICustomersService
    {
        private readonly IUnitOfWorks works;
        private readonly IMapper mapper;

        public CustomersManager(IMapper _mapper,IUnitOfWorks _work)
        {
            works = _work;
            mapper = _mapper;
        }

        public IResult Add(CustomersUpdateDto data)
        {
            try
            {
                works.CustomersRepository.Add(mapper.Map<Customers>(data));
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
                works.CustomersRepository.Delete(works.CustomersRepository.GetByIdFirst(x => x.Id == Id));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Silme Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Silme Başarısız");
            }
        }

        public IDataResult<IList<CustomersDto>> GetAll()
        {
            IList<CustomersDto> data = new List<CustomersDto>();
            foreach (var item in works.CustomersRepository.GetAll())
            {
                data.Add(mapper.Map<CustomersDto>(item));
            }
            if (data.Count > 0)
            {
                return new DataResult<IList<CustomersDto>>(ResultStatus.Success,data.Count + " Kayıt Listelendi",data);
            }
            else
            {
                return new DataResult<IList<CustomersDto>>(ResultStatus.Info,"Kayıt Bulunamadı.", null);
            }
        }

        public IDataResult<CustomersUpdateDto> GetById(int Id)
        {
            var data = works.CustomersRepository.GetByIdFirst(x => x.Id == Id);
            if (data !=null)
            {
                return new DataResult<CustomersUpdateDto>(ResultStatus.Success,"1 Kayıt Getirildi.",mapper.Map<CustomersUpdateDto>(data));
            }
            else
            {
                return new DataResult<CustomersUpdateDto>(ResultStatus.Info, "Kayıt Bulunamadı",null);
            }
        }

        public IDataResult<CustomersDto> Login(string EPosta, string Telefon, string Sifre)
        {
            if (EPosta == null) // EMail Boş Telefon Doluysa Çalışacak.
            {
                var data = works.CustomersRepository.GetByIdFirst(x => x.Phone == Telefon && x.Password == Sifre);

                if (data != null)
                {
                    return new DataResult<CustomersDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<CustomersDto>(data));
                }
                else
                {
                    return new DataResult<CustomersDto>(ResultStatus.Info, "Kayıt Bulunamadı", null);
                }
            }
            else if (Telefon == null) // Telefon Boş EMail Doluysa Çalışacak.
            {
                var data = works.CustomersRepository.GetByIdFirst(x => x.Email == EPosta && x.Password == Sifre);
                if (data != null)
                {
                    return new DataResult<CustomersDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<CustomersDto>(data));
                }
                else
                {
                    return new DataResult<CustomersDto>(ResultStatus.Info, "Kayıt Bulunamadı", null);
                }
            }
            else // EMail ve Telefon Doluysa Çalışacak.
            {
                var data = works.CustomersRepository.GetByIdFirst(x => x.Email == EPosta && x.Password == Sifre && x.Phone == Telefon);
                if (data != null)
                {
                    return new DataResult<CustomersDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<CustomersDto>(data));
                }
                else
                {
                    return new DataResult<CustomersDto>(ResultStatus.Info, "Kayıt Bulunamadı", null);
                }
            }
        }
        public IResult Update(CustomersUpdateDto data)
        {
            try
            {
                works.CustomersRepository.Update(mapper.Map<Customers>(data));
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
