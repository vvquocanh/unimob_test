using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListAccess
{
    public static T GetRandomElementFromList<T>(List<T> list) where T : class
    {
        if (list == null)
        {
            Debug.LogError($"List {nameof(list)} is null.");
            return null;
        }

        if (list.Count == 0)
        {
            Debug.LogWarning($"List {nameof(list)} is empty.");
            return null;
        }

        int randomIndex = Random.Range(0, list.Count);

        if (list[randomIndex] == null)
        {
            Debug.LogError($"List {nameof(list)} has null element at index {randomIndex}");
            return null;
        }

        return list[randomIndex];
    }
}
