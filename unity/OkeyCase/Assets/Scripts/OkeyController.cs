using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OkeyGame;
using System.Linq;

public class OkeyController : MonoBehaviour
{
        public static OkeyController instance;

        public DraggableCard prototypeCard;

        public Color[] colors;

        List<DraggableCard> cardPooling = new List<DraggableCard>();

        public SlotZone[] slots;

        public float dragForce = 3f;

        Table table;
        Deck deck;

        void Start()
        {
            instance = this;
            table = new Table();
            deck = table.decks[0];
            prototypeCard.gameObject.SetActive(false);
            table.cards.ForEach((card) =>{
                DraggableCard obj = Instantiate(prototypeCard,transform);
                obj.id = (int)card.id;
                obj.color.color = colors[(int)card.Color];
                obj.number.color = colors[(int)card.Color];
                obj.number.text = card.Number.ToString();
                RectTransform rtransform = obj.GetComponent<RectTransform>();
                rtransform.Translate(Random.onUnitSphere * 500);

                obj.gameObject.SetActive(false);
                cardPooling.Add(obj);
            });

                for (int i = 0; i < slots.Length; i++)
                    slots[i].id = i;
                 prototypeCard.gameObject.SetActive(true);

        Distribute();
        }

        public void Restart()
        {
            table = new Table();
            deck = table.decks[0];

            cardPooling.ForEach((obj) => {
                RectTransform rtransform = obj.GetComponent<RectTransform>();
                rtransform.Translate(Random.onUnitSphere * 500);

                obj.gameObject.SetActive(false);
            });

            for (int i = 0; i < slots.Length; i++)
                slots[i].id = i;

            Distribute();
        }

        public void Distribute()
        {
                table.Distribute();
                prototypeCard.id = (int)table.okey.id;
                prototypeCard.color.color = colors[(int)table.okey.Color];
                prototypeCard.number.color = colors[(int)table.okey.Color];
                prototypeCard.number.text = table.okey.Number.ToString();

                table.cards.ForEach((card) => {
                    DraggableCard obj = cardPooling[(int)card.id];
                    if (!obj.gameObject.activeInHierarchy) return;
                    if (card.SameWith(table.okey)) 
                        obj.ShowFront();
                    else
                        obj.ShowBack();
                });

    }

        public void Combine(Deck.CombineMethod method)
        {
                int index = 0;
                CardCombine combine = deck.Combine(method);
                List<CardOnDeck> list = deck.Sort();
                foreach(CardGroup group in combine.members)
                {
                    if(index<14 && (index + group.members.Count) > 13)
                    {
                        while (index < 14)
                        {
                            deck.slots[index] = null;
                            index++;
                        }
                    }

            foreach (CardOnDeck card in group.members)
                    {
                        list.Remove(card);
                        deck.slots[index] = card.card;
                        index++;
                    }
                    deck.slots[index] = null;
                    index++;
                }
                while (index < 28)
                {
                    deck.slots[index] = null;
                    index++;
                }

                while (list.Count>0)
                {
                    index--;
                    CardOnDeck card =list.Last();
                    list.Remove(card);
                    deck.slots[index] = card.card;
                }
        }

        public void Combine123()
        {
            Combine(Deck.CombineMethod.COMBINE123);
        }
        public void Combine777()
        {
            Combine(Deck.CombineMethod.COMBINE777);
        }
        public void CombineSmart()
        {
            Combine(Deck.CombineMethod.SMART);
        }
    public void OnCardDragBegin()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (deck.slots[i] != null)
                {
                    DraggableCard draggableCard = cardPooling[(int)deck.slots[i].id];
                    draggableCard.image.raycastTarget = false;
                }
            }
        }
        public void OnCardDragEnd()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (deck.slots[i] != null)
                {
                    DraggableCard draggableCard = cardPooling[(int)deck.slots[i].id];
                    draggableCard.image.raycastTarget = true;
                }
            }
         }

        public void Move(int cardId,int targetSlotId,int sourceSlodId)
        {
        Debug.Log("cardId : " + cardId);
        Debug.Log("targetSlotId : " + targetSlotId);
        Debug.Log("sourceSlodId : " + sourceSlodId);
            ShiftSlot(targetSlotId);
        Swap( targetSlotId,  sourceSlodId);
        }

        public void Swap(int targetSlotId, int sourceSlodId)
        {
        Debug.Log("Swap " + targetSlotId + " - " + sourceSlodId);
            Card temp = deck.slots[targetSlotId];
            deck.slots[targetSlotId] = deck.slots[sourceSlodId];
            deck.slots[sourceSlodId] = temp;
        }
        
        public void ShiftSlot(int target)
        {
            if (deck.slots[target] == null) return;
            int left = SearchLeftForEmpty(target);
            int right = SearchRightForEmpty(target);

        Debug.Log("L="+left);
        Debug.Log("R="+right);

        if (left != -1 && right == -1)
            ShiftLeft(target, left);
        else if (left == -1 && right != -1)
            ShiftRight(target, right);
        else if (left == -1 && right == -1)
            return;
        else  if(left>=right)
            ShiftRight(target, right);
         else if(right>left)
            ShiftLeft(target, left);
    }

    public void ShiftLeft(int target,int group)
    {
        Debug.Log("Shift Left " + target + " - " + group);
        for (int i = group; i > 0; i--)
            Swap(target - i, target - i + 1);
    }
    public void ShiftRight(int target, int group)
    {
        Debug.Log("Shift Right " + target + " - " + group);
        for (int i = group; i > 0; i--)
            Swap(target + i, target + i - 1);
    }

    public int SearchLeftForEmpty(int target)
        {
            int i = 0;
            if (target < 14)
            {
             int t = target;
                while ( --t >= 0 )
                {
                    i++;
                    if (deck.slots[t] == null)
                        return i;
                }
            }
            else {
             int t = target;
            while (--t > 13)
            {
                i++;
                if (deck.slots[t] == null)
                    return i;
            }
        }
            return -1;
        }


    public int SearchRightForEmpty(int target)
    {
        int i = 0;
        if (target < 14)
        {
            int t = target;
            while (++t < 14)
            {
                i++;
                if (deck.slots[t] == null)
                    return i;
            }
        }
        else
        {
            int t = target;
            while (++t < 28)
            {
                i++;
                if (deck.slots[t] == null)
                    return i;
            }
        }
        return -1;
    }

    void Update()
        {
            for(int i=0; i < slots.Length; i++)
            {
                if (deck.slots[i] != null)
                {
                    DraggableCard draggableCard = cardPooling[(int)deck.slots[i].id];
                    draggableCard.targetSlot = i;
                    if (draggableCard.onDrag) continue;
                    draggableCard.gameObject.SetActive(true);
                    RectTransform rtransformForCard = draggableCard.GetComponent<RectTransform>();
                    RectTransform rtransformForSlot = slots[i].GetComponent<RectTransform>();
                   rtransformForCard.position = Vector3.Lerp(rtransformForCard.position, rtransformForSlot.position, Time.deltaTime*dragForce);
                }
            }
        }

    }
