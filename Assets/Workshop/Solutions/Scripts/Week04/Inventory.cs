using System.Collections.Generic;
using UnityEngine;

namespace Solution {
    public class Inventory : MonoBehaviour
    {
        private Dictionary<ItemData, int> items = new Dictionary<ItemData, int>();

        // เพิ่มไอเท็ม
        public void AddItem(ItemData item, int amount)
        {
            if (items.ContainsKey(item))
            {
                items[item] += amount;
            }
            else
            {
                items.Add(item, amount);
            }

            Debug.Log($"Add {item.name} x{amount}");
        }

        // ลบไอเท็ม
        public void UseItem(ItemData item, int amount)
        {
            if (!HasItem(item, amount))
            {
                Debug.Log("Item not enough!");
                return;
            }

            items[item] -= amount;

            if (items[item] <= 0)
            {
                items.Remove(item);
            }

            Debug.Log($"Use {item.name} x{amount}");
        }
        public bool HasItem(ItemData item, int amount)
        {
            if (!items.ContainsKey(item)) 
                return false;

            if (items[item] < amount) 
                return false;
            //2. ตรวจสอบว่ามีไอเท็มนี้ในคลังหรือไม่ และมีจำนวนเพียงพอหรือไม่
            return true;
        }
        // ตรวจสอบจำนวนไอเท็ม
        public int GetItemCount(ItemData item)
        {

            if (!items.ContainsKey(item))
                return 0;

            return items[item];
        }

        // แสดงรายการทั้งหมดในคลัง
        public void PrintInventory()
        {
            Debug.Log("=== Inventory ===");

            foreach (var pair in items)
            {
                Debug.Log(pair.Key.name + " x" + pair.Value);
            }

        }
    }
}

