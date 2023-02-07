using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpiderAIPath), typeof(AIDestinationSetter), typeof(SpiderAnimation))]
public class Spider : MonoBehaviour
{
    #region Attributes

    [SerializeField] private float minimumSpeed = 4.0f;

    [SerializeField] private float maximumSpeed = 5.0f;
    
    private SpiderAIPath spiderAIPath = null;

    private AIDestinationSetter aiDestinationSetter = null;

    private Action<Spider> onDestinationReached = null;

    private SpiderAnimation spiderAnimation = null;
    #endregion

    #region Initialize

    private void Awake()
    {
        spiderAIPath = GetComponent<SpiderAIPath>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        spiderAnimation = GetComponent<SpiderAnimation>();
    }

    private void OnEnable()
    {
        if (!IsAIPathExist()) return;
        
        spiderAIPath.maxSpeed = Random.Range(minimumSpeed, maximumSpeed);
    }

    private void OnDisable()
    {
        onDestinationReached = null;
    }

    private void Update()
    {
        if (!spiderAIPath || !spiderAnimation) return;

        var velocity = spiderAIPath.velocity.normalized;
        spiderAnimation.SetMovingDirection(velocity.x, velocity.y);
    }

    #endregion

    #region Methods

    public void SetDestination(Transform target)
    {
        if (!IsAIDestinationSetterExist()) return;
        aiDestinationSetter.target = target;
    }

    public void AddMethod(Action<Spider> method)
    {
        if (!IsAIPathExist()) return;
        spiderAIPath.AddMethod(method);
        onDestinationReached += method;
    }

    private bool IsAIPathExist()
    {
        if (!spiderAIPath)
        {
            Debug.LogError("AI spider path is missing.");
            onDestinationReached?.Invoke(this);
            return false;
        }

        return true;
    }

    private bool IsAIDestinationSetterExist()
    {
        if (!aiDestinationSetter)
        {
            Debug.LogError("AI destination setter is missing.");
            onDestinationReached?.Invoke(this);
            return false;
        }

        return true;
    }
    #endregion
}
