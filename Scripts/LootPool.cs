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
    public abstract class Item
    {
        public readonly int Id;
        public readonly string Name;
        public readonly string Description;
        public readonly float DropChance;
        public Item(int Id, string Name, string Description, float DropChance)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.DropChance = DropChance;
        }
        public virtual void AddItemToCharacter(ICharacter character) { throw new NotImplementedException(); }
        public virtual void RemoveItemFromCharacter(ICharacter character) { throw new NotImplementedException(); }
    }

    class BoostBoots : Item
    {
        public BoostBoots(int Id, string Name, string Description, float DropChance) : base(Id, Name, Description, DropChance)
        {
        }

        public override void AddItemToCharacter(ICharacter character)
        {
            base.AddItemToCharacter(character);
        }

        public override void RemoveItemFromCharacter(ICharacter character)
        {
            base.RemoveItemFromCharacter(character);
        }
    }

    class JitterJuice : Item
    {
        public JitterJuice(int Id, string Name, string Description, float DropChance) : base(Id, Name, Description, DropChance)
        {
        }

        public override void AddItemToCharacter(ICharacter character)
        {
            base.AddItemToCharacter(character);
        }

        public override void RemoveItemFromCharacter(ICharacter character)
        {
            base.RemoveItemFromCharacter(character);
        }
    }

    class BigBalls : Item
    {
        public BigBalls(int Id, string Name, string Description, float DropChance) : base(Id, Name, Description, DropChance)
        {
        }

        public override void AddItemToCharacter(ICharacter character)
        {
            base.AddItemToCharacter(character);
        }

        public override void RemoveItemFromCharacter(ICharacter character)
        {
            base.RemoveItemFromCharacter(character);
        }
    }

    public static class LootPool
    {
        public static Item BoostBoots = new BoostBoots(0, "BoostBoots", "Why fight when you can run?", 0.1f);
        public static Item JitterJuice = new JitterJuice(0, "JitterJuice", "When you need that edge", 0.1f);
        public static Item BigBalls = new BigBalls(0, "BigBalls", "Big balls, big fun", 0.3f);

        private static List<Item> Items = new List<Item>() { BoostBoots, JitterJuice, BigBalls };
        
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

            return null;
        }
    }

    
}
