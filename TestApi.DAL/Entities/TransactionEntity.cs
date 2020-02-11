using System;
using System.Collections.Generic;
using System.Text;
using TestApi.DAL.Enums;

namespace TestApi.DAL.Entities
{
    public class TransactionEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        public TypeOperation Operation { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public ProductEntity Product { get; set; }
        public string UserName { get; set; }
    }
}
