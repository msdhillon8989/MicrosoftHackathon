
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamnotfound.DataModel
{

    public class ProjectCount


    {
        public Project Project { set; get; }
        public int BidCount { set; get; }
        public List<Bid> Bids { set; get; }
    }
}
