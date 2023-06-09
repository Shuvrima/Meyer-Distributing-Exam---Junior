﻿using System;
using System.Collections.Generic;
using InterviewTest.Orders;
using InterviewTest.Returns;

namespace InterviewTest.Customers
{
    public abstract class CustomerBase : ICustomer
    {
        private readonly OrderRepository _orderRepository;
        private readonly ReturnRepository _returnRepository;

        protected CustomerBase(OrderRepository orderRepo, ReturnRepository returnRepo)
        {
            _orderRepository = orderRepo;
            _returnRepository = returnRepo;
        }

        public abstract string GetName();
        
        public void CreateOrder(IOrder order)
        {
            _orderRepository.Add(order);
        }

        public List<IOrder> GetOrders()
        {
            return _orderRepository.Get();
        }

        public void CreateReturn(IReturn rga)
        {
            _returnRepository.Add(rga);
        }

        public List<IReturn> GetReturns()
        {
            return _returnRepository.Get();
        }

        public float GetTotalSales() 
        {
            float totalSales = 0;
            foreach (IOrder o in GetOrders()) {
                foreach (OrderedProduct op in o.Products) {
                    totalSales += op.Product.GetSellingPrice();
                }
            }
            return totalSales;
        }

        public float GetTotalReturns() 
        {
            float totalReturns = 0;
            foreach (IReturn r in GetReturns()) {
                foreach (ReturnedProduct rp in r.ReturnedProducts) {
                    totalReturns += rp.OrderProduct.Product.GetSellingPrice();
                }
            }
            return totalReturns;
        }

        public float GetTotalProfit() //to do
        {
            return GetTotalSales() - GetTotalReturns();
        }
    }
}
