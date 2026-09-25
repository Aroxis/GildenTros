namespace GildedTros.App.Updaters
{
    /// <summary>
    /// Knows how to age a single <see cref="Item"/> by one day.
    /// One implementation per item category; the first whose
    /// <see cref="AppliesTo"/> returns true handles the item.
    /// </summary>
    public interface IItemUpdater
    {
        bool AppliesTo(Item item);
        void Update(Item item);
    }
}
