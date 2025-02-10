﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{
    public class Inventory(IInventoryOwner _owner)
    {
        private Dictionary<IItem, int> Items { get; set; } = [];
        private readonly IInventoryOwner owner = _owner;
        public int GetItemNum(IItem item) => Items[item];
        public bool Use(IItem item)
        {
            if(Items.ContainsKey(item))
            {
                if (Items[item] > 0)
                {
                    item.Use();
                    Subtract(item);
                }
                
                return true;
            }
            return false;
        }

        public IEnumerable<KeyValuePair<IItem, int>> Find(IItem _item)
        {
            var query = from item in Items
                        where item.Key.GetType() == _item.GetType()
                        select item;

            return query;
        }       

        public void Add(IItem item, int num = 1) 
        {
            if (Items.ContainsKey(item))
            {
                Items[item] += num;
            }
            else
            {
                Items[item] = num;
            }
        }

        public void Add(Inventory inventory)
        {
            foreach (var item in inventory.Items)
            {
                if (Items.ContainsKey(item.Key))
                {
                    Items[item.Key] += 1;
                }
                else
                {
                    Items[item.Key] = 1;
                }
            }
        }

        public void Remove(IItem item) => Items.Remove(item);
        public void Subtract(IItem item, int num = 1)
        {
            if (Items.ContainsKey(item))
            {
                Items[item] -= num;
                if (Items[item] < 1)
                {
                    Items.Remove(item);
                }
            }
        }

        public static Inventory operator +(Inventory a, Inventory b)
        {
            Inventory result = new(a.owner);

            foreach (var kvp in a.Items)
                result.Items[kvp.Key] = kvp.Value;

            foreach (var kvp in b.Items)
            {
                if (result.Items.ContainsKey(kvp.Key))
                    result.Items[kvp.Key] += kvp.Value;
                else
                    result.Items[kvp.Key] = kvp.Value;
            }

            return result;
        }

        public static Inventory operator +(Inventory a, IItem b)
        {
            Inventory result = new(a.owner);

            foreach (var kvp in a.Items)
                result.Items[kvp.Key] = kvp.Value;

            if (result.Items.ContainsKey(b))
                result.Items[b] += 1;
            else
                result.Items[b] = 1;

            return result;
        }

        public static Inventory operator -(Inventory a, IItem b)
        {
            Inventory result = new(a.owner);

            foreach (var kvp in a.Items)
                result.Items[kvp.Key] = kvp.Value;

            if (result.Items.ContainsKey(b))
                result.Remove(b);
            else
                result.Items[b] = 1;

            return result;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(Items);
        }
    }
}
