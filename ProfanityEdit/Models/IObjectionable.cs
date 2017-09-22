namespace ProfanityEdit.Models
{
    public interface IObjectionable
    {
        int CategoryId { get; set; }
        Category Category { get; set; }

        int Level { get; set; }
    }
}
