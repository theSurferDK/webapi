using System;

namespace BookApp.DataTransferObjects
{
		public class BookDTO
		{
				public Guid Id { get; set; }
				public string Title { get; set; }
				public string Author { get; set; }

		}
}