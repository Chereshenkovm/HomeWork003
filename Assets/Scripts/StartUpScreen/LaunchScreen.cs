using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LaunchScreen : MonoBehaviour
{
    [SerializeField] private MaskableGraphic elem;
    [SerializeField] private MaskableGraphic elem2;
    [SerializeField] private GameObject _startWindow;
    [Header("Время проигрывания интро")]
    [SerializeField] private float time;


    private void Start()
    {
        var s = DOTween.Sequence();
        s.Insert(0f, elem.DOFade(1f, time / 2));
        s.Insert(time / 2, elem.transform.DOMove(elem.transform.position + new Vector3(0, 2f, 0), time/2));
        s.Insert(time / 2, elem2.DOFade(1f, time / 2));
        s.Insert(time, elem2.DOFade(0, time / 2));
        s.Insert(time, elem.DOFade(0, time / 2));
        s.InsertCallback(3* time / 2, () =>
        {
            ChangeToStartMenu();
        });
    }

    private void ChangeToStartMenu()
    {
        gameObject.SetActive(false);
        _startWindow.SetActive(true);
    }
}
