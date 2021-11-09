using System;
using System.Collections;
using GameNamespace;
using UnityEngine;

public class ClickMechanics : MonoBehaviour
{
    [SerializeField] private SpawnBehaviour _spawner;
    [SerializeField] private MainMechanics _main;
    
    [Header("Иконки для отображения наличия эффекта")]
    [SerializeField] private GameObject EffectIcon1;
    [SerializeField] private GameObject EffectIcon2;
    
    [Header("Системы частиц для бонусов")]
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private ParticleSystem ps2;
    
    [Header("Количество набранных очков")]
    private GameNamespace.Points _Points;

    private int PointsMultip = 1;
    private int GameMode = 2;
    
    private Vector2 mousePos;
    private RaycastHit2D hitObject;

    private void OnEnable()
    {
        _Points = _main._points;
        GameMode = _main.GameMode;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hitObject = Physics2D.Raycast(mousePos, Vector2.zero);
            if (GameMode == 1)
            {
                if (hitObject)
                {
                    if (hitObject.collider.gameObject.GetComponent<InflateMechanics>())
                    {
                        _Points.points += PointsMultip *
                                          hitObject.collider.GetComponent<InflateMechanics>().GetPoints(mousePos);
                        _spawner.clearSpawnPoint((Vector2) hitObject.collider.transform.parent.position);
                        Destroy(hitObject.collider.transform.parent.gameObject);
                    }
                    else if (hitObject.collider.gameObject.GetComponent<FlyAxeScript>())
                    {
                        if (hitObject.collider.gameObject.tag == "AxeFreeze")
                        {
                            if (!EffectIcon1.activeSelf)
                                StartCoroutine(timeSlow());
                            Destroy(hitObject.collider.gameObject);
                        }
                        else if (hitObject.collider.gameObject.tag == "AxeMult")
                        {
                            if (!EffectIcon2.activeSelf)
                                StartCoroutine(pointsDoubled());
                            Destroy(hitObject.collider.gameObject);
                        }
                    }
                }
                else
                {
                    _Points.points += -50;
                }
            }
            else if (GameMode == 2)
            {
                if (hitObject)
                {
                    if (hitObject.collider.gameObject.GetComponent<FlaskMechanics>())
                    {
                        _Points.points += hitObject.collider.gameObject.GetComponent<FlaskMechanics>().DamageFlask();
                    }
                }
            }
        }
    }

    IEnumerator timeSlow()
    {
        Time.timeScale = 0.5f;
        EffectIcon1.SetActive(true);
        ps.Play();
        yield return new WaitForSeconds(5f);
        ps.Stop();
        EffectIcon1.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator pointsDoubled()
    {
        PointsMultip = 2;
        EffectIcon2.SetActive(true);
        ps2.Play();
        yield return new WaitForSeconds(5f);
        ps2.Stop();
        EffectIcon2.SetActive(false);
        PointsMultip = 1;
    }
}
