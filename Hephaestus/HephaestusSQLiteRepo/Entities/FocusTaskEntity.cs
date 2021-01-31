using System;
using System.ComponentModel.DataAnnotations;

namespace HephaestusSQLiteRepo.Entities
{
    public class FocusTaskEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTimeOffset StartTime { get; set; }
    }
}