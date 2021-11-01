using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FlyAxeScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float deltaRotation = 2;
    private Rigidbody2D rb2d;
    private bool isLeft;

    private Transform tailTrans;
    void Start()
    {
        StartCoroutine(Despawn());
        rb2d = GetComponent<Rigidbody2D>();
        tailTrans = transform.GetChild(0);
        
        switch (Random.Range(0,2))
        {
            case 0:
                gameObject.tag = "AxeFreeze";
                tailTrans.GetComponent<TrailRenderer>().startColor = Color.blue;
                tailTrans.GetComponent<TrailRenderer>().endColor = Color.blue;
                break;
            case 1:
                gameObject.tag = "AxeMult";
                tailTrans.GetComponent<TrailRenderer>().startColor = Color.red;
                tailTrans.GetComponent<TrailRenderer>().endColor = Color.red;
                break;
        }
        
        if (transform.position.x > 0)
        {
            isLeft = false;
            rb2d.velocity = new Vector2(-15, 5);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            tailTrans.localPosition =new Vector3(-tailTrans.localPosition.x, tailTrans.localPosition.y, 0);
        }else if (transform.position.x < 0)
        {
            isLeft = true;
            rb2d.velocity = new Vector2(15, 5);
        }
    }

    private void FixedUpdate()
    {
        if (isLeft)
        {
            rb2d.rotation -= deltaRotation;
        }else if (!isLeft)
        {
            rb2d.rotation += deltaRotation;
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
