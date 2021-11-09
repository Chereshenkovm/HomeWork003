using System.Collections;
using UnityEngine;
using Core.Sounds;
using Zenject;

public class FlyAxeScript : MonoBehaviour
{
    private SoundManager _soundManager;
    
    [Header("Звуковая дорожка для полёта топора")]
    [SerializeField] private AudioClip axeFlyClip;

    private Rigidbody2D rb2d;
    private Transform tailTrans;
    private bool isLeft;
    private float deltaRotation = 2;

    [Inject] 
    private void Construct(SoundManager soundManager)
    {
        _soundManager = soundManager;
    }
    
    void Start()
    {
        StartCoroutine(Despawn());
        rb2d = GetComponent<Rigidbody2D>();
        tailTrans = transform.GetChild(0);
        
        _soundManager.CreateSoundObject().Play(axeFlyClip, transform.position, 1f);
        
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
