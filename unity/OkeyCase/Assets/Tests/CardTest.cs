using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;
namespace Tests
{

    public class CardTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void CardTestSimplePasses()
        {
            Card card = new Card(22);
            Assert.AreEqual(card.Color, Card.ColorType.BLUE);
            Assert.AreEqual(card.Number,10);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator CardTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [Test]
        public void Shuffler()
        {
            int range = 104;
            int[] array = Table.Shuffler(range);

            for(int i = 0; i < range; i++)
            {
                Assert.False(array[i] < 0 || array[i] >= range,"int value range is not corrent");
                for (int j  = i+1; j < range; j++)
                {
                    Assert.AreNotEqual(array[i], array[j],"int values are not unique : ");
                }
            }
        }

        [Test]
        public void DistrubutionTest() {
            Table table = new Table();
            Assert.Greater(table.decks.Length, 0);
            table.Distribute();
            foreach(Deck deck in table.decks)
            {
                int count = 0;
                for (int i = 0; i < deck.slots.Length; i++)
                {
                    if (deck.slots[i] != null)
                    {
                        count++;
                        Debug.Log(deck.slots[i].ToString());
                    }
                }
                Debug.Log("-- Sorted ---");
                deck.Sort().ForEach(Debug.Log);
                Assert.AreEqual(count, 14);
            }
        }

    }

    public class Deck
    {

        // max 9 grup olabilir
        // BIR GRUP MIN 3 MAX 15 taştan oluşur
        //  GRUPLA -> 777 veya 123
        // GRUPLARI KOMBINE ET
        // UYUMSUZ KOMBINLERI ELE
        // KOMBINLERI KALAN TAŞ'A GÖRE SIRALA


        public Card[] slots = new Card[28];

        public Deck()
        {
            // Deck constructor
        }

        public void Combine() { }
        public void Group() { }
        public void Group777() { }
        public void Group123() { }

        public List<Card> List()
        {
            List<Card> list = new List<Card>();
            for (int i = 0; i < slots.Length; i++)
                if (slots[i] != null)
                    list.Add(slots[i]);
           return list;
        }

        public List<Card> Sort()
        {
            List<Card> list = List();

            list.Sort((a, b) =>(a.Number-b.Number));

            return list;
        }

        public void AddCard(Card card)
        {
            for(int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == null)
                {
                    slots[i] = card;
                    return;
                }
            }
            Debug.LogError("AddCard Slot Overflow");
        }
    }

    public class Table
    {
        public Deck[] decks;
        private List<Card> cards = new List<Card>();

        public Card GetCard(int i)
        {
            return cards[i];
        }

        public enum TableType
        {
            ALONE = 0,
            TWOPLAYER = 1,
            FOURPLAYER  = 2,
        }

        public TableType type;

        public Table()
        {
            decks = new Deck[1] { new Deck() };
            type = TableType.ALONE;
            for (uint i = 0; i < 104; i++)
                cards.Add(new Card(i));
        }

        public void Distribute()
        {
            int[] shuffeled = Shuffler(104);
            int index = 0;
            foreach (Deck deck in decks)
            {
                for (int i = 0; i< 14;i++)
                {
                    deck.AddCard( GetCard(shuffeled[index]) );
                    index++;
                }
            }
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


    public class Card
    {
        // id index of Card
        public uint id = 0;

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

    }
}
