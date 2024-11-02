namespace API_ForUNT.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string EnglishName { get; set; }
        public string KhmerName { get; set; }
        public string Address { get; set; }
        public string AttentionTo { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public bool IsDelete { get; set; }
        public int CreateBy { get;set; }
        public DateTime CreatyDate { get; set; }
        public int? UpdateBy { get;set; }
        public DateTime? UpdateAt { get; set; }
    }
}
