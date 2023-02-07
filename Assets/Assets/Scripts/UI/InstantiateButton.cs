using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Button))]
public class InstantiateButton : MonoBehaviour
{

    #region Attribute

    [SerializeField] private int instantiateNumber = 20;

    private Button button = null;

    private TextMeshProUGUI displayText = null;
    
    private Action<int> onButtonClick = null;
    #endregion

    #region Initialize

    private void Awake()
    {
        button = GetComponent<Button>();
        displayText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (displayText) displayText.SetText($"Create: {instantiateNumber}");

        if (button != null) button.clicked += ButtonClick;
    }

    #endregion
    
    #region Methods

    private void ButtonClick()
    {
        onButtonClick?.Invoke(instantiateNumber);
    }

    public void AddMethod(Action<int> method)
    {
        onButtonClick += method;
    }

    #endregion
}
