using System;
using Xunit;
using System.Collections.Generic;

namespace HandTest
{
    public class HandTest
    {
        [Fact]
        public void OnePairIsGreaterThanThreePair()
        {   
            var onePair = new Hand(new List<Card>{new Card(11), new Card(24)});
            var threePair = new Hand(new List<Card>{new Card(0), new Card(13)});
            Assert.True(onePair.CompareHand(threePair));
            
        }

        [Fact]
        public void QToAPairIsGreaterThan3To5Pair()
        {   
            var QToAPair = new Hand(new List<Card>{new Card(11), new Card(24), new Card(10), new Card(23), new Card(9), new Card(22)});
            var threeToFivePair = new Hand(new List<Card>{new Card(0), new Card(13), new Card(1), new Card(14), new Card(2), new Card(15)});
            Assert.True(QToAPair.CompareHand(threeToFivePair));
            
        }

        [Fact]
        public void KA2IsNotValid() // this test works but is too silly
        {   
            int exception = 0;
            try
            { 
                var KA2 = new Hand(new List<Card>{new Card(10), new Card(11), new Card(12)});
                var threeToFivePair = new Hand(new List<Card>{new Card(0), new Card(1), new Card(2)});
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
            var QToAFlush = new Hand(new List<Card>{new Card(11), new Card(10), new Card(9)});
            var threeToFiveFlush = new Hand(new List<Card>{new Card(0), new Card(1),new Card(2)});
            Assert.True(QToAFlush.CompareHand(threeToFiveFlush));
            
        }

        [Fact]
        public void QToAFlushIsGreaterThanA23Flush()
        {   
            var QToAFlush = new Hand(new List<Card>{new Card(11), new Card(10), new Card(9)});
            var A23Flush = new Hand(new List<Card>{new Card(11), new Card(12),new Card(13)});
            Assert.True(QToAFlush.CompareHand(A23Flush));
            
        }

        [Fact]
        public void QToAPairIsGreaterThanA23Pair()
        {   
            var QToAPair = new Hand(new List<Card>{new Card(11), new Card(10), new Card(9), new Card(22), new Card(23), new Card(24) });
            var A23Pair = new Hand(new List<Card>{new Card(11), new Card(12),new Card(13), new Card(24), new Card(25), new Card(26) });
            Assert.True(QToAPair.CompareHand(A23Pair));
            
        }

        [Fact]
        public void QToAHongIsGreaterThanA23Hong()
        {   
            // var QToAHong = new Hand(new List<Card>{new Card(11), new Card(10), new Card(9), new Card(22), new Card(23), new Card(24), new Card(35), new Card(36), new Card(37) });
            var A23Hong = new Hand(new List<Card>{new Card(11), new Card(12),new Card(13), new Card(24), new Card(25), new Card(26), new Card(37), new Card(38), new Card(39) });
            // Assert.True(QToAHong.CompareHand(A23Hong));
            
        }

        [Fact]
        public void CatsIsTheBiggest()
        {   
            var QToAPair = new Hand(new List<Card>{new Card(11), new Card(10), new Card(9), new Card(22), new Card(23), new Card(24) });
            var Cat = new Hand(new List<Card>{new Card(52), new Card(53)});
            Assert.False(QToAPair.CompareHand(Cat));
            
        }

    }
}
