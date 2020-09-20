/// ProgressBar code acquired from Fractal Pixels: https://fractalpixels.com/devblog/unity-2D-progress-bars

using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    // Unity UI References
    public Slider slider;
    public Text displayText;

    // Private variables
    private float currVal = 0f;
    public float CurrVal
    {
        get {
            return currVal;
        }
        set {
            currVal = value;
            slider.value = currVal;
            displayText.text = (slider.value / slider.maxValue * 100).ToString("0.00") + "%";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrVal = 0f;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
