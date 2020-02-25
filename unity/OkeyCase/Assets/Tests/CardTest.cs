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
            Assert.AreEqual(card.Number, 10);
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

            for (int i = 0; i < range; i++)
            {
                Assert.False(array[i] < 0 || array[i] >= range, "int value range is not corrent");
                for (int j = i + 1; j < range; j++)
                {
                    Assert.AreNotEqual(array[i], array[j], "int values are not unique : ");
                }
            }
        }

        [Test]
        public void DistrubutionTest() {
            Table table = new Table();
            Assert.Greater(table.decks.Length, 0);
            table.Distribute();
            foreach (Deck deck in table.decks)
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
                Assert.AreEqual(count, 14);
                Debug.Log("Okey is :");
                Debug.Log(table.okey);
                Assert.AreNotEqual(table.okey, null);

                CardCombine bestCombine = deck.Combine();

                Debug.Log("# Best Combine " + bestCombine.CardCount);

                bestCombine.members.ForEach((group) =>
                {
                    Debug.Log("=== ");

                    group.members.ForEach((m) =>
                    {
                        Debug.Log("- " + m.card.ToString());
                    });
                });



            }
        }

        [Test]
        public void Group123Test()
        {
            Table table = new Table();

            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 2)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 9)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.RED, 8)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 10)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 3)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 7)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLACK, 5)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 11)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 10)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 4)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 5)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.RED, 5)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.RED, 10)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLACK, 13)));

            table.okey = table.GetCard(Card.GetID(Card.ColorType.RED, 10));

            List<CardGroup> groups = table.decks[0].Group123(table.decks[0].Sort());

            int i = 0;
            groups.ForEach((g) =>
            {
                i++;
                Debug.Log("# Group " + i + " count:" + g.members.Count);
                g.members.ForEach((m) =>
                {
                    Debug.Log("- " + m.card.ToString());
                });
            });
        }

        [Test]
        public void Group777Test()
        {
            Table table = new Table();

            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 2)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 9)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.RED, 8)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 10)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 3)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 7)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLACK, 5)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 11)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 10)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 4)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 5)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.RED, 5)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.RED, 10)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLACK, 13)));

            table.okey = table.GetCard(Card.GetID(Card.ColorType.RED, 10));

            List<CardGroup> groups = table.decks[0].Group777(table.decks[0].Sort());

            int i = 0;
            groups.ForEach((g) =>
            {
                i++;
                Debug.Log("# Group " + i + " count:" + g.members.Count);
                g.members.ForEach((m) =>
                {
                    Debug.Log("- " + m.card.ToString());
                });
            });
        }

        [Test]
        public void GroupTest()
        {
            Table table = new Table();

            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 2)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 9)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.RED, 8)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 10)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 3)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 7)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLACK, 5)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 11)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 10)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 4)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 5)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.RED, 5)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.RED, 10)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLACK, 13)));

            table.okey = table.GetCard(Card.GetID(Card.ColorType.RED, 10));

            List<CardGroup> groups = table.decks[0].Group(table.decks[0].Sort());

            int i = 0;
            groups.ForEach((g) =>
            {
                i++;
                Debug.Log("# Group " + i + " count:" + g.members.Count);
                g.members.ForEach((m) =>
                {
                    Debug.Log("- " + m.card.ToString());
                });
            });
        }

        [Test]
        public void CombineTest()
        {
            Table table = new Table();

            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 2)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 9)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.RED, 8)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 10)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 3)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 7)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLACK, 5)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 11)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 10)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 4)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 5)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.RED, 5)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.RED, 10)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLACK, 13)));

            table.okey = table.GetCard(Card.GetID(Card.ColorType.RED, 10));

            CardCombine bestCombine = table.decks[0].Combine();

            Debug.Log("# Best Combine " + bestCombine.CardCount);

            bestCombine.members.ForEach((group) =>
            {
                Debug.Log("=== ");

                group.members.ForEach((m) =>
                {
                    Debug.Log("- " + m.card.ToString());
                });
            });

            Assert.True(bestCombine.CardCount == 11);

        }
        [Test]
        public void CombineTest2()
        {
            Table table = new Table();

            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 4)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 5)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 6)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 4,1)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 10)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 10)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 11)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 12)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.RED, 1)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 3)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.RED, 5)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLUE, 11,1)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.BLACK, 1)));
            table.decks[0].AddCard(table.GetCard(Card.GetID(Card.ColorType.YELLOW, 9)));

            table.okey = table.GetCard(Card.GetID(Card.ColorType.RED, 10));

            CardCombine bestCombine = table.decks[0].Combine();

            Debug.Log("# Best Combine " + bestCombine.CardCount);

            bestCombine.members.ForEach((group) =>
            {
                Debug.Log("=== ");

                group.members.ForEach((m) =>
                {
                    Debug.Log("- " + m.card.ToString());
                });
            });

           // Assert.True(bestCombine.CardCount == 11);

        }

    }

    public struct CardOnDeck {
        public int slot;
        public Card card;
        public CardOnDeck(int _slot, Card _card)
        {
            slot = _slot;
            card = _card;
        }
    }

    public class CardGroup {
        public List<CardOnDeck> members = new List<CardOnDeck>();
        public CardGroup Copy()
        {
            CardGroup copy = new CardGroup();
            copy.members.AddRange(this.members);
            return copy;
        }
    }

    public class CardCombine
    {
        public List<CardGroup> members = new List<CardGroup>();
        public int CardCount
        {
            get { return members.Sum((m) => m.members.Count); }
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

        public Card[] slots = new Card[28]; // card slots in the deck

        public Table table;

        public Deck(Table _table)
        {
            table = _table;
        }

        public enum CombineMethod
        {
            COMBINE123 = 0,
            COMBINE777 = 1,
            SMART = 2,
        }

        public CardCombine Combine(CombineMethod method=CombineMethod.SMART) {
            // min combine group count 1;
            // min combine group count 5;
            Debug.Log("> Combining : " + method);

            List<CardGroup> groups=null;
            switch (method)
            {
                case CombineMethod.COMBINE123:
                    groups = Group123(Sort());
                    break;
                case CombineMethod.COMBINE777:
                    groups = Group777(Sort());
                    break;
                case CombineMethod.SMART:
                    groups = Group(Sort());
                    break;
            }


            CardCombine bestCombine = new CardCombine();


            int debug_iteration = 0; //debug
            int debug_iteration_validation = 0;//debug

            for (int i = 1; i < 6; i++)
            {
                foreach (IEnumerable<CardGroup> combine in Combiner.Combinations(groups, i))
                {
                    int count = combine.Sum((group) => group.members.Count);
                    debug_iteration++;//debug
                    if (count > bestCombine.CardCount)
                    {
                        HashSet<int> slotIDs = new HashSet<int>();
                        bool valid = combine.All( (group) => group.members.All((card) => slotIDs.Add(card.slot)) );
                        debug_iteration_validation++;//debug
                        if (valid)
                        {
                            bestCombine.members = combine.ToList();
                        }

                    }
                }
            }

            Debug.Log("Iteration Count :" + debug_iteration);
            Debug.Log("Validation Iteration Count :" + debug_iteration_validation);

            return bestCombine;
        }

        public List<CardGroup> Group(List<CardOnDeck> sortedList) {
            List<CardGroup> merged = Group123(sortedList);
            merged.AddRange(Group777(sortedList));
            return merged;
        }

        public List<CardGroup> Group777(List<CardOnDeck> sortedList) {
            Debug.Log("> Group 777");

            List<CardGroup> groups = new List<CardGroup>();
            List<CardOnDeck> normalCards, okeys;
            ExtractOkeys(sortedList, out normalCards, out okeys);
            Debug.Log("Normal Cards : " + normalCards.Count);
            Debug.Log("Okey Cards : " + okeys.Count);

            for (int i = 0; i < normalCards.Count; i++)
            {
                Card card = normalCards[i].card;
                Debug.Log("for i " + card.ToString());
                CardGroup temp_group = new CardGroup();
                temp_group.members.Add(normalCards[i]);
                int usedOkeys = 0;

                for (int j = 1; j < normalCards.Count; j++)
                {
                    int jindex = (i + j) % normalCards.Count;
                    Card card2 = normalCards[jindex].card;
                    Debug.Log(" - for j " + card2.ToString());


                    if (card.Number == card2.Number)
                    {
                        if (card.Color != card2.Color)
                        {
                            Debug.Log(" - - match " + card2.ToString());

                            temp_group.members.Add(normalCards[jindex]);
                            if (temp_group.members.Count > 2)
                            {
                                groups.Add(temp_group.Copy());
                                Debug.Log(" - - new group");
                            }
                            continue;
                        }

                    }
                    else
                    {
                        if (okeys.Count > usedOkeys) // if has usable okey cards
                        {
                            Debug.Log(" - using okey " + okeys[usedOkeys].card.ToString());

                            // use okey card
                            temp_group.members.Add(okeys[usedOkeys]);
                            if (temp_group.members.Count > 2)
                            {
                                groups.Add(temp_group.Copy());
                                Debug.Log(" - - new group");
                            }
                            usedOkeys++;
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }


                }
            }
            return groups;
        }

        public List<CardGroup> Group123(List<CardOnDeck> sortedList) {
            Debug.Log("> Group 123");

            List<CardGroup> groups = new List<CardGroup>();
            List<CardOnDeck> normalCards, okeys;
            ExtractOkeys(sortedList, out normalCards, out okeys);
            Debug.Log("Normal Cards : " + normalCards.Count);
            Debug.Log("Okey Cards : " + okeys.Count);

            for (int i = 0; i < normalCards.Count; i++)
            {
                Card card = normalCards[i].card;
                Debug.Log("for i " + card.ToString());
                int number = card.Number;
                CardGroup temp_group = new CardGroup();
                temp_group.members.Add(normalCards[i]);
                int usedOkeys = 0;

                for (int j = 1; j < normalCards.Count; j++)
                {
                    int jindex = (i + j) % normalCards.Count;
                    Card card2 = normalCards[jindex].card;
                    Debug.Log(" - for j " + card2.ToString());


                    if (card.Color == card2.Color)
                    {

                        if (card2.Number == ((number%13+1) % 13 + 1))
                        {
                            if (okeys.Count > usedOkeys) // if has usable okey cards
                            {
                                Debug.Log(" - using okey " + okeys[usedOkeys].card.ToString());

                                // use okey card
                                number = number % 13 + 1;
                                temp_group.members.Add(okeys[usedOkeys]);
                                if (temp_group.members.Count > 2)
                                {
                                    groups.Add(temp_group.Copy());
                                    Debug.Log(" - - new group");
                                }
                                usedOkeys++;
                            }
                        }


                        if (card2.Number == (number % 13 + 1))
                        {
                            Debug.Log(" - - match " + card2.ToString());

                            number = (number % 13 + 1);

                            temp_group.members.Add(normalCards[jindex]);
                            if (temp_group.members.Count > 2)
                            {
                                groups.Add(temp_group.Copy());
                                Debug.Log(" - - new group");
                            }
                            continue;
                        }

                        if (card2.Number == (number % 13 ))
                        {
                            continue;
                        }

                        if (okeys.Count > usedOkeys) // if has usable okey cards
                        {
                            Debug.Log(" - using okey " + okeys[usedOkeys].card.ToString());

                            // use okey card
                            number = number % 13 + 1;
                            temp_group.members.Add(okeys[usedOkeys]);
                            if (temp_group.members.Count > 2)
                            {
                                groups.Add(temp_group.Copy());
                                Debug.Log(" - - new group");
                            }
                            usedOkeys++;
                            continue;
                        }
                        else
                        {
                            break;
                        }

                    }
                    


                }
            }
            return groups;
        }

        public void ExtractOkeys(List<CardOnDeck> cards, out List<CardOnDeck> normalCards, out List<CardOnDeck> okeys)
        {
            okeys = new List<CardOnDeck>();
            normalCards = new List<CardOnDeck>();
            foreach (CardOnDeck c in cards)
            {
                if (c.card.SameWith(table.okey))
                    okeys.Add(c);
                else
                    normalCards.Add(c);
            }
        }

        // Get list of cards in the deck
        public List<CardOnDeck> List()
        {
            List<CardOnDeck> list = new List<CardOnDeck>();
            for (int i = 0; i < slots.Length; i++)
                if (slots[i] != null)
                    list.Add(new CardOnDeck(i, slots[i]));
            return list;
        }

        // Sorts the cards for optimization
        public List<CardOnDeck> Sort()
        {
            List<CardOnDeck> list = List();

            list.Sort((a, b) => (a.card.Number - b.card.Number));

            return list;
        }

        // AddCart to a slot that is empty
        public void AddCard(Card card)
        {
            for (int i = 0; i < slots.Length; i++)
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

    public static class Combiner{
        private static bool NextCombination(IList<int> num, int n, int k)
        {
            bool finished;

            var changed = finished = false;

            if (k <= 0) return false;

            for (var i = k - 1; !finished && !changed; i--)
            {
                if (num[i] < n - 1 - (k - 1) + i)
                {
                    num[i]++;

                    if (i < k - 1)
                        for (var j = i + 1; j < k; j++)
                            num[j] = num[j - 1] + 1;
                    changed = true;
                }
                finished = i == 0;
            }

            return changed;
        }

        public static IEnumerable Combinations<T>(IEnumerable<T> elements, int k)
        {
            var elem = elements.ToArray();
            var size = elem.Length;

            if (k > size) yield break;

            var numbers = new int[k];

            for (var i = 0; i < k; i++)
                numbers[i] = i;

            do
            {
                yield return numbers.Select(n => elem[n]);
            } while (NextCombination(numbers, size, k));
        }
    }

}
