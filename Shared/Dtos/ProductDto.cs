namespace Shared.Dtos
{
    /// <summary>
    /// ProductDto class to map product model with subscribed property.
    /// </summary>
    public class ProductDto
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public string Rate { get; set; }
        public string Price { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
