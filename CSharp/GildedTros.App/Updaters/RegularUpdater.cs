namespace GildedTros.App.Updaters
{
    /// <summary>
    /// Fallback rule for ordinary goods: quality drops by 1 per day and
    /// twice as fast once the sell-by date has passed. Quality never goes negative.
    /// </summary>
    public class RegularUpdater : IItemUpdater
    {
        public bool AppliesTo(Item item) => true;

        public void Update(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality -= 1;
            }

            item.SellIn -= 1;

            if (item.SellIn < 0 && item.Quality > 0)
            {
                item.Quality -= 1;
            }
        }
    }
}
