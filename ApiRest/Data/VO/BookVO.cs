using System;

namespace ApiRest.Data.VO
{
    public class BookVO
    {
        public long Id { get; set; }

        public string Author { get; set; }

        public DateTime LauchDate { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }

    }
}
