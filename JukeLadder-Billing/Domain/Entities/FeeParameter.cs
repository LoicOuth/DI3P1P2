
namespace Domain.Entities;
    public class FeeParameter
    {

        public string Id { get; set; }
        public long Minimum { get; set; }
        public long Maximum { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt  { get; set; }

    public FeeParameter(long minimum, long maximum, bool active, DateTime createdAt, DateTime updatedAt) 
    {
        Id = Guid.NewGuid().ToString();
        Minimum = minimum;
        Maximum = maximum;
        Active = active;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}

