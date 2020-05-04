using System;
using Xunit;
using System.Collections.Generic;

namespace ModelTest
{
    public class HandTest
    {
        private static readonly Card THREE_OF_SPADE = new Card(0);
        private static readonly Card FOUR_OF_SPADE = new Card(1);
        private static readonly Card FIVE_OF_SPADE = new Card(2);
        private static readonly Card QUEEN_OF_SPADE = new Card(9);
        private static readonly Card KING_OF_SPADE = new Card(10);
        private static readonly Card ACE_OF_SPADE = new Card(11);
        private static readonly Card TWO_OF_SPADE = new Card(12);
        private static readonly Card THREE_OF_HEART = new Card(13);
        private static readonly Card FOUR_OF_HEART = new Card(14);
        private static readonly Card FIVE_OF_HEART = new Card(15);
        private static readonly Card QUEEN_OF_HEART = new Card(22);
        private static readonly Card KING_OF_HEART = new Card(23);
        private static readonly Card ACE_OF_HEART = new Card(24);
        private static readonly Card TWO_OF_HEART = new Card(25);
        private static readonly Card THREE_OF_DIAMOND = new Card(26);
        private static readonly Card QUEEN_OF_DIAMOND = new Card(35);
        private static readonly Card KING_OF_DIAMOND = new Card(36);
        private static readonly Card ACE_OF_DIAMOND = new Card(37);
        private static readonly Card TWO_OF_DIAMOND = new Card(38);
        private static readonly Card THREE_OF_CLUB = new Card(39);
        private static readonly Card QUEEN_OF_CLUB = new Card(48);
        private static readonly Card KING_OF_CLUB = new Card(49);
        private static readonly Card ACE_OF_CLUB = new Card(50);
        private static readonly Card TWO_OF_CLUB = new Card(51);
        private static readonly Card SMALL_JOKER = new Card(52);
        private static readonly Card BIG_JOKER = new Card(53);

        [Fact]
        public void ThreeIsGreaterThanEmptyHand()
        {
            var emptyHand = new Hand(new List<Card>(){});
            var three = new Hand(new List<Card>(){THREE_OF_CLUB});
            Assert.True(three.CompareHand(emptyHand));
        }
        
        [Fact]
        public void OnePairIsGreaterThanThreePair()
        {   
            var onePair = new Hand(new List<Card>{ACE_OF_SPADE, ACE_OF_HEART});
            var threePair = new Hand(new List<Card>{THREE_OF_SPADE, THREE_OF_HEART});
            Assert.True(onePair.CompareHand(threePair));
            
        }

        [Fact]
        public void QToAPairIsGreaterThan3To5Pair()
        {   
            var QToAPair = new Hand(new List<Card>{ACE_OF_SPADE, ACE_OF_HEART, KING_OF_SPADE, KING_OF_HEART, QUEEN_OF_SPADE, QUEEN_OF_HEART});
            var threeToFivePair = new Hand(new List<Card>{THREE_OF_SPADE, THREE_OF_HEART, FOUR_OF_SPADE, FOUR_OF_HEART, FIVE_OF_SPADE, FIVE_OF_HEART});
            Assert.True(QToAPair.CompareHand(threeToFivePair));
            
        }

        [Fact]
        public void KA2IsNotValid() // this test works but is too silly
        {   
            int exception = 0;
            try
            { 
                var KA2 = new Hand(new List<Card>{KING_OF_SPADE, ACE_OF_SPADE, TWO_OF_SPADE});
                var threeToFivePair = new Hand(new List<Card>{THREE_OF_SPADE, FOUR_OF_SPADE, FIVE_OF_SPADE});
                Assert.True(KA2.CompareHand(threeToFivePair));
            }
            catch(Exception e){
                exception = 1;
            }
           
            Assert.Equal(1, exception);

        }

        [Fact]
        public void QToAFlushIsGreaterThan3To5Flush()
        {   
            var QToAFlush = new Hand(new List<Card>{ACE_OF_SPADE, KING_OF_SPADE, QUEEN_OF_SPADE});
            var threeToFiveFlush = new Hand(new List<Card>{THREE_OF_SPADE, FOUR_OF_SPADE, FIVE_OF_SPADE});
            Assert.True(QToAFlush.CompareHand(threeToFiveFlush));
            
        }

        [Fact]
        public void QToAFlushIsGreaterThanA23Flush()
        {   
            var QToAFlush = new Hand(new List<Card>{ACE_OF_SPADE, KING_OF_SPADE, QUEEN_OF_SPADE});
            var A23Flush = new Hand(new List<Card>{ACE_OF_SPADE, TWO_OF_SPADE, THREE_OF_SPADE});
            Assert.True(QToAFlush.CompareHand(A23Flush));
            
        }

        [Fact]
        public void QToAPairIsGreaterThanA23Pair()
        {   
            var QToAPair = new Hand(new List<Card>{ACE_OF_SPADE, ACE_OF_HEART, KING_OF_SPADE, KING_OF_HEART, QUEEN_OF_SPADE, QUEEN_OF_HEART});
            var A23Pair = new Hand(new List<Card>{ACE_OF_SPADE, ACE_OF_HEART, TWO_OF_SPADE, TWO_OF_HEART, THREE_OF_SPADE, THREE_OF_HEART});
            Assert.True(QToAPair.CompareHand(A23Pair));
            
        }

        [Fact]
        public void QToAHongIsGreaterThanA23Hong()
        {   
            var QToAHong = new Hand(new List<Card>
            {
                QUEEN_OF_SPADE, KING_OF_SPADE, ACE_OF_SPADE,
                QUEEN_OF_HEART, KING_OF_HEART, ACE_OF_HEART,
                QUEEN_OF_DIAMOND, KING_OF_DIAMOND, ACE_OF_DIAMOND
            });
            var A23Hong = new Hand(new List<Card>
            {
                ACE_OF_SPADE, TWO_OF_SPADE, THREE_OF_HEART,
                ACE_OF_HEART, TWO_OF_HEART, THREE_OF_DIAMOND,
                ACE_OF_DIAMOND, TWO_OF_DIAMOND, THREE_OF_CLUB
            });
            Assert.True(QToAHong.CompareHand(A23Hong));
        }

        [Fact]
        public void ThreeHongIsGreaterThanQKAFlush()
        {
            var threeHong = new Hand(new List<Card>
            {
                THREE_OF_HEART, THREE_OF_DIAMOND, THREE_OF_CLUB
            });
            var QKAFlush = new Hand(new List<Card>
            {
                QUEEN_OF_SPADE, KING_OF_SPADE, ACE_OF_SPADE
            });
            Assert.True(threeHong.CompareHand(QKAFlush));
        }

        [Fact]
        public void ThreeBombIsGreaterThanQToAHong()
        {
            var threeBomb = new Hand(new List<Card>
            {
                THREE_OF_SPADE, THREE_OF_HEART, THREE_OF_DIAMOND, THREE_OF_CLUB
            });
            var QToAHong = new Hand(new List<Card>
            {
                QUEEN_OF_SPADE, KING_OF_SPADE, ACE_OF_SPADE,
                QUEEN_OF_HEART, KING_OF_HEART, ACE_OF_HEART,
                QUEEN_OF_DIAMOND, KING_OF_DIAMOND, ACE_OF_DIAMOND
            });
            Assert.True(threeBomb.CompareHand(QToAHong));
        }

        [Fact]
        public void QToABombIsGreaterThanA23Bomb()
        {   
            var QToABomb = new Hand(new List<Card>
            {
                QUEEN_OF_SPADE, KING_OF_SPADE, ACE_OF_SPADE,
                QUEEN_OF_HEART, KING_OF_HEART, ACE_OF_HEART,
                QUEEN_OF_DIAMOND, KING_OF_DIAMOND, ACE_OF_DIAMOND,
                QUEEN_OF_CLUB, KING_OF_CLUB, ACE_OF_CLUB
            });
            var A23Bomb = new Hand(new List<Card>
            {
                ACE_OF_SPADE, TWO_OF_SPADE, THREE_OF_SPADE,
                ACE_OF_HEART, TWO_OF_HEART, THREE_OF_HEART,
                ACE_OF_DIAMOND, TWO_OF_DIAMOND, THREE_OF_DIAMOND,
                ACE_OF_CLUB, TWO_OF_CLUB, THREE_OF_CLUB
            });
            Assert.True(QToABomb.CompareHand(A23Bomb));
        }

        [Fact]
        public void CatsIsTheBiggest()
        {   
            var QToAPair = new Hand(new List<Card>{ACE_OF_SPADE, ACE_OF_HEART, KING_OF_SPADE, KING_OF_HEART, QUEEN_OF_SPADE, QUEEN_OF_HEART});
            var Cat = new Hand(new List<Card>{SMALL_JOKER, BIG_JOKER});
            Assert.False(QToAPair.CompareHand(Cat));
            
        }

    }
}
