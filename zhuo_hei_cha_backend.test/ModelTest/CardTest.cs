using System;
using Xunit;

namespace ModelTest
{
    public class CardTest
    {
        [Fact]
        public void number0ShouldBeBlack3()
        {
            var black3 = new Card(0);
            Assert.Equal(Suit.Spade, black3.Suit);
            Assert.Equal(3, black3.Number);
        }
    }
}
