using System.Collections.Generic;

namespace OkeyGame
{
    public class CardGroup
    {
        public List<CardOnDeck> members = new List<CardOnDeck>();
        public CardGroup Copy()
        {
            CardGroup copy = new CardGroup();
            copy.members.AddRange(this.members);
            return copy;
        }
    }
}
