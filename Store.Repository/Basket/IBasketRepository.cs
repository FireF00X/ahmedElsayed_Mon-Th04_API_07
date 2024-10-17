using Store.Repository.Basket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Basket
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetCustomerAsync(string basketId);
        Task<CustomerBasket> UpdateCustomerAsync(CustomerBasket basket);
        Task<bool> DeleteCustomerAsync(string basketId);
    }
}
