using System;
using Xunit;
using System.Collections.Generic;

namespace HandTest
{
    public class HandTest
    {
        [Fact]
        public void ONEONEisAPair()
        {   var a = new Hand(new List<Card>{});
            Assert.Equal(1, a.IsPair(new List<Card>{new Card(0), new Card(13)}));
            
        }
    }
}
