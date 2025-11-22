namespace BibliotecaLuisSeila.Models
{
    public class Role
    {
        public int Id { get; set; }


        public string Name { get; set; } = null!;

        // Relación 1:N
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}