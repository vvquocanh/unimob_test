using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Spider))]
public class SpiderAIPath : AIPath
{
    #region Attribute
    private Action<Spider> onDestinationReached = null;

    private Spider spider = null;
        
    #endregion

    #region Initialize

    protected override void Awake()
    {
        base.Awake();
        spider = GetComponent<Spider>();
    }

    public override void OnTargetReached()
    {
        base.OnTargetReached();
        if (!spider)
        {
            Debug.LogError("Spider is missing.");
            return;
        }
        
        onDestinationReached?.Invoke(spider);
    }

    public void AddMethod(Action<Spider> method)
    {
        onDestinationReached += method;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onDestinationReached = null;
    }

    #endregion
    
    
}