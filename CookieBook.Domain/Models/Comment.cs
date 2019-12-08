using CookieBook.Domain.Models.Base;

namespace CookieBook.Domain.Models
{
	public class Comment : Entity
	{
		public float Rate { get; set; }
		public string Text { get; set; }

		public Comment(float rate, string text) : base()
		{
			Rate = rate;
			Text = text;
		}
	}
}