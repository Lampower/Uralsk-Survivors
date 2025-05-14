using UnityEngine;
using UnityEngine.UI;

public class SliderFiller : MonoBehaviour
{
    public Slider slider;          // Ссылка на UI Slider
    public float targetValue = 1f; // Значение, до которого заполняется
    public float fillSpeed = 1f; // Скорость заполнения (единиц в секунду)

    private void Start()
    {
        slider.maxValue = targetValue; // Устанавливаем максимальное значение слайдера
        slider.value = 0f; // Начальное значение
    }

    private void OnEnable()
    {
        slider.maxValue = targetValue; // Устанавливаем максимальное значение слайдера
        slider.value = 0f; // Начальное значение
    }

    private void Update()
    {
        if (slider.value < targetValue)
        {
            slider.value += fillSpeed * Time.deltaTime;
            if (slider.value > targetValue)
                slider.value = targetValue;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void ResetSlider()
    {
        slider.value = 0f;
    }
}
