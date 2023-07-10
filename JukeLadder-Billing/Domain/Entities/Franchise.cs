namespace Domain.Entities
{
    public class Franchise
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Theme { get; set; }

        public Franchise(string name, string userId, string theme)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            UserId = userId;
            Theme = theme;
        }
    }
}
