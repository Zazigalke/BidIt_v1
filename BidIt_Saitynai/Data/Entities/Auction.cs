namespace BidIt_Saitynai.Data.Entities
{
    public class Auction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
        public Company Company { get; set; }
    }
}
