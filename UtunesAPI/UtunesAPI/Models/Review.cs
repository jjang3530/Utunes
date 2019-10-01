using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UtunesAPI.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string Comments { get; set; }
        public int SongId { get; set; }
    }
}
