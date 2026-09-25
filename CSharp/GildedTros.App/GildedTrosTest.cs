using System.Collections.Generic;
using Xunit;

namespace GildedTros.App
{
    public class GildedTrosTest
    {
        // Regular items, normal degradation and value behavior
        private const string RegularItemName = "Ring of Cleansening Code";
        private const string SmellyDuplicateCodeName = "Duplicate Code";
        private const string SmellyLongMethodsName = "Long Methods";
        private const string SmellyUglyVariableNamesName = "Ugly Variable Names";

        // Exclusive items which increase whenever a deathline is met
        private const string TicketReFactorName = "Backstage passes for Re:factor";
        private const string TicketHaxxName = "Backstage passes for HAXX";

        // Rares, they keep their value
        private const string GoodWineName = "Good Wine";
        private const string LegendaryItemName = "B-DAWG Keychain";

        private static GildedTros AppWith(params Item[] items)
        {
            return new GildedTros(new List<Item>(items));
        }

        #region Degrading value items

        
        [Fact]
        public void RegularItem_DegradesByOnePerDay_BeforeSellByDate()
        {
            var item = new Item { Name = RegularItemName, Quality = 20, SellIn = 10 };
            var app = AppWith(item);

            for (int day = 1; day <= 10; day++)
            {
                app.UpdateQuality();
                Assert.Equal(20 - day, item.Quality);
                Assert.Equal(10 - day, item.SellIn);
            }
        }

        [Fact]
        public void RegularItem_DegradesTwiceAsFast_AfterSellByDate()
        {
            var item = new Item { Name = RegularItemName, Quality = 10, SellIn = 0 };
            var app = AppWith(item);

            app.UpdateQuality();

            Assert.Equal(8, item.Quality);   // -2 because the sell-by date has passed
            Assert.Equal(-1, item.SellIn);
        }

        [Fact]
        public void RegularItem_QualityNeverGoesNegative()
        {
            var item = new Item { Name = RegularItemName, Quality = 0, SellIn = 5 };
            var app = AppWith(item);

            app.UpdateQuality();

            Assert.Equal(0, item.Quality);
        }

        #endregion

        #region Smelly items

        [Fact]
        public void SmellyItems_DegradeTwiceAsFast()
        {
            var duplicateCode = new Item { Name = SmellyDuplicateCodeName, Quality = 20, SellIn = 10 };
            var longMethods = new Item { Name = SmellyLongMethodsName, Quality = 20, SellIn = 10 };
            var uglyVariableNames = new Item { Name = SmellyUglyVariableNamesName, Quality = 20, SellIn = 10 };
            var app = AppWith(duplicateCode, longMethods, uglyVariableNames);

            app.UpdateQuality();

            Assert.Equal(18, duplicateCode.Quality);
            Assert.Equal(18, longMethods.Quality);
            Assert.Equal(18, uglyVariableNames.Quality);
        }

        #endregion

        #region Tickets

        [Theory]
        [InlineData(TicketReFactorName)]
        [InlineData(TicketHaxxName)]
        public void Ticket_IncreasesByOne_WhenMoreThanTenDaysLeft(string name)
        {
            var item = new Item { Name = name, Quality = 20, SellIn = 15 };
            var app = AppWith(item);

            app.UpdateQuality();

            Assert.Equal(21, item.Quality);
            Assert.Equal(14, item.SellIn);
        }

        [Theory]
        [InlineData(TicketReFactorName)]
        [InlineData(TicketHaxxName)]
        public void Ticket_IncreasesByTwo_WhenTenDaysOrLessLeft(string name)
        {
            var item = new Item { Name = name, Quality = 20, SellIn = 10 };
            var app = AppWith(item);

            app.UpdateQuality();

            Assert.Equal(22, item.Quality);
        }

        [Theory]
        [InlineData(TicketReFactorName)]
        [InlineData(TicketHaxxName)]
        public void Ticket_IncreasesByThree_WhenFiveDaysOrLessLeft(string name)
        {
            var item = new Item { Name = name, Quality = 20, SellIn = 5 };
            var app = AppWith(item);

            app.UpdateQuality();

            Assert.Equal(23, item.Quality);
        }

        [Theory]
        [InlineData(TicketReFactorName)]
        [InlineData(TicketHaxxName)]
        public void Ticket_DropsToZero_AfterTheConference(string name)
        {
            var item = new Item { Name = name, Quality = 30, SellIn = 0 };
            var app = AppWith(item);

            app.UpdateQuality();

            Assert.Equal(0, item.Quality);
            Assert.Equal(-1, item.SellIn);
        }

        [Theory]
        [InlineData(TicketReFactorName)]
        [InlineData(TicketHaxxName)]
        public void Ticket_QualityNeverExceedsFifty(string name)
        {
            var item = new Item { Name = name, Quality = 49, SellIn = 5 };
            var app = AppWith(item);

            app.UpdateQuality();

            Assert.Equal(50, item.Quality);
        }

        #endregion

        #region Rare items

        [Fact]
        public void GoodWine_IncreasesInQuality_AsItAges()
        {
            var item = new Item { Name = GoodWineName, Quality = 0, SellIn = 2 };
            var app = AppWith(item);

            app.UpdateQuality();

            Assert.Equal(1, item.Quality);
            Assert.Equal(1, item.SellIn);
        }

        [Fact]
        public void GoodWine_IncreasesTwiceAsFast_AfterSellByDate()
        {
            var item = new Item { Name = GoodWineName, Quality = 10, SellIn = 0 };
            var app = AppWith(item);

            app.UpdateQuality();

            Assert.Equal(12, item.Quality);
            Assert.Equal(-1, item.SellIn);
        }

        [Fact]
        public void GoodWine_QualityNeverExceedsFifty()
        {
            var item = new Item { Name = GoodWineName, Quality = 50, SellIn = 5 };
            var app = AppWith(item);

            app.UpdateQuality();

            Assert.Equal(50, item.Quality);
        }

        [Fact]
        public void LegendaryItem_NeverChangesQualityOrSellIn()
        {
            var item = new Item { Name = LegendaryItemName, Quality = 80, SellIn = 0 };
            var app = AppWith(item);

            app.UpdateQuality();

            Assert.Equal(80, item.Quality);
            Assert.Equal(0, item.SellIn);
        }

        #endregion
    }
}
