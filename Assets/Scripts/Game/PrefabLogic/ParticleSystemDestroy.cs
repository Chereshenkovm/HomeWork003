using System.Collections;
using UnityEngine;

public class ParticleSystemDestroy : MonoBehaviour
{
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
