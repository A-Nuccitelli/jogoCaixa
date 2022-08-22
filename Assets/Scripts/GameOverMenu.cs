using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    private LTDescr restartAnimation;
    private void OnEnable()
    {
        var rectTransform = GetComponent<RectTransform>();
        var gameOverCanvasGroup = GetComponent<CanvasGroup>();
        gameOverCanvasGroup.LeanAlpha(0, 0.01f);
        rectTransform.anchoredPosition = new Vector2(0, rectTransform.rect.height);

        gameOverCanvasGroup.LeanAlpha(1, 1f).delay = 0.3f;
        rectTransform.LeanMoveY(0, 1f).setEaseOutElastic().delay = 0.5f;

        if (restartAnimation is null)
        {
            restartAnimation = GetComponentInChildren<TMPro.TextMeshProUGUI>().gameObject
            .LeanScale(new Vector3(1.5f, 1.5f), 0.4f)
            .setLoopPingPong();
        }
        restartAnimation.resume();
    }
    public void Restart()
    {
        restartAnimation.pause();
        gameObject.SetActive(false);
        GameManager.Instance.Enable();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
