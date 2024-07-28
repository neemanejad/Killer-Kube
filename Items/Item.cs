using Godot;
using Nova.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killer_Kube.Scripts
{
    public delegate void ItemEffectHandler(PlayableCharacter character);

    public class InventoryItem
    {
        public int Count { get; set; }
        public Item Item { get; set; }
        public InventoryItem(Item item)
        {
            Count = 0;
            Item = item;
        }
    }

    public class Item
    {
        public readonly int Id;
        public readonly string Name;
        public readonly string Description;
        public readonly float DropChance;
        public readonly PackedScene ItemScene;
        public readonly ItemEffectHandler ItemEffectHandler;
        public Item(int Id, string Name, string Description, float DropChance, PackedScene ItemScene, ItemEffectHandler ItemEffectHandler)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.DropChance = DropChance;
            this.ItemScene = ItemScene;
            this.ItemEffectHandler = ItemEffectHandler;
        }
    }
}
