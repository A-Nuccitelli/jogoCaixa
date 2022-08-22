using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] private RectTransform scoreRectTransform;
    [SerializeField] private CanvasGroup scoreCanvasGroup;

    private void Start()
    {
        scoreCanvasGroup.LeanAlpha(0, 0.01f);
        scoreRectTransform.anchoredPosition = new Vector2(scoreRectTransform.anchoredPosition.x, 10);
        
        GetComponentInChildren<TMPro.TextMeshProUGUI>().gameObject
            .LeanScale(new Vector3(1.5f, 1.5f), 0.4f)
            .setLoopPingPong();
    }
    public void Play()
    {
        GetComponent<CanvasGroup>().LeanAlpha(0, 0.2f).setOnComplete(onComplete);
    }

    private void onComplete()
    {
        scoreCanvasGroup.LeanAlpha(1, 1.4f);
        scoreRectTransform.LeanMoveY(-30f, 1.5f).setEaseOutBounce();

        gameManager.Enable();
        Destroy(gameObject);
    }
}
