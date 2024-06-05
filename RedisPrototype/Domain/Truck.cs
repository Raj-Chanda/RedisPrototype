namespace RedisPrototype.Domain
{
    public class Truck
    {
        public Guid Id { get; set; }
        public string TruckNumber { get; set; }
        public string? EngineNumber { get; set; }
        public string? ChasisNumber { get; set; }
        public int? Wheels { get; set; }
    }
}
