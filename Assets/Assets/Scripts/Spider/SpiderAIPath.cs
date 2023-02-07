using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Spider))]
public class SpiderAIPath : AILerp
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
        onDestinationReached = null;
    }

    public void AddMethod(Action<Spider> method)
    {
        onDestinationReached += method;
    }

    #endregion
    
    
}