using System.Collections.Generic;
using System.Linq;
using GildedTros.App.Updaters;

namespace GildedTros.App
{
    public class GildedTros
    {
        IList<Item> Items;

        // Ordered by specificity: the first updater that applies handles the item.
        private static readonly IReadOnlyList<IItemUpdater> Updaters = new IItemUpdater[]
        {
            new GoodWineUpdater(),
            new BackstagePassUpdater(),
            new LegendaryUpdater(),
            new SmellyUpdater(),
            // Catch all, must remain last in any case
            new RegularUpdater(),
        };

        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                UpdaterFor(item).Update(item);
            }
        }

        private static IItemUpdater UpdaterFor(Item item)
        {
            return Updaters.First(updater => updater.AppliesTo(item));
        }
    }
}
