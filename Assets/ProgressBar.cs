/// ProgressBar code acquired from Fractal Pixels: https://fractalpixels.com/devblog/unity-2D-progress-bars

using UnityEngine;
using UnityEngine.Events;

public class ProgressBar : FillBar
{

    // Event to invoke when the progress bar fills up
    [HideInInspector]
    public static UnityEvent onProgressComplete;

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
                onProgressComplete.Invoke();

            base.CurrVal = value;
        }
    }

    void Start()
    {
        // Initialize onProgressComplete and set a basic callback
        if (onProgressComplete == null)
            onProgressComplete = new UnityEvent();
        onProgressComplete.AddListener(OnProgressComplete);
    }

    void Update()
    {
        // Check to see if dog is pet
        if (ClickManager.isPet)
            CurrentValue += 0.0153f;
    }

    // The method to call when the progress bar fills up
    void OnProgressComplete()
    {
        Debug.Log("Progress Complete");
    }
}