using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public Transform[] toPoints;

	public Transform getRandom()
    {
        return toPoints[Random.Range(0, toPoints.Length)];
    }
}
