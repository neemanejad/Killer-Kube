using Godot;
using Nova.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killer_Kube.Scripts
{
    public class InventoryItem
    {
        public int Count { get; set; }
        public Item ItemRef { get; set; }
        public InventoryItem(ref Item item)
        {
            Count = 0;
            ItemRef = item;
        }
    }

    public class Item
    {
        public readonly int Id;
        public readonly string Name;
        public readonly string Description;
        public readonly float DropChance;
        public readonly PackedScene ItemScene;
        public Item(int Id, string Name, string Description, float DropChance, PackedScene itemScene)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.DropChance = DropChance;
            ItemScene = itemScene;
        }
    }
}
