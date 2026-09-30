using System;
using System.Collections.Generic;

namespace GildedTros.App.Updaters
{
    /// <summary>
    /// Smelly items degrade in quality twice as fast as normal items: -2 per day
    /// and twice as fast again (-4) once the sell-by date has passed. Quality never
    /// goes negative. Add new smelly items to <see cref="ItemNames"/>.
    /// </summary>
    public class SmellyUpdater : IItemUpdater
    {
        public static readonly IReadOnlySet<string> ItemNames = new HashSet<string>
        {
            "Duplicate Code",
            "Long Methods",
            "Ugly Variable Names",
        };

        public bool AppliesTo(Item item) => ItemNames.Contains(item.Name);

        public void Update(Item item)
        {
            DecreaseQuality(item, 2);

            item.SellIn -= 1;

            if (item.SellIn < 0)
            {
                DecreaseQuality(item, 2);
            }
        }

        private static void DecreaseQuality(Item item, int amount)
        {
            item.Quality = Math.Max(0, item.Quality - amount);
        }
    }
}
