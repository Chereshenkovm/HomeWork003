using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDestroy : MonoBehaviour
{
    // Start is called before the first frame update

    public void StartCor(float _radiusEye)
    {
        StartCoroutine(LifeFunction(_radiusEye));
    }

    IEnumerator LifeFunction(float _radiusEye)
    {
        var shape = GetComponent<ParticleSystem>().shape;
        shape.radius = _radiusEye;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
