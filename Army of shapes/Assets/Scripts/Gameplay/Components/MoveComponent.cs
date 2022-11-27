using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private UnitComponent _unitComponent;
    private Rigidbody _rigidbody;
    private void Start()
    {
        _unitComponent = gameObject.GetComponent<UnitComponent>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    public void MoveTo(Vector3 position)
    {
        float step = _unitComponent.GetSpeed()  * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);
    }
    
    public void FollowTargetWithRotation(Transform target, float distanceToStop)
    {
        if(Vector3.Distance(transform.position, target.position) > distanceToStop)
        {
            transform.LookAt(target);
            _rigidbody.velocity = transform.forward * (_unitComponent.GetSpeed());
        }
        else
        {
            Stop();
        }
    }

    public void Stop()
    {
        _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
    }
}
