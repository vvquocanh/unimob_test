using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FPSDisplay : MonoBehaviour
{
    #region Attributes

    private TextMeshProUGUI fpsDisplay = null;

    private float deltaTime = 0;
    
    #endregion
    #region Initialize
    private void Awake()
    {
        fpsDisplay = GetComponent<TextMeshProUGUI>();
    }

    #endregion

    #region Update

    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        if (!fpsDisplay)
        {
            Debug.LogError("FPS display is missing.");
            return;
        }
        
        fpsDisplay.SetText($"FPS: {Mathf.Ceil(fps)}");
    }

    #endregion
}
