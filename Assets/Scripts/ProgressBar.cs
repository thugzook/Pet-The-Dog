/// ProgressBar code acquired from Fractal Pixels: https://fractalpixels.com/devblog/unity-2D-progress-bars

using UnityEngine;
using UnityEngine.Events;

public class ProgressBar : FillBar
{

    // Event to invoke when the progress bar fills up
    [HideInInspector]
    public static UnityEvent onProgressComplete;
    private static Transform MaskPos_start; 
    public SpriteMask spriteMask;

    // Create a property to handle the slider's value
    public new float CurrentValue
    {
        get
        {
            return base.CurrVal;
        }
        set
        {
            // If the value exceeds the max fill, invoke the completion function
            if (value >= slider.maxValue)
            {
                onProgressComplete.Invoke();
                value = 0;
            }
            base.CurrVal = value;
        }
    }

    private Vector2 MaskPos;

    void Start()
    {
        // Initialize onProgressComplete and set a basic callback
        if (onProgressComplete == null)
            onProgressComplete = new UnityEvent();
        onProgressComplete.AddListener(OnProgressComplete);

        // Initialize starting postiion of Sprite Mask for progress
        if (MaskPos_start == null)
            MaskPos_start = spriteMask.GetComponent<Transform>();

        MaskPos = new Vector2(MaskPos_start.position.x, MaskPos_start.position.y);
    }

    void Update()
    {
        // if dog is pet
        if (ClickManager.isPet)
        {
            CurrentValue += 0.0153f;
        }
        MaskPos = new Vector2(MaskPos.x + (CurrentValue / slider.maxValue), MaskPos_start.position.y);
        spriteMask.GetComponent<Transform>().position = new Vector3(MaskPos.x, MaskPos.y, 0);
    }

    // The method to call when the progress bar fills up
    void OnProgressComplete()
    {
        Debug.Log("Progress Complete");
    }
}