﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird.Models;
using LinenAndBird.DataAccess;

namespace LinenAndBird.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private BirdRepository _birdRepository;
        private HatRepository _hatRepository;
        private OrdersRepository _orderRepository;


        public OrdersController(BirdRepository birdRepo, HatRepository hatRepo, OrdersRepository orderRepo)
        {
            //_birdRepository = new BirdRepository(connectionString);
            _birdRepository = birdRepo;
            _hatRepository = hatRepo;
            _orderRepository = orderRepo;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderRepository.GetAll());

        }

        [HttpPost]
        public IActionResult CreateOrder(CreateOrderCommand command)
        {
            var hatToOrder = _hatRepository.GetById(command.HatId);
            var birdToOrder = _birdRepository.GetById(command.BirdId);

            if (hatToOrder == null)
                return NotFound("There was no matching hat in the database");
            
            if (birdToOrder == null)
                return NotFound("There was no matching bird in the database");

            var order = new Order
            {
                Bird = birdToOrder,
                Hat = hatToOrder,
                Price = command.Price
            };

            _orderRepository.Add(order);

            return Created($"/api/orders/{order.Id}", order);

        }
        [HttpGet("{id}")]
        public IActionResult GetOrderById(Guid id)
        {
            Order order = _orderRepository.Get(id);

            if (order == null)
            {
                return NotFound("No order exists with that id");
            }
            return Ok(order);
        }

    }
}
