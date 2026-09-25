namespace GildedTros.App.Updaters
{
    /// <summary>
    /// "Good Wine" increases in quality the older it gets: +1 per day and
    /// twice as fast once the sell-by date has passed. Quality is capped at 50.
    /// </summary>
    public class GoodWineUpdater : IItemUpdater
    {
        public const string ItemName = "Good Wine";

        public bool AppliesTo(Item item) => item.Name == ItemName;

        public void Update(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality += 1;
            }

            item.SellIn -= 1;

            if (item.SellIn < 0 && item.Quality < 50)
            {
                item.Quality += 1;
            }
        }
    }
}
