using System.Collections.Generic;
using UnityEngine;

public class PoolingObject : MonoBehaviour
{
    #region Attribute
    private int initializeNumber = 0;

    private GameObject objectPrefab = null;

    private Queue<GameObject> objectQueue = new Queue<GameObject>();
    #endregion

    #region Methods

    public void InstantiateObject(int intializeNumber, GameObject objectPrefab)
    {
        this.initializeNumber = intializeNumber;
        this.objectPrefab = objectPrefab;

        if (IsOneOfThingNull(nameof(InstantiateObject))) return;
        
        for (var i = 0; i < initializeNumber; i++)
        {
            var gameObject = Instantiate(objectPrefab);
            EnqueueGameObject(gameObject);
        }
    }

    private bool IsOneOfThingNull(string functionName)
    {
        return IsQueueNull(functionName) || IsObjectPrefabNull(functionName);
    }

    private bool IsObjectPrefabNull(string functionName)
    {
        if (!objectPrefab)
        {
            Debug.LogError($"Pooling object is null when using {functionName}.");
            return true;
        }

        return false;
    }

    private bool IsQueueNull(string functionName)
    {
        if (objectQueue == null)
        {
            Debug.LogError($"Object queue is null when using {functionName}.");
            return true;
        }
        
        return false;
    }

    public GameObject GetGameObject()
    {
        if (IsOneOfThingNull(nameof(GetGameObject))) return null;
        
        if (objectQueue.Peek()) return objectQueue.Dequeue();

        return Instantiate(objectPrefab);
    }

    public void ReturnGameObject(GameObject gameObject)
    {
        if (IsQueueNull(nameof(ReturnGameObject))) return;

        if (!gameObject)
        {
            Debug.LogError($"Return object is null.");
            return;
        }
        
        EnqueueGameObject(gameObject);
    }

    private void EnqueueGameObject(GameObject gameObject)
    {
        objectQueue.Enqueue(gameObject);
        gameObject.SetActive(false);
    }
    #endregion
}
