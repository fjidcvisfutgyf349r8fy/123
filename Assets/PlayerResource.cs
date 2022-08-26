using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerResource : MonoBehaviour
{
    [SerializeField] private float value = 0;
    [SerializeField] private float maxValue = 9000;
    public string resourceName = "money";

    public float GetValue() => value;
    public float GetMaxValue() => maxValue;

    public static PlayerResource Find(string name)
    {
        foreach (var resource in FindObjectsOfType<PlayerResource>())
        {
            if (resource.resourceName == name)
            {
                return resource;
            }
        }
        return null;
    }

    public event Action<float> ValueChangeEvent;
    public UnityEvent OnZero;
    public void ChangeValue(float change)
    {
        value = Mathf.Clamp(value + change, 0, maxValue);
        if (ValueChangeEvent != null)
        {
            ValueChangeEvent(value);
        }
        if (value == 0)
        {
            OnZero.Invoke();
        }
    }
}
