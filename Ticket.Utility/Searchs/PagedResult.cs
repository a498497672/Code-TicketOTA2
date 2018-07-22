using System.Collections.Generic;

namespace Ticket.Utility.Searchs
{
    public class PagedResult<TItem>
    {
        public PagedResult(IEnumerable<TItem> items,int? count)
        {
            Items = items;
            Count = count;
        }

        public IEnumerable<TItem> Items { get; set; }

        public long? Count { get; set; }
    }
}
