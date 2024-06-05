using RedisPrototype.Domain;
using RedisPrototype.Enums;
using RedisPrototype.Models;

namespace RedisPrototype.Services
{
    public interface IDBService
    {
        Task<Truck> GetAsync(Guid id);
        Task<Truck> GetByIdAsync(Guid id);
        Task<ResponseMessage<Truck>> GetFromDBAsync(Guid id);
        Task<ResponseMessage<Truck>> GetFromCacheAsync(Guid id);
        Task<ResponseMessage<Truck>> PostAsync(TruckModel truckModel);
        Task<bool> ValidateInput(TruckModel truck);
        string GetStatus(OperationStatus status);
    }
}
