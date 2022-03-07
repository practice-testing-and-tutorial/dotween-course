using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Target : MonoBehaviour
{
    [SerializeField]
    private Sprite _sprite1;

    [SerializeField]
    private Sprite _sprite2;

    private SpriteRenderer spriteRenderer;

    private float _travel = 50f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _sprite1;
    }

    # region Controls
    public void Move(string direction)
    {
        switch(direction)
        {
            case "up":
                transform.DOMove(
                new Vector3(
                    transform.position.x, 
                    transform.position.y + _travel, 
                    transform.position.z), 
                1);  // {vector3, duration}
                break;
            case "right":
                transform.DOMove(
                new Vector3(
                    transform.position.x + _travel, 
                    transform.position.y, 
                    transform.position.z), 
                1);
                break;
            case "down":
                transform.DOMove(
                new Vector3(
                    transform.position.x, 
                    transform.position.y - _travel, 
                    transform.position.z), 
                1);
                break;
            case "left":
                transform.DOMove(
                new Vector3(
                    transform.position.x - _travel, 
                    transform.position.y, 
                    transform.position.z), 
                1);
                break;
        }
    }
    #endregion Controls

    #region Fade
    public void Fade(string type)
    {
        switch(type)
        {
            case "in":
                spriteRenderer.DOFade(1, 1); // {alpha, duration}
                break;
            case "out":
                spriteRenderer.DOFade(0, 1);
                break;
        }
    }

    public void FadeChangeImage()
    {
        var sequence = DOTween.Sequence();

        spriteRenderer.DOFade(0, 1).OnStepComplete(() => SwapSprite());
    }

    private Tween SwapSprite()
    {
        if (spriteRenderer.sprite == _sprite1)
        {
            spriteRenderer.sprite = _sprite2;
        }
        else
        {
            spriteRenderer.sprite = _sprite1;
        }

        return spriteRenderer.DOFade(1, 1);
    }
    #endregion Fade

    #region Sequence
    public void MoveUpAndRotate()
    {
        transform.DOMoveY(
            transform.position.y + _travel,
            1); // {endPoint, duration}

        transform.DORotate(new Vector3(0, 0, 180), 1); // {vector3, duration}
    }

    public void MoveDownThenRotate()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(
            transform.DOMoveY(
            transform.position.y - _travel,
            1)
        ).Append(
            transform.DORotate(new Vector3(0, 0, 0), 1)
        );
    }

    public void MoveUpDownIn(int loopTimes)
    {
        transform.DOMoveY(transform.position.y + _travel, 1)
        .SetEase(Ease.InOutSine)
        .SetLoops(loopTimes, LoopType.Yoyo);
    }
    #endregion Sequence
}
