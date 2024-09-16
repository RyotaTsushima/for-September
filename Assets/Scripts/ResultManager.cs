using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] Image FadePanel;
    [SerializeField] Text Text;
    [SerializeField] SpriteRenderer UnityImage;
    [SerializeField] Sprite UnityGoodSprite;
    [SerializeField] Sprite UnityBadSprite;
    [SerializeField] SpriteRenderer TokoImage;
    [SerializeField] Sprite TokoGoodSprite;
    [SerializeField] Sprite TokoBadSprite;
    void Start()
    {
        StartCoroutine(FadeIn());
        if (GameManager.IsCleared)
        {
            Text.text = "You Win";
            UnityImage.sprite = UnityGoodSprite;
            TokoImage.sprite = TokoGoodSprite;
        }
        else
        {
            Text.text = "You Lose";
            UnityImage.sprite = UnityBadSprite;
            TokoImage.sprite = TokoBadSprite;
        }
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeOut(string sceneName)
    {
        FadePanel.gameObject.SetActive(true);
        FadePanel.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        FadePanel.gameObject.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeIn()
    {
        FadePanel.gameObject.SetActive(true);
        FadePanel.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        FadePanel.gameObject.SetActive(false);
    }
}
