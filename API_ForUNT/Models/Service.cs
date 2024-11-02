namespace API_ForUNT.Models
{
    public class Service
    {
        public int SeviceId { get; set; }
        public string SeviceName { get; set; }
        public string ServiceNameKhmer { get; set; }
        public double Price { get; set; }
        public string Currency { get;set; }
        public int CreateBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        
    }
}
