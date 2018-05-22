using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace VirtualTour.Models
{
    public class Image
    {
        public Image()
        {
            Comments = new List<Comment>();
        }
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string UserId { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}