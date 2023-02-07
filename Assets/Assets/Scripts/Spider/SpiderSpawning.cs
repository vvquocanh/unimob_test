using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpiderPooling))]
public class SpiderSpawning : MonoBehaviour
{
    #region Attributes

    [Header("Data")]
    [SerializeField] private Spider spiderPrefabs = null;

    [SerializeField] private List<Transform> startingPoints = new List<Transform>();

    [SerializeField] private List<Transform> endingPoints = new List<Transform>();

    [Header("Config")] 
    [Range(0.01f, 1f)]
    [SerializeField] private float spawningGapTime = 0.1f;

    [SerializeField] private int spiderInitNumber = 100;

    private PoolingObject<Spider> pool = null;
    #endregion

    #region Initialize

    private void Awake()
    {
        pool = GetComponent<PoolingObject<Spider>>();
    }

    private void Start()
    {
        AddMethodToButtons();
        InitializePool();
    }

    private void InitializePool()
    {
        if (!IsPoolExist()) return;

        pool.InitializeObjects(spiderInitNumber, spiderPrefabs);
    }
    
    private void AddMethodToButtons()
    {
        var buttons = FindObjectsOfType<InstantiateButton>(true);
        foreach (var button in buttons)
        {
            button.AddMethod(InstantiateSpider);
        }
    }

    #endregion

    #region Methods

    private void InstantiateSpider(int number)
    {
        StartCoroutine(Spawn(number));
    }

    IEnumerator Spawn(int number)
    {
        if (!IsPoolExist()) yield break;

        var waitForSeconds = new WaitForSeconds(spawningGapTime);
        for (int i = 0; i < number; i++)
        {
            SpawnSpider();
            yield return waitForSeconds;
        }
    }

    private void SpawnSpider()
    {
        var startingTransform = ListAccess.GetRandomElementFromList(startingPoints);
        if (!startingTransform) return;

        var endingTransfrorm = ListAccess.GetRandomElementFromList(endingPoints);
        if (!endingTransfrorm) return;
        
        var spider = pool.GetGameObject();
        if (!spider)
        {
            Debug.LogError("Error when spawning spiders");
            return;
        }

        spider.transform.position = startingTransform.position;
        spider.AddMethod(pool.ReturnGameObject);
        spider.SetDestination(endingTransfrorm);
        spider.gameObject.SetActive(true);
    }
    
    private bool IsPoolExist()
    {
        if (!pool)
        {
            Debug.LogError("Pooling object is missing.");
            return false;
        }

        return true;
    }
    #endregion
}
