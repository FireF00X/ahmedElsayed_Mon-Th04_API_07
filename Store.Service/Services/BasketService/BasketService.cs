using AutoMapper;
using Store.Repository.Basket;
using Store.Repository.Basket.Models;
using Store.Service.Services.BasketService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository , IMapper mapper) 
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
            => await _basketRepository.DeleteCustomerAsync(basketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string basketId)
        {
            var basket = _basketRepository.GetCustomerAsync(basketId);

            if (basket == null)
            {
                return  new CustomerBasketDto();
            }

            var mappedCustomerBasket = _mapper.Map<CustomerBasketDto>(basket);
            return mappedCustomerBasket;
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto input)
        {
            if (input.Id == null)
                input.Id = GenerateRandomBasketId();

            var mappedCustomerBasket = _mapper.Map<CustomerBasket>(input);

            var updatedBasket = await  _basketRepository.UpdateCustomerAsync(mappedCustomerBasket);

            var mappedUpdateBasket = _mapper.Map<CustomerBasketDto>(updatedBasket);

            return mappedUpdateBasket;

        }


        private string GenerateRandomBasketId()
        {
            Random random = new Random();
            var basketId = random.Next(1000, 10000);
            return $"-BS{basketId}";
        }
    }
}
