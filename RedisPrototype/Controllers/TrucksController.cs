﻿using Microsoft.AspNetCore.Mvc;
using RedisPrototype.Domain;
using RedisPrototype.Enums;
using RedisPrototype.Models;
using RedisPrototype.Services;

namespace RedisPrototype.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrucksController : ControllerBase
    {
        private readonly IDBService _service;
        public TrucksController(IDBService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Truck>> GetAsync(Guid id)
        {
            var result = _service.GetAsync(id);
            return await result;
        }

        [HttpGet("GetFromDB/{id}")]
        public async Task<ActionResult<ResponseMessage<Truck>>> GetFromDBAsync(Guid id)
        {
            var result = _service.GetFromDBAsync(id);
            return await result;
        }

        [HttpGet("GetFromCache/{id}")]
        public async Task<ActionResult<ResponseMessage<Truck>>> GetFromCacheAsync(Guid id)
        {
            var result = _service.GetFromCacheAsync(id);
            return await result;
        }

        [HttpPost("Save")]
        public async Task<ActionResult<ResponseMessage<Truck>>> PostAsync([FromBody] TruckModel truck)
        {
            if (await _service.ValidateInput(truck))
            {
                var result = _service.PostAsync(truck);
                return await result;
            }
            else
            {
                return new ResponseMessage<Truck>() { Status = _service.GetStatus(OperationStatus.InvalidData), Message = "Invalid Data" };
            }
        }
    }
}
