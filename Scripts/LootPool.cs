using Godot;
using Microsoft.CodeAnalysis;
using Nova.Scripts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Killer_Kube.Scripts
{
    public static class LootPool
    {
        public static Item BoostBoots = new Item(0, "BoostBoots", "Why fight when you can run?", 1f, null);
        public static Item JitterJuice = new Item(0, "JitterJuice", "When you need that edge", 1f, null);
        public static Item ProsperityPants = new Item(0, "ProsperityPants", "Pants with bottomless pockets", 1f, null);

        private static List<Item> Items = new List<Item>() { BoostBoots, JitterJuice, ProsperityPants };
        
        public static SortedDictionary<float, List<Item>> Pool = new SortedDictionary<float, List<Item>>();

        public static void CreatePool()
        {
            // Remove any existing items from loot pool
            Pool.Clear();

            foreach (Item item in Items)
            {
                if (!Pool.ContainsKey(item.DropChance))
                {
                    Pool[item.DropChance] = new List<Item>();
                }

                Pool[item.DropChance].Add(item);
            }
        }

        public static Optional<Item> GetRandomItemFromPool()
        {
            float randomValue = GD.Randf();

            foreach (var DropChancePool in Pool)
            {
                if (randomValue <= DropChancePool.Key)
                {
                    var items = DropChancePool.Value;
                    var itemIndex = GD.RandRange(0, items.Count - 1);
                    return items[itemIndex];
                }
            }

            return new Optional<Item> { };
        }
    }

    
}
