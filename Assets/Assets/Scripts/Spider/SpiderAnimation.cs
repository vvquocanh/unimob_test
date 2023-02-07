using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SpiderAnimation : MonoBehaviour
{
    #region Attributes

    [SerializeField] private List<AnimatorOverrideController> animatorList = new List<AnimatorOverrideController>();

    private Animator animator = null;

    #endregion

    #region Initialize

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (!IsAnimatorExist()) return;

        var animatorController = ListAccess.GetRandomElementFromList(animatorList);
        if (!animatorController) return;
        
        animator.runtimeAnimatorController = animatorController;
    }

    #endregion

    #region Methods

    public void SetMovingDirection(float x, float y)
    {
        if (!IsAnimatorExist()) return;
        
        animator.SetFloat("MovementX", x);
        animator.SetFloat("MovementY", y);
    }

    private bool IsAnimatorExist()
    {
        if (!animator)
        {
            Debug.LogError($"Animator is missing on spider {this.GetInstanceID()}");
            return false;
        }

        return true;
    }
    #endregion
}
