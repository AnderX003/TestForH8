using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Helpers.ExtensionsAndDS
{
    public static class MyExtensions
    {
        public static void AddEntry(
            this EventTrigger trigger,
            EventTriggerType triggerType,
            UnityAction<BaseEventData> action)
        {
            EventTrigger.Entry foundEntry = null;
            foreach (var entry in trigger.triggers)
            {
                if (entry.eventID != triggerType) continue;
                foundEntry = entry;
                break;
            }

            if (foundEntry != null)
            {
                foundEntry.callback.AddListener(action);
                return;
            }

            var newEntry = new EventTrigger.Entry();
            newEntry.eventID = triggerType;
            newEntry.callback.AddListener(action);
            trigger.triggers.Add(newEntry);
        }

        public static bool RemoveEntry(
            this EventTrigger trigger,
            EventTriggerType triggerType,
            UnityAction<BaseEventData> action)
        {
            EventTrigger.Entry foundEntry = null;
            foreach (var entry in trigger.triggers)
            {
                if (entry.eventID != triggerType) continue;
                foundEntry = entry;
                break;
            }

            if (foundEntry != null)
            {
                foundEntry.callback.RemoveListener(action);
                return true;
            }

            return false;
        }

        public static T TryAddComponent<T>(this GameObject obj) where T : Component
        {
            var component = obj.GetComponent<T>();
            return component != null
                ? component
                : obj.AddComponent<T>();
        }

        [Pure]
        public static Vector3 AveragePos(this Vector3[] vertices)
        {
            var localPos = Vector3.one;
            foreach (var vertex in vertices)
            {
                localPos += vertex;
            }

            return localPos / vertices.Length;
        }

        [Pure]
        public static bool Includes(this LayerMask mask, int layer)
        {
            return (mask.value & 1 << layer) > 0;
        }

        [Pure]
        public static bool Excludes(this LayerMask mask, int layer)
        {
            return !mask.Includes(layer);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            list.Shuffle(new System.Random());
        }

        public static void Shuffle<T>(this IList<T> list, System.Random random)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}
