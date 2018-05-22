using System;


namespace VirtualTour.Models
{
    public class Comment
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public Guid? ImageId { get; set; }
        public Image Image { get; set; }
    }
}