using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private UnitComponent _unitComponent;

    private void Start()
    {
        _unitComponent = gameObject.GetComponent<UnitComponent>();
    }

    public void MoveTo(Vector3 position)
    {
        float step = _unitComponent.GetSpeed()  * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);
    }
}
