﻿using Core.Entitiess.Abstract;
namespace Entities.Dto
{
	public class CustomersDto : IDto
    {
        // Kullanıcı Dto'nun içerisine veri atmayacaksa Doğrulama Yapmıyoruz.
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
    }
}
