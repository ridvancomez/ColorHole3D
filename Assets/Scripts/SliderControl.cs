using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    [SerializeField]
    private GameObject toplanacaklar;
    Slider slider;
    int maxValue;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        maxValue = toplanacaklar.transform.childCount;
        slider.maxValue = maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = maxValue - toplanacaklar.transform.childCount;
    }
}
