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
using System.Linq;

namespace Business.Concrete
{
    public class TemporaryBasketsManager : ITemporaryBasketsService
    {
        private readonly IUnitOfWorks works;
        private readonly IMapper mapper;

        public TemporaryBasketsManager(IUnitOfWorks _works, IMapper _mapper)
        {
            works = _works;
            mapper = _mapper;

        }
        public IResult AddUpdate(int ProductId, int CookiesId, int[] variantIds)
        {
            try
            {


                if (works.TemporaryBasketsRepository.GetByIdFirst(x => x.ProductsId == ProductId && x.CookiesId == CookiesId) == null)
                {
                    var FindProduct = works.ProductsRepository.GetByIdFirst(x => x.Id == ProductId);
                    TemporaryBasketsDto temporarybasket = new TemporaryBasketsDto();
                    temporarybasket.ProductsId = FindProduct.Id;
                    temporarybasket.Name = FindProduct.Name;
                    temporarybasket.Quantity = 1;
                    temporarybasket.CookiesId = CookiesId;

                    foreach (var variantId in variantIds)
                    {
                        var VariantFound = works.VariantsRepository.GetByIdFirst(x => x.Id == variantId);
                        temporarybasket.VariantName += VariantFound.Name + "/";
                    }
                    if (variantIds.Length > 0)
                    {
                        temporarybasket.VariantName = temporarybasket.VariantName.TrimEnd('/');
                    }
                    temporarybasket.Price = FindProduct.Discount;

                    temporarybasket.MainImage = FindProduct.MainImage;
                    works.TemporaryBasketsRepository.Add(mapper.Map<TemporaryBaskets>(temporarybasket));
                    works.SaveChanges();
                    return new Result(ResultStatus.Success, "Ürün sepete eklenmiştir.");

                }
                else
                {
                    var BulunanUrun = works.TemporaryBasketsRepository.GetByIdFirst(x => x.ProductsId == ProductId && x.CookiesId == CookiesId);
                    BulunanUrun.Quantity++;
                    works.TemporaryBasketsRepository.Update(BulunanUrun);
                    works.SaveChanges();
                    return new Result(ResultStatus.Success, "Ürün'ün adeti arttırılmıştır.");

                }


            }
            catch (Exception)
            {

                return new Result(ResultStatus.Error, "Kayıt Başarısız");
            }

        }

        public IResult AutoBasketUpdate(AutoBasketsDto data)
        {

            try
            {

                works.AutoBasketsRepository.Update(mapper.Map<AutoBaskets>(data));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Güncelleme Başarılı");

            }
            catch (Exception)
            {

                return new Result(ResultStatus.Error, "Güncelleme Başarısız");
            }



        }


        public IDataResult<AutoBasketsDto> GetByIdAuto(int Id)
        {
            var data = works.AutoBasketsRepository.GetByIdFirst(x => x.Id == Id);
            if (data != null)
            {
                return new DataResult<AutoBasketsDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<AutoBasketsDto>(data));

            }
            else
            {
                return new DataResult<AutoBasketsDto>(ResultStatus.Info, "Kayıt Bulunamadı.", null);

            }

        }



        public IResult Delete(int Id)
        {
            try
            {
                works.TemporaryBasketsRepository.Delete(works.TemporaryBasketsRepository.GetByIdFirst(x => x.Id == Id));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Silme Başarılı");


            }
            catch (Exception)
            {

                return new Result(ResultStatus.Error, "Silme Başarısız");
            }
        }

        public IDataResult<IList<TemporaryBasketsDto>> GetAll(int CookiesId)
        {
            List<TemporaryBasketsDto> data = new List<TemporaryBasketsDto>();
            foreach (var item in works.TemporaryBasketsRepository.GetAll(x => x.CookiesId == CookiesId))
            {
                data.Add(mapper.Map<TemporaryBasketsDto>(item));

            }
            if (data.Count > 0)
            {
                return new DataResult<IList<TemporaryBasketsDto>>(ResultStatus.Success, data.Count + "Kayıt Listelendi", data);
            }
            else
            {

                return new DataResult<IList<TemporaryBasketsDto>>(ResultStatus.Info, "Kayıt Bulunamadı", null);
            }

        }


        public IDataResult<TemporaryBasketsDto> GetById(int Id)
        {
            var data = works.TemporaryBasketsRepository.GetByIdFirst(x => x.Id == Id);
            if (data != null)
            {
                return new DataResult<TemporaryBasketsDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<TemporaryBasketsDto>(data));

            }
            else
            {
                return new DataResult<TemporaryBasketsDto>(ResultStatus.Info, "Kayıt Bulunamadı.", null);

            }


        }


        public IResult SepetArttirEksilt(int Id, bool islem)
        {
            try
            {
                var Sepetim = works.TemporaryBasketsRepository.GetByIdFirst(x => x.Id == Id);
                if (islem)
                {
                    Sepetim.Quantity++;
                    works.TemporaryBasketsRepository.Update(Sepetim);
                    works.SaveChanges();
                    return new Result(ResultStatus.Success, "Ürün'ün adeti arttırılmıştır.");
                }
                else
                {
                    if (Sepetim.Quantity > 1)
                    {
                        Sepetim.Quantity--;
                        works.TemporaryBasketsRepository.Update(Sepetim);
                        works.SaveChanges();
                        return new Result(ResultStatus.Success, "Ürün'ün adeti azaltılmıştır.");
                    }
                    else
                    {
                        return new Result(ResultStatus.Success, "Ürün'ün adeti azaltılamaz.");
                    }


                }

            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
