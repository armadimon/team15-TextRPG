using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _15TextRPG.Source
{
    public readonly struct ItemIdentifier
    {
        public string ItemType { get; init; }
        public int Tag { get; init; }
        public string Name { get; init; }
    }

    public class Inventory(IInventoryOwner _owner)
    {
        private Dictionary<ItemIdentifier, int> Items { get; set; } = [];
        private readonly IInventoryOwner owner = _owner;

        public int GetItemNum(IItem item)
        {
            ItemIdentifier II = new()
            {
                ItemType = typeof(IItem).ToString(),
                Tag = item.Tag,
                Name = item.Name,
            };

            return Items[II];
        }

        public bool Use(IItem item)
        {
            ItemIdentifier II = new()
            {
                ItemType = typeof(IItem).ToString(),
                Tag = item.Tag,
                Name = item.Name,
            };

            if (Items.ContainsKey(II))
            {
                if (Items[II] > 0)
                {
                    item.Use();
                }

                return true;
            }
            return false; // 아이템 사용 실패
        }

        public bool Use(string name)
        {
            foreach (var _ in Items)
            {
                if (_.Key.Name == name)
                {
                    if (_.Value > 0)
                    {
                        
                    }

                    return true;
                }
            }
            return false;
        }

        public bool Use(int tag)
        {
            foreach (var _ in Items)
            {
                if (_.Key.Tag == tag)
                {
                    if (_.Value > 0)
                    {
                    }

                    return true;
                }
            }
            return false;
        }

        public IEnumerable<KeyValuePair<ItemIdentifier, int>> Find(IItem _item)
        {
            var query = from item in Items
                        where item.Key.GetType() == _item.GetType()
                        select item;

            return query;
        }       

        public void Add(IItem item, int num = 1) 
        {
            ItemIdentifier II = new()
            {
                ItemType = typeof(IItem).ToString(),
                Tag = item.Tag,
                Name = item.Name,
            };

            if (Items.ContainsKey(II))
            {
                Items[II] += num;
            }
            else
            {
                Items[II] = num;
            }
        }

        public void Add(int tag, int num = 1)
        {
            foreach (var _ in Items)
            {
                if (_.Key.Tag == tag)
                {
                    Items[_.Key] += num;
                }
            }
        }

        public void Add(string name, int num = 1)
        {
            foreach (var _ in Items)
            {
                if (_.Key.Name == name)
                {
                    Items[_.Key] += num;
                }
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

        public void Remove(IItem item)
        {
            ItemIdentifier II = new()
            {
                ItemType = typeof(IItem).ToString(),
                Tag = item.Tag,
                Name = item.Name,
            };
            Items.Remove(II);
        }
        public void Subtract(IItem item, int num = 1)
        {
            ItemIdentifier II = new()
            {
                ItemType = typeof(IItem).ToString(),
                Tag = item.Tag,
                Name = item.Name,
            };
            if (Items.ContainsKey(II))
            {
                Items[II] -= num;
                if (Items[II] < 0)
                {
                    Items.Remove(II);
                }
            }
        }

        public void Subtract(int tag, int num = 1)
        {
            foreach (var _ in Items)
            {
                if (_.Key.Tag == tag)
                {
                    Items[_.Key] += num;
                    if (Items[_.Key] < 0)
                    {
                        Items.Remove(_.Key);
                    }
                }
            }
        }

        public void Subtract(string name, int num = 1)
        {
            foreach (var _ in Items)
            {
                if (_.Key.Name == name)
                {
                    Items[_.Key] += num;
                    if (Items[_.Key] < 0)
                    {
                        Items.Remove(_.Key);
                    }
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

            ItemIdentifier II = new()
            {
                ItemType = typeof(IItem).ToString(),
                Tag = b.Tag,
                Name = b.Name,
            };

            foreach (var kvp in a.Items)
                result.Items[kvp.Key] = kvp.Value;

            if (result.Items.ContainsKey(II))
                result.Items[II] += 1;
            else
                result.Items[II] = 1;

            return result;
        }

        public static Inventory operator -(Inventory a, IItem b)
        {
            Inventory result = new(a.owner);

            ItemIdentifier II = new()
            {
                ItemType = typeof(IItem).ToString(),
                Tag = b.Tag,
                Name = b.Name,
            };

            foreach (var kvp in a.Items)
                result.Items[kvp.Key] = kvp.Value;

            if (result.Items.ContainsKey(II))
                result.Remove(b);
            else
                result.Items[II] = 1;

            return result;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(Items);
        }
    }
}
