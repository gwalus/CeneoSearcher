using System.ComponentModel.DataAnnotations;

namespace WebEngine.Model
{
    public class Product
    {
        public string Name { get; set; }
        [Key]
        public string Link { get; set; }
        public string Image { get; set; }
        public string Rate { get; set; }
        public string Price { get; set; }
    }
}
