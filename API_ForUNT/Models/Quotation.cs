namespace API_ForUNT.Models
{
    public class Quotation
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public int CustomerId { get; set; }
        public  DateTime QuotationDate { get; set; }
        public double TotalAmout { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime Expiration { get;set; } 
        public ICollection<QuotationDetail> quotationDetails { get; set; }  


    }
}
