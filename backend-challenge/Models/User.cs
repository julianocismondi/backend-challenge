﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using System.Text.Json.Serialization;

namespace backend_challenge.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public int RoleId { get; set; }

        [Timestamp]
        public DateTime? CreateAt { get; set; } = DateTime.UtcNow;
    }
}
