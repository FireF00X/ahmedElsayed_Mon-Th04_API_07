﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService.Dto
{
    public class OrderItemDto
    {
        public decimal Price { get; set; }
        public int  Quantity{ get; set; }
        public int ProductItemId { get; set; }
        public int ProductName { get; set; }
        public string PictureUrl { get; set; }
        public Guid OrderId { get; set; }
    }
}
