using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : UnitComponent
{

    private float ACCEPTABLE_DISTANCE = 0.5f;
    public float movespeed;
    public float rotationspeed;

    protected Vector3 m_location;

    //can only move to a certain location
    public void move(Vector3 _location, GameObject _unit)
    {
        if(Vector3.Distance(_location, _unit.transform.position) > ACCEPTABLE_DISTANCE)
        {
            Vector3 direction = _location - _unit.transform.position;
            direction.Normalize();
            direction *= movespeed;
            _unit.GetComponent<CharacterController>().SimpleMove(direction);
        }
    }
}
