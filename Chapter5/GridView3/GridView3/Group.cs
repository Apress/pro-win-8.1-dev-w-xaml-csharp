using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridView2
{
    public class Group<TKey, TItem>
    {
        public TKey Key { get; set; }
        public IList<TItem> Items { get; set; }
    }
}
