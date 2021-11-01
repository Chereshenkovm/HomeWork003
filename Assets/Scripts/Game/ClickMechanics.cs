using System;
using System.Collections;
using Game;
using UnityEngine;

public class ClickMechanics : MonoBehaviour
{
    [SerializeField] private SpawnBehaviour _spawner;
    [SerializeField] private MainMechanics _main;
    [SerializeField] private GameObject EffectIcon1;
    [SerializeField] private GameObject EffectIcon2;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private ParticleSystem ps2;
    
    [Header("Количество набранных очков")]
    private Game.Points _Points;

    private Vector2 mousePos;
    private RaycastHit2D hitObject;
    private int PointsMultip = 1;

    private void OnEnable()
    {
        _Points = _main._points;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hitObject = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hitObject)
            {
                if (hitObject.collider.gameObject.name == "Eye")
                {
                    _Points.points += PointsMultip *
                                      hitObject.collider.GetComponent<InflateMechanics>().GetPoints(mousePos);
                    _spawner.clearSpawnPoint((Vector2) hitObject.collider.transform.parent.position);
                    Destroy(hitObject.collider.transform.parent.gameObject);
                }

                if (hitObject.collider.gameObject.tag == "AxeFreeze")
                {
                    if(!EffectIcon1.activeSelf)
                        StartCoroutine(timeSlow());
                    Destroy(hitObject.collider.gameObject);
                }
                else if (hitObject.collider.gameObject.tag == "AxeMult")
                {
                    if(!EffectIcon2.activeSelf)
                        StartCoroutine(pointsDoubled());
                    Destroy(hitObject.collider.gameObject);
                }
            }
            else
            {
                _Points.points += -50;
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
