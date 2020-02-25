namespace OkeyGame
{
    public class Card
    {
        public uint id = 0;  // id index of Card

        // Constructor
        public Card(uint _id)
        {
            id = _id;
        }

        // Colors
        public enum ColorType
        {
            RED = 0,
            BLUE = 1,
            BLACK = 2,
            YELLOW = 3
        }

        // Color getter
        public ColorType Color
        {
            get { return (ColorType)((id / 13) % 4); }
        }

        // Number getter
        public int Number
        {
            get { return (int)(id % 13) + 1; }
        }

        public override string ToString()
        {
            return Color + " " + Number + " [ " + id + " ]";
        }

        public bool SameWith(Card card)
        {
            return card.Number == this.Number && card.Color == this.Color;
        }

        public static int GetID(ColorType color, int number, int team = 0)
        {
            return ((int)color) * 13 + number - 1 + team * 52;
        }

    }
}
