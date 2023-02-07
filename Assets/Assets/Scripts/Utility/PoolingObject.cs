using System.Collections.Generic;
using UnityEngine;

public class PoolingObject<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Attribute
    private int initializeNumber = 0;

    private T objectPrefab = null;

    private Queue<T> objectQueue = new Queue<T>();
    
    #endregion

    #region Methods

    public void InitializeObjects(int intializeNumber, T objectPrefab)
    {
        this.initializeNumber = intializeNumber;
        this.objectPrefab = objectPrefab;

        if (IsOneOfThingNull(nameof(InitializeObjects))) return;
        
        for (var i = 0; i < initializeNumber; i++)
        {
            var gameObject = Instantiate(objectPrefab.gameObject);
            EnqueueGameObject(gameObject.GetComponent<T>());
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

    public T GetGameObject()
    {
        if (IsOneOfThingNull(nameof(GetGameObject))) return null;
        
        if (objectQueue.Count > 0) return objectQueue.Dequeue();

        return Instantiate(objectPrefab);
    }

    public void ReturnGameObject(T gameObject)
    {
        if (IsQueueNull(nameof(ReturnGameObject))) return;

        if (!gameObject)
        {
            Debug.LogError($"Return object is null.");
            return;
        }
        
        EnqueueGameObject(gameObject);
    }

    private void EnqueueGameObject(T gameObject)
    {
        objectQueue.Enqueue(gameObject);
        gameObject.gameObject.SetActive(false);
    }
    #endregion
}