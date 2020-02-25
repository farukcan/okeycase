namespace OkeyGame
{

    public struct CardOnDeck
    {
        public int slot;
        public Card card;
        public CardOnDeck(int _slot, Card _card)
        {
            slot = _slot;
            card = _card;
        }
    }
}
