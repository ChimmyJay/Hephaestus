using System.ComponentModel.DataAnnotations;

namespace HephaestusSQLiteRepo.Entities
{
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}