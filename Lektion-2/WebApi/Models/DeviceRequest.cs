using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class DeviceRequest
    {
        [Required]
        public string Id { get; set; } = null!;
    }
}
