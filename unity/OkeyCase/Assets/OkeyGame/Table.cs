using System.Collections.Generic;
using UnityEngine;

namespace OkeyGame
{

    public class Table
    {
        public Deck[] decks;
        private List<Card> cards = new List<Card>();

        public Card okey;

        // GetCard in the slot
        public Card GetCard(int i)
        {
            return cards[i];
        }

        // TableType 
        public enum TableType
        {
            ALONE = 0,
            TWOPLAYER = 1,
            THREEPLAYER = 2,
            FOURPLAYER = 3,
        }

        public TableType type;

        // Consructor
        public Table()
        {
            decks = new Deck[1] { new Deck(this) };
            type = TableType.ALONE;
            for (uint i = 0; i < 104; i++)
                cards.Add(new Card(i));
        }

        // Distrubute the cards to the decks
        public void Distribute()
        {
            int[] shuffeled = Shuffler(104);
            int index = 0;
            foreach (Deck deck in decks)
            {
                for (int i = 0; i < 14; i++)
                {
                    deck.AddCard(GetCard(shuffeled[index]));
                    index++;
                }
            }
            okey = GetCard(shuffeled[index]);
        }

        public static int[] Shuffler(int range)
        {
            int[] shuffleIndexes = new int[range];

            for (int t = 0; t < range; t++)
            {
                int r = Random.Range(0, t);
                shuffleIndexes[t] = shuffleIndexes[r];
                shuffleIndexes[r] = t;
            }

            return shuffleIndexes;
        }

    }

}
