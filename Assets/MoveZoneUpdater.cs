using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZoneUpdater : MonoBehaviour
{
    public GameObject sphere;
    private Vector3 scaleChange;
    public GameObject soul;

    private void Awake() {
        soul = GameObject.Find("Soul");
    }

    private void Update() 
    {
        float jointDistance = soul.GetComponent<DistanceJoint2D>().distance; 
        scaleChange = new Vector3(jointDistance + 0.2f, jointDistance + 0.2f, 0);
        sphere.transform.localScale = scaleChange;
    }
}
