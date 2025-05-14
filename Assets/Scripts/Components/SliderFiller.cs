using UnityEngine;
using UnityEngine.UI;

public class SliderFiller : MonoBehaviour
{
    public Slider slider;          // ������ �� UI Slider
    public float targetValue = 1f; // ��������, �� �������� �����������
    public float fillSpeed = 1f; // �������� ���������� (������ � �������)

    private void Start()
    {
        slider.maxValue = targetValue; // ������������� ������������ �������� ��������
        slider.value = 0f; // ��������� ��������
    }

    private void OnEnable()
    {
        slider.maxValue = targetValue; // ������������� ������������ �������� ��������
        slider.value = 0f; // ��������� ��������
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
