using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlideShow : MonoBehaviour
{
    [SerializeField] private RectTransform viewport;
    [SerializeField] private RectTransform content;
    [SerializeField] private float duration;
    [SerializeField] private bool vertical;
    [SerializeField] private GameObject previousButton;
    [SerializeField] private GameObject nextButton;

    private int max = 1;
    private int current = 0;

    private void Start()
    {
        DOTween.Sequence().AppendInterval(.5f).AppendCallback(() =>
        {
            if (vertical)
            {
                max = (int)(content.rect.height / viewport.rect.height);
            }
            else
            {
                max = (int)(content.rect.width / viewport.rect.width);
            }
        });

        CheckButton();
    }

    public void OnClickPrevious()
    {
        if (current > 0)
        {
            if (vertical)
            {
                content.DOLocalMoveY(content.localPosition.y - viewport.rect.height, duration);
                current--;
            }
            else
            {
                content.DOLocalMoveX(content.localPosition.x + viewport.rect.width, duration);
                current--;
            }
        }

        CheckButton();
    }

    public void OnClickNext()
    {
        if (current < max)
        {
            if (vertical)
            {
                content.DOLocalMoveY(content.localPosition.y + viewport.rect.height, duration);
                current++;
            }
            else
            {
                content.DOLocalMoveX(content.localPosition.x - viewport.rect.width, duration);
                current++;
            }
        }

        CheckButton();
    }

    public void CheckButton()
    {
        previousButton.SetActive(current > 0);
        nextButton.SetActive(current < max);
    }
}
