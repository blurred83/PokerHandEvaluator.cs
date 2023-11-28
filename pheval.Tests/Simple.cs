using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using System.Text;

namespace pheval.Tests
{
    [TestFixture]
    public class Simple
    {
        [Test]
        public void Conversion()
        {

            // convert Card to and from string
            Assert.AreEqual(0, (new Card("2c")).id);
            Assert.AreEqual(4, (new Card("3c")).id);
            Assert.AreEqual("2c", (new Card(0)).ToString());
            // convert 'hand' of Cards to and from string
            var ids = new byte[] { 0, 4, 8, 12, 16 };
            var hand = "2c3c4c5c6c";
            Assert.AreEqual(hand, Card.CardsToString(Card.Cards(ids)));
            Assert.AreEqual(ids, Card.Cards(hand).Select(c => c.id));
        }

        [Test]
        public void RankCategories()
        {
            // check a few rank categories, their descriptions, and the rank description
            {
                int rank = Eval.Eval5String("ackcqcjctc2d3d");
                Assert.AreEqual(Rank.Category.StraightFlush, Rank.GetCategory(rank));
                Assert.AreEqual("Straight Flush", Rank.DescribeRankCategory(rank));
                Assert.AreEqual("Royal Flush", Rank.DescribeRank(rank));
            }
            {
                int rank = Eval.Eval5String("ackcqcjctc");
                Assert.AreEqual(Rank.Category.StraightFlush, Rank.GetCategory(rank));
                Assert.AreEqual("Straight Flush", Rank.DescribeRankCategory(rank));
                Assert.AreEqual("Royal Flush", Rank.DescribeRank(rank));
            }
            {
                int rank = Eval.Eval5String("kcqcjctc9c");
                Assert.AreEqual(Rank.Category.StraightFlush, Rank.GetCategory(rank));
                Assert.AreEqual("Straight Flush", Rank.DescribeRankCategory(rank));
                Assert.AreEqual("King-High Straight Flush", Rank.DescribeRank(rank));
            }
            {
                int rank = Eval.Eval5String("7s5s4s3s2d");
                Assert.AreEqual(Rank.Category.HighCard, Rank.GetCategory(rank));
                Assert.AreEqual("High Card", Rank.DescribeRankCategory(rank));
                Assert.AreEqual("Seven-High", Rank.DescribeRank(rank));
            }
        }

        [Test]
        public void RankWithWilds()
        {
             var bytes = Convert.FromBase64String("byZeOU50cU14OW9Q");
             var decoded = System.Text.Encoding.UTF8.GetString(bytes);
            {
                List<Card> nonWildCards = new List<Card>()
                {
                    new Card("Ac")
                };
                int rank = Eval.GetBestRankForWildCardHand(nonWildCards);
                Console.WriteLine(Rank.DescribeRankCategory(rank));
                Console.WriteLine(Rank.DescribeRank(rank));
                Assert.AreEqual("Five of a Kind", Rank.DescribeRankCategory(rank));
                Assert.AreEqual("Five Aces", Rank.DescribeRank(rank));
            }
            {
                List<Card> nonWildCards = new List<Card>()
                {
                    new Card("Ac"),
                    new Card("Ah"),
                    new Card("Ad")
                };
                int rank = Eval.GetBestRankForWildCardHand(nonWildCards);
                Console.WriteLine(Rank.DescribeRankCategory(rank));
                Console.WriteLine(Rank.DescribeRank(rank));
                Assert.AreEqual("Five of a Kind", Rank.DescribeRankCategory(rank));
                Assert.AreEqual("Five Aces", Rank.DescribeRank(rank));
            }
            {
                List<Card> nonWildCards = new List<Card>()
                {
                    new Card("Ac"),
                    new Card("4c")
                };
                int rank = Eval.GetBestRankForWildCardHand(nonWildCards);
                Console.WriteLine(Rank.DescribeRankCategory(rank));
                Console.WriteLine(Rank.DescribeRank(rank));
                Assert.AreEqual("Straight Flush", Rank.DescribeRankCategory(rank));
                Assert.AreEqual("Five-High Straight Flush", Rank.DescribeRank(rank));
            }
            {
                List<Card> nonWildCards = new List<Card>()
                {
                    new Card("Ac"),
                    new Card("Tc")
                };
                int rank = Eval.GetBestRankForWildCardHand(nonWildCards);
                Console.WriteLine(Rank.DescribeRankCategory(rank));
                Console.WriteLine(Rank.DescribeRank(rank));
                Assert.AreEqual("Straight Flush", Rank.DescribeRankCategory(rank));
                Assert.AreEqual("Royal Flush", Rank.DescribeRank(rank));
            }
            {
                // Deuces are wild
                // Ah Kh Qh 4c 3s 2h 2d
                List<Card> nonWildCards = new List<Card>()
                {
                    new Card("Ah"),
                    new Card("Kh"),
                    new Card("Qh"),
                    new Card("4c"),
                    new Card("3s")
                };
                int rank = Eval.GetBestRankForWildCardHand(nonWildCards, 2);
                Console.WriteLine(Rank.DescribeRankCategory(rank));
                Console.WriteLine(Rank.DescribeRank(rank));
                Assert.AreEqual("Straight Flush", Rank.DescribeRankCategory(rank));
                Assert.AreEqual("Royal Flush", Rank.DescribeRank(rank));
            }
            {
                // Deuces are wild
                // Ah Kh Qh 4c 3s 2h 2d
                List<Card> nonWildCards = new List<Card>()
                {
                    new Card("Ah"),
                    new Card("Kh"),
                    new Card("9h"),
                    new Card("4c"),
                    new Card("3s")
                };
                int rank = Eval.GetBestRankForWildCardHand(nonWildCards, 2);
                Console.WriteLine(Rank.DescribeRankCategory(rank));
                Console.WriteLine(Rank.DescribeRank(rank));
                Assert.AreEqual("Flush", Rank.DescribeRankCategory(rank));
                Assert.AreEqual("Ace-High Flush", Rank.DescribeRank(rank));
            }
            {
                // Deuces are wild
                // Ah Kh Qh 4c 3s 2h 2d
                List<Card> nonWildCards = new List<Card>()
                {
                    new Card("Ah"),
                    new Card("Kh"),
                    new Card("9h"),
                    new Card("4c"),
                    new Card("3s"),
                    new Card("2s")
                };
                int rank = Eval.GetBestRankForWildCardHand(nonWildCards, 1);
                Console.WriteLine(Rank.DescribeRankCategory(rank));
                Console.WriteLine(Rank.DescribeRank(rank));
                Assert.AreEqual("Straight", Rank.DescribeRankCategory(rank));
                Assert.AreEqual("Five-High Straight", Rank.DescribeRank(rank));
            }
            {
                // Deuces are wild
                // Ah Kh Qh 4c 3s 2h 2d
                List<Card> nonWildCards = new List<Card>()
                {
                    new Card("6h"),
                    new Card("Kh"),
                    new Card("9h"),
                    new Card("4c"),
                    new Card("3s"),
                    new Card("2s")
                };
                int rank = Eval.GetBestRankForWildCardHand(nonWildCards, 1);
                Console.WriteLine(Rank.DescribeRankCategory(rank));
                Console.WriteLine(Rank.DescribeRank(rank));
                Assert.AreEqual("Straight", Rank.DescribeRankCategory(rank));
                Assert.AreEqual("Six-High Straight", Rank.DescribeRank(rank));
            }
        }
    }
}