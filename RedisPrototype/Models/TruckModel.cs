namespace RedisPrototype.Models
{
    public class TruckModel
    {
        public Guid? Id { get; set; }
        public string? TruckNumber { get; set; }
        public string? EngineNumber { get; set; }
        public string? ChasisNumber { get; set; }
        public int? Wheels { get; set; }
    }
}
