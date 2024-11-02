using System.ComponentModel.DataAnnotations;

namespace API_ForUNT.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        [Required]
        public string EnglishName { get; set; }
        public string KhmerName { get; set; }
        public string Address { get; set; }
        public string AttentionTo { get; set; }
        public string ContactNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public int CreateBy { get; set; }
    }
}
