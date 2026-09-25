namespace GildedTros.App.Updaters
{
    /// <summary>
    /// "Backstage passes" increase in quality as the conference approaches:
    /// +2 when 10 days or fewer remain, +3 when 5 or fewer remain, capped at 50.
    /// Quality drops to 0 once the conference has passed.
    /// </summary>
    public class BackstagePassUpdater : IItemUpdater
    {
        public const string NamePrefix = "Backstage passes";
        
        public bool AppliesTo(Item item) => item.Name != null && item.Name.StartsWith(NamePrefix);

        public void Update(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality += 1;

                if (item.SellIn < 11 && item.Quality < 50)
                {
                    item.Quality += 1;
                }

                if (item.SellIn < 6 && item.Quality < 50)
                {
                    item.Quality += 1;
                }
            }

            item.SellIn -= 1;

            if (item.SellIn < 0)
            {
                item.Quality = 0;
            }
        }
    }
}
