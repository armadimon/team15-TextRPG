using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _15TextRPG.Source
{

    public class Inventory
    {
        public List<ItemIdentifier> Items { get; set; } = [];

        public class ItemIdentifier
        {
            public ItemIdentifier(string itemType, int tag, string name, int count)
            {
                ItemType = itemType;
                Tag = tag;
                Name = name;
                Count = count;
            }

            // Properties of an item
            public string ItemType { get; }
            public int Tag { get; }
            public string Name { get; }
            public int Count { get; set; }
        }

        public int GetItemNum(IItem item)
        {
            foreach (var _ in Items)
            {
                if(item.GetType() == Type.GetType(_.ItemType))
                {
                    return _.Count;
                }
            }
            return 0;
        }

        public bool Use(IItem item)
        {
            foreach (var _ in Items)
            {
                if (item.GetType() == Type.GetType(_.ItemType))
                {
                    item.Use();
                    return true;
                }
            }
            return false;
        }

        public void Add(IItem item, int num = 1)
        {
            foreach (var _ in Items)
            {
                if (item.GetType() == Type.GetType(_.ItemType))
                {
                    _.Count += num;
                    return;
                }
            }
            Items.Add(new ItemIdentifier(item.GetType().ToString(), item.Tag, item.Name, num));
        }

        public void Add(int tag, int num = 1)
        {
            foreach (var _ in Items)
            {
                if (tag == _.Tag)
                {
                    _.Count += num;
                }
            }
        }

        public void Add(string name, int num = 1)
        {
            foreach (var _ in Items)
            {
                if (name == _.Name)
                {
                    _.Count += num;
                }
            }
        }

        public void Add(Inventory inventory) => Items = [.. Items, .. inventory.Items];

        public void Remove(IItem item)
        {
            foreach (var _ in Items)
            {
                if (item.GetType() == Type.GetType(_.ItemType))
                {
                    Items.Remove(_);
                }
            }
        }
        public void Subtract(IItem item, int num = 1)
        {
            foreach (var _ in Items)
            {
                if (item.GetType() == Type.GetType(_.ItemType))
                {
                    _.Count -= num;
                    if(_.Count < 0)
                    {
                        Items.Remove(_);
                    }
                }
            }
        }

        public void Subtract(int tag, int num = 1)
        {
            foreach (var _ in Items)
            {
                if (tag == _.Tag)
                {
                    _.Count -= num;
                    if (_.Count < 0)
                    {
                        Items.Remove(_);
                    }
                }
            }
        }

        public void Subtract(string name, int num = 1)
        {
            foreach (var _ in Items)
            {
                if (name == _.Name)
                {
                    _.Count -= num;
                    if (_.Count < 0)
                    {
                        Items.Remove(_);
                    }
                }
            }
        }

        //public static Inventory operator +(Inventory a, Inventory b) => new Inventory(a.owner) { Items = [..a.Items,..b.Items] };

        //public static Inventory operator +(Inventory a, IItem b) => new Inventory(a.owner) { Items = [.. a.Items, b] };

        //public static Inventory operator -(Inventory a, IItem b) => new Inventory(a.owner) { Items = a.Remove(b) };

        public override string ToString()
        {
            string result = string.Empty;
            foreach(var _ in Items)
            {
                result += $"{_.Name}: {_.Count} 개                        \n";
            }
            return result;
        }

        // 테스트 중-----
        // 에라 모르겠다 함수들
        // 권장되지 않음
        public bool Use(string name)
        {
            foreach (var _ in Items)
            {
                if (_.Name == name)
                {
                    if (_.Count > 0)
                    {
                        Type? type = Type.GetType(_.ItemType);

                        if(type is not null)
                        {
                            object? instance = Activator.CreateInstance(type);

                            MethodInfo? method = type.GetMethod("Use");

                            method?.Invoke(instance, null);
                        }
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
                if (_.Tag == tag)
                {
                    if (_.Count > 0)
                    {
                        Type? type = Type.GetType(_.ItemType);

                        if (type is not null)
                        {
                            object? instance = Activator.CreateInstance(type);

                            MethodInfo? method = type.GetMethod("Use");

                            method?.Invoke(instance, null);
                        }
                    }

                    return true;
                }
            }
            return false;
        }

        public IEnumerable<ItemIdentifier> Find(IItem _item)
        {
            var query = from item in Items
                        where item.GetType() == _item.GetType()
                        select item;

            return query;
        }       
    }
}
