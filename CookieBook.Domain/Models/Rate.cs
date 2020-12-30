using CookieBook.Domain.Models.Base;

namespace CookieBook.Domain.Models
{
    public class Rate : Entity
    {
        public bool Deleted { get; set; }
        public float Value { get; set; }
        public string Description { get; set; }

        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        public Rate(float value, string description) : base()
        {
            Deleted = false;
            Value = value;
            Description = description;
        }
    }
}