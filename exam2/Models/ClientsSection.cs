using System.ComponentModel.DataAnnotations.Schema;

namespace exam2.Models
{
    public class ClientsSection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? formFile { get; set; }

    }
}
