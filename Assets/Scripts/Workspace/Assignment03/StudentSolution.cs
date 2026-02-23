using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssignmentSystem.Services;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using Debug = AssignmentSystem.Services.AssignmentDebugConsole;

namespace Assignment03
{
    public class StudentSolution : MonoBehaviour, IAssignment
    {
        #region Lecture

        public void LCT01_SyntaxLinkedList()
        {
           // 1. สร้าง LinkedList ของประเภท string
            LinkedList<string> linkedList = new LinkedList<string>();

            // 2. เพิ่มข้อมูลที่ท้ายของ LinkedList
            linkedList.AddLast("Node 1");
            linkedList.AddLast("Node 2");

            // 3. เพิ่มข้อมูลที่ต้นของ LinkedList
            linkedList.AddFirst("Node 0");

            // 4. แสดงเนื้อหาใน LinkedList
            LCT01_PrintLinkedList(linkedList);

            // 5. เช้าถึงข้อมูลใน LinkedList
            LinkedListNode<string> firstNode = linkedList.First;
            Debug.Log("first", firstNode.Value);
            LinkedListNode<string> lastNode = linkedList.Last;
            Debug.Log("last", lastNode.Value);
            LinkedListNode<string> node1 = linkedList.Find("Node 1");
            Debug.Log(node1.Previous.Value);
            Debug.Log(node1.Next.Value);
            if (firstNode.Previous == null)
            {
                Debug.Log("firstNode.Previous is null");
            }
            if (lastNode.Next == null)
            {
                Debug.Log("lastNode.Next is null");
            }

            // 6. add node ก่อน หรือ หลัง node ที่กำหนด
            linkedList.AddAfter(node1, "Node 1.5");
            linkedList.AddBefore(node1, "Node 0.5");
            LCT01_PrintLinkedList(linkedList);

            // 6. ลบ Node แรก
            linkedList.RemoveFirst();
            LCT01_PrintLinkedList(linkedList);

            // 7. ลบ Node ตามค่าที่กำหนด
            linkedList.Remove("Node 2");
            LCT01_PrintLinkedList(linkedList);
        }

        private void LCT01_PrintLinkedList(LinkedList<string> linkedList)
        {
            Debug.Log("LinkedList...");
            foreach(var node in linkedList)
            {
                Debug.Log(node);
            }
        }

        public void LCT02_SyntaxHashTable()
        {
            
            Hashtable hashtable = new Hashtable();
            //Key Value
            hashtable.Add(1,"Apple");
            hashtable.Add(2,"Banana");
            hashtable.Add("bad-fruit","Rotten Tomato");

            string fruit1 = (string)hashtable[1];
            string fruit2 = (string)hashtable[2];
            string badFruit = (string)hashtable["bad-fruit"];

            Debug.Log($"fruit1: {fruit1}");
            Debug.Log($"fruit2: {fruit2}");
            Debug.Log($"badFruit: {badFruit}");

            LCT02_PrintHashTable(hashtable);

            int key = 2;
            if (hashtable.ContainsKey(key))
            {
                Debug.Log($"found {key}");
            }
            else
            {
                Debug.Log($"not found {key}");
            }

            int keyToRemove = 1;
            hashtable.Remove(keyToRemove);
            LCT02_PrintHashTable(hashtable);
        }
        public void LCT02_PrintHashTable(Hashtable hashtable)
        {
            Debug.Log("table ...");
            foreach(DictionaryEntry entry in hashtable)
            {
                Debug.Log($"Key: {entry.Key}, Value: {entry.Value}");
            }
        }

        public void LCT03_SyntaxDictionary()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Assignment

        public void AS01_CountWords(string[] words)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            foreach (string w in words)
                counts[w] = counts.ContainsKey(w) ? counts[w] + 1 : 1;

            string[] keys = counts.Keys.ToArray();
            int[] values = counts.Values.ToArray();
            for (int i = 0; i < keys.Length; i++)
                Debug.Log($"word: '{keys[i]}' count: {values[i]}");
        }

        public void AS02_CountNumber(int[] numbers)
        {
            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (int n in numbers)
                counts[n] = counts.ContainsKey(n) ? counts[n] + 1 : 1;

            int[] keys = counts.Keys.ToArray();
            int[] values = counts.Values.ToArray();
            for (int i = 0; i < keys.Length; i++)
                Debug.Log($"number: {keys[i]} count: {values[i]}");
        }

        public void AS03_CheckValidBrackets(string input)
        {
            Dictionary<char, char> map = new Dictionary<char, char> { { '(', ')' }, { '[', ']' }, { '{', '}' } };
            LinkedList<char> stack = new LinkedList<char>();

            foreach (char c in input)
            {
                if (map.ContainsKey(c)) stack.AddLast(c);
                else if (map.ContainsValue(c))
                {
                    if (stack.Count == 0 || map[stack.Last.Value] != c) { Debug.Log("Invalid"); return; }
                    stack.RemoveLast();
                }
            }
            Debug.Log(stack.Count == 0 ? "Valid" : "Invalid");
        }

        public void AS04_PrintReverseLinkedList(LinkedList<int> list)
        {
            if (list == null || list.Count == 0) { Debug.Log("List is empty"); return; }
            var current = list.Last;
            while (current != null)
            {
                Debug.Log(current.Value);
                current = current.Previous;
            }
        }

        public void AS05_FindMiddleElement(LinkedList<string> list)
        {
            if (list == null || list.Count == 0) { Debug.Log("List is empty"); return; }
            var slow = list.First;
            var fast = list.First;
            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }
            Debug.Log(slow.Value);
        }

        public void AS06_MergeDictionaries(Dictionary<string, int> dict1, Dictionary<string, int> dict2)
        {
            Dictionary<string, int> merged = new Dictionary<string, int>(dict1);
            foreach (var kvp in dict2)
                merged[kvp.Key] = merged.ContainsKey(kvp.Key) ? merged[kvp.Key] + kvp.Value : kvp.Value;

            foreach (var kvp in merged) Debug.Log($"key: {kvp.Key}, value: {kvp.Value}");
        }

        public void AS07_RemoveDuplicatesFromLinkedList(LinkedList<int> list)
        {
            if (list == null || list.Count <= 1) return;
            Dictionary<int, bool> seen = new Dictionary<int, bool>();
            var current = list.First;
            while (current != null)
            {
                var next = current.Next;
                if (seen.ContainsKey(current.Value)) list.Remove(current);
                else seen[current.Value] = true;
                current = next;
            }
            foreach (int i in list) Debug.Log(i);
        }

        public void AS08_TopFrequentNumber(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0) { Debug.Log("Input array is empty"); return; }
            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (int n in numbers) counts[n] = counts.ContainsKey(n) ? counts[n] + 1 : 1;

            int topNum = numbers[0], maxCount = 0;
            foreach (var kvp in counts)
                if (kvp.Value > maxCount) { topNum = kvp.Key; maxCount = kvp.Value; }

            Debug.Log($"{topNum} count: {maxCount}");
        }

        public void AS09_PlayerInventory(Dictionary<string, int> inventory, string itemName, int quantity)
        {
            if (inventory == null) { Debug.Log("Inventory is null"); return; }
            inventory[itemName] = inventory.ContainsKey(itemName) ? inventory[itemName] + quantity : quantity;
            foreach (var kvp in inventory) Debug.Log($"{kvp.Key}: {kvp.Value}");
        }

        #endregion

        #region Extra

        public void EX01_GameEventQueue(LinkedList<GameEvent> eventQueue)
        {
            throw new System.NotImplementedException();
        }

        public void EX02_PlayerStatsTracker(Dictionary<string, int> playerStats, string statName, int value)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
