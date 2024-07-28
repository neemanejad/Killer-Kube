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
        private static List<Item> Items = new List<Item>() {
            new Item(0, "Boost Boots", "Move 10% faster", 1f, null, (PlayableCharacter character) =>
            {
                character.Speed *= 1.1f;
            }),
            new Item(0, "Trigger Finger", "Shoot 5% faster", 1f, null, (PlayableCharacter character) =>
            {
                character.Weapon.WeaponHitRate *= 0.95;
                character.Weapon.HitTimer.WaitTime = character.Weapon.WeaponHitRate;
            })
        };

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
