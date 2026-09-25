using System.Collections.Generic;

namespace GildedTros.App.Updaters
{
    /// <summary>
    /// Legendary items never have to be sold and never change in quality,
    /// so ageing them is a no-op. Add new legendary items to <see cref="ItemNames"/>.
    /// </summary>
    public class LegendaryUpdater : IItemUpdater
    {
        public static readonly IReadOnlySet<string> ItemNames = new HashSet<string>
        {
            "B-DAWG Keychain",
        };

        public bool AppliesTo(Item item) => ItemNames.Contains(item.Name);

        public void Update(Item item)
        {
            // Legendary items do not change.
        }
    }
}
