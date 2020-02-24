namespace OkeyGame
{
    public class Card
    {
        // id index of Card
        public uint id = 0;

        // Constructor
        public Card(uint _id) {
            id = _id;
        }

        // Colors
        public enum ColorType {
            RED =0,
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
        public uint Number
        {
            get { return (id % 13) + 1; }
        }

    }
}
