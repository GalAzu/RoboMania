using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    private void Start()
    {
        bar = transform;
    }

    public void SetSize(float sizeNormalized)
    {
        if(bar != null)
        bar.localScale = new Vector3(sizeNormalized , 1f);
    }
}
