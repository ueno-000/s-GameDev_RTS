using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    public void UpdateSlider(float value)
    {
        _slider.value = value; 
    }
}
