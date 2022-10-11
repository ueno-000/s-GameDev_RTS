using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] public Slider HPSlider;
    // Start is called before the first frame update
    void Start()
    {
        HPSlider = GetComponent<Slider>();
    }

    public void MaxValueSetting(float value)
    {
        HPSlider.maxValue = value; 
    }

    public void UpdateSlider(float value)
    {
        HPSlider.value = value; 
    }
}
