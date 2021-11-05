using Core.Entitiess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    // Orders'e tıklayınca, OrderDetail,Information,Notes Tabloları listeleneceği için, bunlara 1 tane Dto yeterlidir.
	public class OrderNotesDto : IDto
    {
        public int Id { get; set; }
        public int OrdersId { get; set; }
        public string Notes { get; set; }
        public DateTime NoteDate { get; set; }
    }
}
