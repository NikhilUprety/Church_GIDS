using System.ComponentModel.DataAnnotations;

namespace Church_GIDS.Model
{
    public class Church
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
