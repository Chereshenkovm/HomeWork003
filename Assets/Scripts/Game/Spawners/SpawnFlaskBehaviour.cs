using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Zenject;

public class SpawnFlaskBehaviour : MonoBehaviour
{
    private DiContainer _container;

    [SerializeField] private Camera mainCamera;
    [Header("Префабы стены, колбы и портала")]
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _flaskPrefab;
    [SerializeField] private GameObject _portalPrefab;
    
    private Vector2 screenCoords;
    private GameObject[] _wall = new GameObject[4];
    private GameObject _flask;
    private GameObject _portal;

    private IEnumerator _spawner;
    
    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
    }
    
    private void OnEnable()
    {
        screenCoords = (Vector2)mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        
        _wall[0] = Object.Instantiate(_wallPrefab, new Vector3(-screenCoords.x, 0, 0), Quaternion.identity);
        _wall[0].GetComponent<SpriteRenderer>().size = new Vector2(1, screenCoords.y * 2);
        _wall[0].GetComponent<BoxCollider2D>().size = new Vector2(1, screenCoords.y * 2);
        _wall[1] = Object.Instantiate(_wallPrefab, new Vector3(screenCoords.x, 0, 0), Quaternion.identity);
        _wall[1].GetComponent<SpriteRenderer>().size = new Vector2(1, screenCoords.y * 2);
        _wall[1].GetComponent<BoxCollider2D>().size = new Vector2(1, screenCoords.y * 2);
        _wall[2] = Object.Instantiate(_wallPrefab, new Vector3(0, screenCoords.y, 0), Quaternion.identity);
        _wall[2].GetComponent<SpriteRenderer>().size = new Vector2(screenCoords.x * 2, 1);
        _wall[2].GetComponent<BoxCollider2D>().size = new Vector2(screenCoords.x * 2, 1);
        _wall[3] = Object.Instantiate(_wallPrefab, new Vector3(0, -screenCoords.y, 0), Quaternion.identity);
        _wall[3].GetComponent<SpriteRenderer>().size = new Vector2(screenCoords.x * 2, 1);
        _wall[3].GetComponent<BoxCollider2D>().size = new Vector2(screenCoords.x * 2, 1);

        _portal = Object.Instantiate(_portalPrefab, Vector3.zero, Quaternion.identity);

        _spawner = spawner();
        StartCoroutine(_spawner);
    }

    private void OnDisable()
    {
        foreach (var wall in _wall)
        {
            Destroy(wall);
        }
        Destroy(_portal);
        StopCoroutine(_spawner);
    }

    IEnumerator spawner()
    {
        while (true)
        {
            _container.InstantiatePrefab(_flaskPrefab, Vector3.zero, Quaternion.identity, transform);
            yield return new WaitForSeconds(1f);
        }
    }
}
