/// ProgressBar code acquired from Fractal Pixels: https://fractalpixels.com/devblog/unity-2D-progress-bars

using UnityEngine;
using UnityEngine.Events;

public class ProgressBar : FillBar
{

    // Event to invoke when the progress bar fills up
    [HideInInspector]
    public static UnityEvent onProgressComplete;
    private static Vector2 MaskPos_start; 
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
        MaskPos_start = new Vector2(spriteMask.transform.position.x, spriteMask.transform.position.y);

        MaskPos = new Vector2(MaskPos_start.x, MaskPos_start.y);
    }

    void Update()
    {
        // if dog is pet
        if (ClickManager.isPet)
        {
            CurrentValue += 0.0153f;
        }
        MaskPos = new Vector2(Mathf.Lerp(MaskPos_start.x, 3.8f, (CurrentValue / slider.maxValue)), MaskPos_start.y);
        //Debug.Log("MaskPos_start: " + MaskPos_start.x +"\nCurr/Max: " + CurrentValue / slider.maxValue + "\nLerp: " + MaskPos.x);
        spriteMask.GetComponent<Transform>().position = new Vector2(MaskPos.x * 1.6744768f, MaskPos.y); // scale with canvas size
    }

    // The method to call when the progress bar fills up
    void OnProgressComplete()
    {
        Debug.Log("Progress Complete");
    }
}