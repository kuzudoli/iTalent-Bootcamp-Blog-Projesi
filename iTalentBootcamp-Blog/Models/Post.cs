namespace iTalentBootcamp_Blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        //Silme işlemi için örnek oluşturulduğunda tekrar tarih ataması yapılıyor
        //nasıl çözülebilir?

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
