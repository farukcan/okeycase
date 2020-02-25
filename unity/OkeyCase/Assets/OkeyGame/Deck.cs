using System.Collections.Generic;
using System.Linq;

namespace OkeyGame
{

    public class Deck
    {

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

        public CardCombine Combine(CombineMethod method = CombineMethod.SMART)
        {
            // min combine group count 1;
            // min combine group count 5;

            List<CardGroup> groups = null;
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


            for (int i = 1; i < 6; i++)
            {
                foreach (IEnumerable<CardGroup> combine in Combiner.Combinations(groups, i))
                {
                    int count = combine.Sum((group) => group.members.Count);
                    if (count > bestCombine.CardCount)
                    {
                        HashSet<int> slotIDs = new HashSet<int>();
                        bool valid = combine.All((group) => group.members.All((card) => slotIDs.Add(card.slot)));
                        if (valid)
                        {
                            bestCombine.members = combine.ToList();
                        }

                    }
                }
            }


            return bestCombine;
        }

        public List<CardGroup> Group(List<CardOnDeck> sortedList)
        {
            List<CardGroup> merged = Group123(sortedList);
            merged.AddRange(Group777(sortedList));
            return merged;
        }

        public List<CardGroup> Group777(List<CardOnDeck> sortedList)
        {

            List<CardGroup> groups = new List<CardGroup>();
            List<CardOnDeck> normalCards, okeys;
            ExtractOkeys(sortedList, out normalCards, out okeys);

            for (int i = 0; i < normalCards.Count; i++)
            {
                Card card = normalCards[i].card;
                CardGroup temp_group = new CardGroup();
                temp_group.members.Add(normalCards[i]);
                int usedOkeys = 0;

                for (int j = 1; j < normalCards.Count; j++)
                {
                    int jindex = (i + j) % normalCards.Count;
                    Card card2 = normalCards[jindex].card;


                    if (card.Number == card2.Number)
                    {
                        if (temp_group.members.All((cardSlot)=> cardSlot.card.Color != card2.Color)  )
                        {

                            temp_group.members.Add(normalCards[jindex]);
                            if (temp_group.members.Count > 2)
                            {
                                groups.Add(temp_group.Copy());
                            }
                            continue;
                        }

                    }
                    else
                    {
                        if (okeys.Count > usedOkeys) // if has usable okey cards
                        {

                            // use okey card
                            temp_group.members.Add(okeys[usedOkeys]);
                            if (temp_group.members.Count > 2)
                            {
                                groups.Add(temp_group.Copy());
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

        public List<CardGroup> Group123(List<CardOnDeck> sortedList)
        {

            List<CardGroup> groups = new List<CardGroup>();
            List<CardOnDeck> normalCards, okeys;
            ExtractOkeys(sortedList, out normalCards, out okeys);

            for (int i = 0; i < normalCards.Count; i++)
            {
                Card card = normalCards[i].card;
                int number = card.Number;
                CardGroup temp_group = new CardGroup();
                temp_group.members.Add(normalCards[i]);
                int usedOkeys = 0;

                for (int j = 1; j < normalCards.Count; j++)
                {
                    int jindex = (i + j) % normalCards.Count;
                    Card card2 = normalCards[jindex].card;


                    if (card.Color == card2.Color)
                    {

                        if (card2.Number == ((number % 13 + 1) % 13 + 1))
                        {
                            if (okeys.Count > usedOkeys) // if has usable okey cards
                            {

                                // use okey card
                                number = number % 13 + 1;
                                temp_group.members.Add(okeys[usedOkeys]);
                                if (temp_group.members.Count > 2)
                                {
                                    groups.Add(temp_group.Copy());
                                }
                                usedOkeys++;
                            }
                        }


                        if (card2.Number == (number % 13 + 1))
                        {

                            number = (number % 13 + 1);

                            temp_group.members.Add(normalCards[jindex]);
                            if (temp_group.members.Count > 2)
                            {
                                groups.Add(temp_group.Copy());
                            }
                            continue;
                        }

                        if (card2.Number == (number % 13))
                        {
                            continue;
                        }

                        if (okeys.Count > usedOkeys) // if has usable okey cards
                        {

                            // use okey card
                            number = number % 13 + 1;
                            temp_group.members.Add(okeys[usedOkeys]);
                            if (temp_group.members.Count > 2)
                            {
                                groups.Add(temp_group.Copy());
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
        }
    }

}