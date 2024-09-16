using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Image FadePanel;
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        FadePanel.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        FadePanel.gameObject.SetActive(false);
    }
    

    public void SceneChange()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        FadePanel.gameObject.SetActive(true);
        FadePanel.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("InGame");
    }
}
