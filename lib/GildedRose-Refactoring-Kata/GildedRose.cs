using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }
        public void UpdateSellIn(int argIndex) {
            Items[argIndex].SellIn = Items[argIndex].SellIn - 1;
        }
        protected void IncreaseQuality(int argIndex) {
            Items[argIndex].Quality = Items[argIndex].Quality +1 > 50 ? 50: Items[argIndex].Quality + 1;
        }
        public void DecreaseQuality(int argIndex) {
            Items[argIndex].Quality = Items[argIndex].Quality -1 < 0 ? 0 : Items[argIndex].Quality-1;
        }
        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var varName = Items[i].Name;
                if (varName != "Sulfuras, Hand of Ragnaros") {
                    if (varName != "Aged Brie" && varName != "Backstage passes to a TAFKAL80ETC concert")
                    {
                        DecreaseQuality(i);
                    }
                    else
                    {
                        IncreaseQuality(i);
                        if (Items[i].SellIn < 11)  { IncreaseQuality(i); }
                        if (Items[i].SellIn < 6) { IncreaseQuality(i); }
                    }
                    UpdateSellIn(i);

                    if (Items[i].SellIn < 0)
                    {
                        if (varName == "Backstage passes to a TAFKAL80ETC concert") {
                            Items[i].Quality = 0;
                        } else if (varName != "Aged Brie")
                        {
                            DecreaseQuality(i);
                        } 
                        else {
                            IncreaseQuality(i);
                        }
                    }
                }
            }
        }
    }
}
