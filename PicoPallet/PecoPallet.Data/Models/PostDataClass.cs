namespace PecoPallet.Data.Models
{
    public class PostDataClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return Id + " > " + Name + " > " + City;
        }
    }
}
