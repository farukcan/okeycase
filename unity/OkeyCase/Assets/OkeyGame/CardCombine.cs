using System.Collections.Generic;
using System.Linq;

namespace OkeyGame
{
    public class CardCombine
    {
        public List<CardGroup> members = new List<CardGroup>();
        public int CardCount
        {
            get { return members.Sum((m) => m.members.Count); }
        }
    }
}
