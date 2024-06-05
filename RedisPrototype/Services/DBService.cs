using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using RedisPrototype.Domain;
using RedisPrototype.Enums;
using RedisPrototype.Infrastructure;
using RedisPrototype.Models;
using System.Text;
using System.Text.Json;

namespace RedisPrototype.Services
{
    public class DBService : IDBService
    {
        private const string CacheKey = "Truck:{0}";
        private readonly RedisDBContext _dbContext;
        private readonly IDistributedCache _cache;

        // Setting up the cache options
        DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(30))
            .SetSlidingExpiration(TimeSpan.FromMinutes(30));
        public DBService(RedisDBContext dBContext, IDistributedCache cache)
        {
            _dbContext = dBContext;
            _cache = cache;
        }

        public async Task<Truck> GetAsync(Guid id)
        {
            var cacheKey = string.Format(CacheKey, id);
            //var cacheOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _redisSettings.Ttl };
            var truck = await GetOrAddAsync(cacheKey, () => GetByIdAsync(id), cacheOptions);
            return truck;

            //var cacheResult = await GetFromCacheAsync(id);

            //if (cacheResult == null)
            //{
            //    var cacheKey = string.Format(CacheKey, id);
            //    var device = await GetOrAddAsync(cacheKey, () => GetByIdAsync(id), cacheOptions);
            //}
        }

        public async Task<Truck> GetByIdAsync(Guid id)
        {
            var result = await _dbContext.Trucks.Where(x => x.Id == id).FirstOrDefaultAsync();
            return result;
        }


        public async Task<ResponseMessage<Truck>> GetFromDBAsync(Guid id)
        {
            var result = await GetByIdAsync(id);
            return new ResponseMessage<Truck>() { Status = GetStatus(OperationStatus.Success), Message = "Success", Data = result };
        }
        public async Task<ResponseMessage<Truck>> GetFromCacheAsync(Guid id)
        {
            var cacheKey = string.Format(CacheKey, id);
            var strTruck = await GetAsync(cacheKey);
            if (!strTruck.IsNullOrEmpty())
            {
                var truck = JsonSerializer.Deserialize<Truck>(strTruck);

                return new ResponseMessage<Truck>() { Status = GetStatus(OperationStatus.Success), Message = "Success", Data = truck, CacheKey = cacheKey };
            }

            return new ResponseMessage<Truck>() { Status = GetStatus(OperationStatus.NotFound), Message = "No data found.", Data = null, CacheKey = cacheKey };


            ////var cacheOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _redisSettings.Ttl };
            //var device = await GetOrAddAsync(cacheKey, () => GetByIdAsync(id), cacheOptions);

            //return device;
        }


        public async Task<ResponseMessage<Truck>> PostAsync(TruckModel truckModel)
        {
            try
            {
                var truck = new Truck()
                {
                    Id = Guid.NewGuid(),
                    TruckNumber = truckModel.TruckNumber,
                    EngineNumber = truckModel.EngineNumber,
                    ChasisNumber = truckModel.ChasisNumber,
                    Wheels = truckModel.Wheels
                };

                await _dbContext.AddAsync(truck);
                await _dbContext.SaveChangesAsync();

                var cacheKey = string.Format(CacheKey, truck.Id);
                await SetAsync(cacheKey, truck, cacheOptions);

                return new ResponseMessage<Truck>() { Status = GetStatus(OperationStatus.Success), Message = "Success", Data = truck, CacheKey = cacheKey };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<Truck>() { Status = GetStatus(OperationStatus.Error), Message = ex.ToString() };
            }
        }

        public async Task<bool> ValidateInput(TruckModel truck)
        {
            var isValid = true;

            if (truck == null) isValid = false;
            else if (truck.Id == Guid.Empty) isValid = false;
            else if (string.IsNullOrEmpty(truck.TruckNumber)) isValid = false;

            return isValid;
        }


        #region "Helper"

        public string GetStatus(OperationStatus status)
        {
            return Enum.GetName(typeof(OperationStatus), status);
            //return status.ToString();
        }

        #endregion


        #region "RedisHelper"

        protected async Task<T?> GetOrAddAsync<T>(string key, Func<Task<T>> getDataAsync, DistributedCacheEntryOptions options)
        {
            //var cacheResult = await _cache.GetAsync(key);
            //if (cacheResult != null)
            //{
            //    return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(cacheResult));
            //}
            //else
            //{
            //    var data = await getDataAsync();
            //    await SetAsync(key, data, options);
            //    return data;
            //}

            var cacheResult = await GetAsync(key);
            if (cacheResult == null)
            {
                var data = await getDataAsync();
                await SetAsync(key, data, options);
                return data;
            }

            return JsonSerializer.Deserialize<T>(cacheResult);
        }

        protected async Task<string?> GetAsync(string key)
        {
            var cacheResult = await _cache.GetAsync(key);
            if (cacheResult != null)
            {
                //return JsonSerializer.Deserialize<Truck>(Encoding.UTF8.GetString(cacheResult));

                return Encoding.UTF8.GetString(cacheResult);
            }

            return null;
        }

        protected Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options)
        {
            var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize<T>(value));

            return _cache.SetAsync(key, bytes, options);
        }

        protected Task DeleteAsync(string key)
        {
            return _cache.RemoveAsync(key);
        }

        #endregion
    }
}
