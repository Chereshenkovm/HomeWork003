using Core.Sounds;
using UnityEngine;
using Random = UnityEngine.Random;
using Zenject;

public class FlaskMechanics : MonoBehaviour
{
    private SoundManager _soundManager;
    
    [Header("Передаём префаб системы частиц, который вызовется при разрушении")]
    [SerializeField] private GameObject ps;
    [Header("")]
    [SerializeField] private Rigidbody2D rb;
    [Header("Звуковые дорожки")]
    [SerializeField] private AudioClip breakClip;
    [SerializeField] private AudioClip wallCollClip;
    [SerializeField] private AudioClip GlassesCollClip;
    
    private int _maxHit = 4;
    
    private int hit = 0;
    private bool apQuit = false;
    private SpriteRenderer SR;
    
    [Inject] 
    private void Construct(SoundManager soundManager)
    {
        _soundManager = soundManager;
    }

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
        SR = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    public void KillFlask()
    {
        Instantiate(ps, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public int DamageFlask()
    {
        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, SR.color.a + 0.25f);
        hit += 1;

        if (hit > _maxHit)
        {
            Destroy(gameObject);
            return 1;
        }
        return 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag=="Wall")
            _soundManager.CreateSoundObject().Play(wallCollClip, transform.position, 1f, 0.13f);
        else if(other.collider.tag!="Wall")
            _soundManager.CreateSoundObject().Play(GlassesCollClip, transform.position, 0.5f, 0f);
        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, SR.color.a + 0.25f);
        hit += 1;
        
        if (hit > _maxHit)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag != "Wall")
        {
            rb.AddForce(10 * (Vector2) (transform.position - other.transform.position).normalized);
        }else if (other.collider.tag == "Wall")
        {
            rb.velocity = 10 * (Vector2) (-transform.position);
        }
    }

    private void OnApplicationQuit()
    {
        apQuit = true;
    }

    private void OnDestroy()
    {
        if (!apQuit)
        {
            var _pO = Instantiate(ps, transform.position, Quaternion.identity);
            _soundManager.CreateSoundObject().Play(breakClip, transform.position);
        }
    }
}
