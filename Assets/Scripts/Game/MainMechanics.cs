using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnBehaviour))]
[RequireComponent(typeof(ClickMechanics))]
[RequireComponent(typeof(TimeMechanics))]
public class MainMechanics : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [Header("����� ����� � ���������")]
    public int numberOfRows = 2;
    public int numberOfCollumns = 4;
    [Header("���������� �������")]
    public Vector2[] spawnPoints;

    public Dictionary<Vector2, bool> fullDictionary;

    private Vector2 screenCoords;


    // Start is called before the first frame update
    void Start()
    {
        screenCoords = (Vector2)mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        spawnPoints = new Vector2[numberOfRows * numberOfCollumns];
        fullDictionary = new Dictionary<Vector2, bool>();

        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < numberOfCollumns; j++)
            {
                spawnPoints[i * numberOfCollumns + j] = new Vector2((float)(2 * j + 1) / (numberOfCollumns * 2) * screenCoords.x * 2 - screenCoords.x, (float)(2 * i + 1) / (numberOfRows * 2) * screenCoords.y * 2 - screenCoords.y);
                fullDictionary.Add(spawnPoints[i * numberOfCollumns + j], false);
            }
        }
    }

    private void Awake()
    {
        Debug.Log("������� ����:\n ������ ����� �� 0 �� 100 �����,\n Ƹ���� ����� �� 100 �� 200 �����,\n ������� ����� �� 200 �� 400 �����,\n ����� ����� �������� 1000 �����,\n ������ �������� 50 �����,\n ��� ������� ���������� ����, ��� ������ ����� ����������.");
    }

    public void StartTheGame()
    {
        gameObject.GetComponent<SpawnBehaviour>().enabled = true;
        gameObject.GetComponent<ClickMechanics>().enabled = true;
        gameObject.GetComponent<TimeMechanics>().enabled = true;
    }
}
