namespace API_ForUNT.Models
{
    public class QuotationDetail
    {
        public int QuotationId { get; set; }
        public string Referents_Code{get;set;}
        public int ServiceId { get; set; }
        public int Validity { get; set; }
        public double Unit { get; set; }
        public int QTY { get; set; }
        public double Rate { get; set; }
        public string Remark { get; set; }
        public double Amount { get; set; }
    }
}