
using System.Collections.Generic;
using System;
using System.Linq;

namespace pheval
{
    public class Eval
    {
        static ushort[] binaries_by_id =  {
          0x1,  0x1,  0x1,  0x1,
          0x2,  0x2,  0x2,  0x2,
          0x4,  0x4,  0x4,  0x4,
          0x8,  0x8,  0x8,  0x8,
          0x10,  0x10,  0x10,  0x10,
          0x20,  0x20,  0x20,  0x20,
          0x40,  0x40,  0x40,  0x40,
          0x80,  0x80,  0x80,  0x80,
          0x100,  0x100,  0x100,  0x100,
          0x200,  0x200,  0x200,  0x200,
          0x400,  0x400,  0x400,  0x400,
          0x800,  0x800,  0x800,  0x800,
          0x1000,  0x1000,  0x1000,  0x1000,
        };

        static ushort[] suitbit_by_id = {
          0x1,  0x8,  0x40,  0x200,
          0x1,  0x8,  0x40,  0x200,
          0x1,  0x8,  0x40,  0x200,
          0x1,  0x8,  0x40,  0x200,
          0x1,  0x8,  0x40,  0x200,
          0x1,  0x8,  0x40,  0x200,
          0x1,  0x8,  0x40,  0x200,
          0x1,  0x8,  0x40,  0x200,
          0x1,  0x8,  0x40,  0x200,
          0x1,  0x8,  0x40,  0x200,
          0x1,  0x8,  0x40,  0x200,
          0x1,  0x8,  0x40,  0x200,
          0x1,  0x8,  0x40,  0x200,
        };

        /*
         * Card id, ranged from 0 to 51.
         * The two least significant bits represent the suit, ranged from 0-3.
         * The rest of it represent the rank, ranged from 0-12.
         * 13 * 4 gives 52 ids.
         */
        public static int Eval5(byte a, byte b, byte c, byte d, byte e)
        {
            // Console.WriteLine("a {0} b {1} c {2} d {3} e {4}", a, b, c, d, e);
            uint suit_hash = 0;

            suit_hash += suitbit_by_id[a];
            suit_hash += suitbit_by_id[b];
            suit_hash += suitbit_by_id[c];
            suit_hash += suitbit_by_id[d];
            suit_hash += suitbit_by_id[e];

            if (DpTables.suits[suit_hash] != 0)
            {
                uint[] suit_binary = { 0, 0, 0, 0 };

                suit_binary[a & 0x3] |= binaries_by_id[a];
                suit_binary[b & 0x3] |= binaries_by_id[b];
                suit_binary[c & 0x3] |= binaries_by_id[c];
                suit_binary[d & 0x3] |= binaries_by_id[d];
                suit_binary[e & 0x3] |= binaries_by_id[e];

                return Hashtable.flush[suit_binary[DpTables.suits[suit_hash] - 1]];
            }

            byte[] quinary = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            quinary[(a >> 2)]++;
            quinary[(b >> 2)]++;
            quinary[(c >> 2)]++;
            quinary[(d >> 2)]++;
            quinary[(e >> 2)]++;

            int hash = Hash.hash_quinary(quinary, 5);

            return Hashtable5.noflush5[hash];
        }

        public static int Eval5Ids(params byte[] ids)
        {
            return Eval5(ids[0], ids[1], ids[2], ids[3], ids[4]);
        }

        public static int Eval5Cards(params Card[] cards)
        {
            return Eval5(cards[0].id, cards[1].id, cards[2].id, cards[3].id, cards[4].id);
        }

        public static int Eval5String(string s)
        {
            Card[] hand = Card.Cards(s);
            return Eval5(hand[0].id, hand[1].id, hand[2].id, hand[3].id, hand[4].id);
        }

        /*
        }
         * Card id, ranged from 0 to 51.
         * The two least significant bits represent the suit, ranged from 0-3.
         * The rest of it represent the rank, ranged from 0-12.
         * 13 * 4 gives 52 ids.
         */
        public static int Eval6(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            int suit_hash = 0;

            suit_hash += suitbit_by_id[a];
            suit_hash += suitbit_by_id[b];
            suit_hash += suitbit_by_id[c];
            suit_hash += suitbit_by_id[d];
            suit_hash += suitbit_by_id[e];
            suit_hash += suitbit_by_id[f];

            if (DpTables.suits[suit_hash] != 0)
            {
                int[] suit_binary = { 0, 0, 0, 0 };

                suit_binary[a & 0x3] |= binaries_by_id[a];
                suit_binary[b & 0x3] |= binaries_by_id[b];
                suit_binary[c & 0x3] |= binaries_by_id[c];
                suit_binary[d & 0x3] |= binaries_by_id[d];
                suit_binary[e & 0x3] |= binaries_by_id[e];
                suit_binary[f & 0x3] |= binaries_by_id[f];

                return Hashtable.flush[suit_binary[DpTables.suits[suit_hash] - 1]];
            }

            byte[] quinary = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            quinary[(a >> 2)]++;
            quinary[(b >> 2)]++;
            quinary[(c >> 2)]++;
            quinary[(d >> 2)]++;
            quinary[(e >> 2)]++;
            quinary[(f >> 2)]++;

            int hash = Hash.hash_quinary(quinary, 6);

            return Hashtable6.noflush6[hash];
        }

        public static int Eval6Ids(params byte[] ids)
        {
            return Eval6(ids[0], ids[1], ids[2], ids[3], ids[4], ids[5]);
        }

        public static int Eval6Cards(params Card[] cards)
        {
            return Eval6(cards[0].id, cards[1].id, cards[2].id, cards[3].id, cards[4].id, cards[5].id);
        }

        public static int Eval6String(string s)
        {
            Card[] hand = Card.Cards(s);
            return Eval6(hand[0].id, hand[1].id, hand[2].id, hand[3].id, hand[4].id, hand[5].id);
        }

        /*
         * Card id, ranged from 0 to 51.
         * The two least significant bits represent the suit, ranged from 0-3.
         * The rest of it represent the rank, ranged from 0-12.
         * 13 * 4 gives 52 ids.
         */
        public static int Eval7(byte a, byte b, byte c, byte d, byte e, byte f, byte g)
        {
            int suit_hash = 0;

            suit_hash += suitbit_by_id[a];
            suit_hash += suitbit_by_id[b];
            suit_hash += suitbit_by_id[c];
            suit_hash += suitbit_by_id[d];
            suit_hash += suitbit_by_id[e];
            suit_hash += suitbit_by_id[f];
            suit_hash += suitbit_by_id[g];

            if (DpTables.suits[suit_hash] != 0)
            {
                int[] suit_binary = { 0, 0, 0, 0 };
                suit_binary[a & 0x3] |= binaries_by_id[a];
                suit_binary[b & 0x3] |= binaries_by_id[b];
                suit_binary[c & 0x3] |= binaries_by_id[c];
                suit_binary[d & 0x3] |= binaries_by_id[d];
                suit_binary[e & 0x3] |= binaries_by_id[e];
                suit_binary[f & 0x3] |= binaries_by_id[f];
                suit_binary[g & 0x3] |= binaries_by_id[g];

                return Hashtable.flush[suit_binary[DpTables.suits[suit_hash] - 1]];
            }

            byte[] quinary = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            quinary[(a >> 2)]++;
            quinary[(b >> 2)]++;
            quinary[(c >> 2)]++;
            quinary[(d >> 2)]++;
            quinary[(e >> 2)]++;
            quinary[(f >> 2)]++;
            quinary[(g >> 2)]++;

            int hash = Hash.hash_quinary(quinary, 7);

            return Hashtable7.noflush7[hash];
        }

        public static int Eval7Ids(params byte[] ids)
        {
            return Eval7(ids[0], ids[1], ids[2], ids[3], ids[4], ids[5], ids[6]);
        }

        public static int Eval7Cards(params Card[] cards)
        {
            return Eval7(cards[0].id, cards[1].id, cards[2].id, cards[3].id, cards[4].id, cards[5].id, cards[6].id);
        }


        public static int Eval7String(string s)
        {
            Card[] hand = Card.Cards(s);
            return Eval7(hand[0].id, hand[1].id, hand[2].id, hand[3].id, hand[4].id, hand[5].id, hand[6].id);
        }
        /// <summary>
        /// When given a list of less than five cards, return the best 5 card hand,
        /// assuming the remaining cards are wild cards.
        /// If a single card is passed, this is 5 of a kind (-13 for Aces, -12 for Kings, etc)
        /// If two cards are passed, this is either 5 of a kind of they are paired, straight flush if they
        /// are suited and can form a straight, and 4 of a kind otherwise.
        /// </summary>
        public static int GetBestRankForWildCardHand(List<Card> nonWildCards)
        {
            if (nonWildCards == null || nonWildCards.Count == 0 || nonWildCards.Count > 4)
            {
                throw new ArgumentException("Must pass between one and four cards");
            }

            if (nonWildCards.Count == 1 || nonWildCards.All(z => z.id / 4 == nonWildCards[0].id / 4))
            {
                return -1 * (nonWildCards[0].id / 4);
            }
            
            int bestRank = Int32.MaxValue;

            for (byte a = 0; a < 48; a += 1)
            {
                for (byte b = (byte)(a + 1); b < 49; b += 1)
                {
                    for (byte c = (byte)(b + 1); c < 50; c += 1)
                    {
                        for (byte d = (byte)(c + 1); d < 51; d += 1)
                        {
                            for (byte e = (byte)(d + 1); e < 52; e += 1)
                            {
                                if (nonWildCards.All(z => z.id == a || z.id == b || z.id == c || z.id == d || z.id == e))
                                {
                                    var eval = Eval.Eval5Ids(a, b, c, d, e);
                                    if (eval < bestRank)
                                    {
                                        bestRank = eval;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return bestRank;
        }

        /// <summary>
        /// For a seven card hand, say you have 4 non-wild cards and 3 wild cards.
        /// The best 5 card hand you can make will combine 3 wild cards and 2 non-wild cards.
        /// If you have 6 non-wild cards and 1 wild card, the best hand you can make
        /// will combine 1 wild card and 4 non-wild cards.
        /// If you have a 5 card hand
        /// </summary>
        /// <param name="nonWildCards">All non-wild cards involved in the hand, whether it's a five or seven card hand.</param>
        /// <param name="numWildCards">The number of wild cards in the hand.</param>
        /// <returns>The rank for the best possible hand with the supplied non-wild cards and count of wild cards.</returns>
        public static int GetBestRankForWildCardHand(List<Card> nonWildCards, int numWildCards)
        {
            int sizeOfCombinations = 5 - numWildCards;

            var combinations = GetUniqueCombinations(nonWildCards, sizeOfCombinations);

            int bestRank = Int32.MaxValue;

            foreach (var combo in combinations)
            {
                int currentRank = GetBestRankForWildCardHand(combo);
                if (currentRank < bestRank)
                {
                    bestRank = currentRank;
                }
            }

            return bestRank;
        }

        public static HashSet<List<T>> GetUniqueCombinations<T>(List<T> list, int length) where T : IComparable
        {
            var results = new HashSet<List<T>>(new ListComparer<T>());
            Combine(list, length, 0, new List<T>(), results);
            return results;
        }

        private static void Combine<T>(List<T> list, int length, int startIndex, List<T> current, HashSet<List<T>> results) where T : IComparable
        {
            if (length == 0)
            {
                results.Add(new List<T>(current));
                return;
            }

            for (int i = startIndex; i <= list.Count - length; i++)
            {
                current.Add(list[i]);
                Combine(list, length - 1, i + 1, current, results);
                current.RemoveAt(current.Count - 1);
            }
        }

        class ListComparer<T> : IEqualityComparer<List<T>> where T : IComparable
        {
            public bool Equals(List<T> x, List<T> y)
            {
                return x.OrderBy(a => a).SequenceEqual(y.OrderBy(a => a));
            }

            public int GetHashCode(List<T> obj)
            {
                int hash = 19;
                foreach (var o in obj.OrderBy(a => a))
                {
                    hash = hash * 31 + o.GetHashCode();
                }
                return hash;
            }
        }
    }
}
