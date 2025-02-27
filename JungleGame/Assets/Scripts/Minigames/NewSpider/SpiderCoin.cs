using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderCoin : MonoBehaviour
{
    public ActionWordEnum type;
    public Transform CoinParent;
    public Vector3 MoveUpPosition;
    private Vector3 Destination = new Vector3(5.6f, -.1f, 0f);
    public Vector3 Origin;
    private Vector3 scaleNormal = new Vector3(0.0046875f, 0.0046875f, 0f);
    private Vector3 scaleSmall = new Vector3(0.0031875f, 0.0031875f, 0f);
    public float moveSpeed = 5f;

    private Animator animator;
    private BoxCollider2D myCollider;
    private Image image;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play(type.ToString());

        RectTransform rt = GetComponent<RectTransform>();
        myCollider = gameObject.AddComponent<BoxCollider2D>();
        myCollider.size = rt.sizeDelta;

        image = GetComponent<Image>();
    }

    public void setOrigin()
    {
        Origin = transform.position;
    }

    public void grow()
    {
        StartCoroutine(growRoutine(scaleNormal));
    }
    public void shrink()
    {
        StartCoroutine(growRoutine(scaleSmall));
    }

    private IEnumerator growRoutine(Vector3 target)
    {
        Vector3 currStart = transform.localScale;
        float timer = 0f;
        float maxTime = 0.5f;

        while (true)
        {
            // animate movement
            timer += Time.deltaTime * 2;
            if (timer < maxTime)
            {
                transform.localScale = Vector3.Lerp(currStart, target, timer / maxTime);
            }
            else
            {
                transform.localScale = target;

                yield break;
            }

            yield return null;
        }
    }

    public void MoveUp()
    {
        StartCoroutine(BounceCoinUpRoutine());
    }

    private IEnumerator BounceCoinUpRoutine()
    {
        //GetComponent<LerpableObject>().
        yield return null;
    }

    public void MoveBack()
    {
        StartCoroutine(MoveUpDownRoutine(MoveUpPosition, .5f));
    }

    public void MoveDown()
    {
        StartCoroutine(MoveUpDownRoutine(Origin,.5f));
    }

    public void correct()
    {
        shrink();
        StartCoroutine(MoveUpDownRoutine(Destination, .5f));
    }

    private IEnumerator MoveUpDownRoutine(Vector3 target, float time)
    {
        Vector3 currStart = transform.position;
        float timer = 0f;
        float maxTime = time;

        while (true)
        {
            // animate movement
            timer += Time.deltaTime * 1;
            if (timer < maxTime)
            {
                transform.position = Vector3.Lerp(currStart, target, timer / maxTime);
            }
            else
            {
                transform.position = target;
                transform.SetParent(CoinParent);
                yield break;
            }

            yield return null;
        }
    }

    public void SetCoinType(ActionWordEnum type)
    {
        this.type = type;
        // get animator if null
        if (!animator)
            animator = GetComponent<Animator>();
        print ("coin type: " + type.ToString());
        animator.Play(type.ToString());
    }

    public void ToggleVisibility(bool opt, bool smooth)
    {
        if (smooth)
            StartCoroutine(ToggleVisibilityRoutine(opt));
        else
        {
            if (!image)
                image = GetComponent<Image>();
            Color temp = image.color;
            if (opt) { temp.a = 1f; }
            else { temp.a = 0; }
            image.color = temp;
        }
    }

    private IEnumerator ToggleVisibilityRoutine(bool opt)
    {
        float end = 0f;
        if (opt) { end = 1f; }
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            Color temp = image.color;
            temp.a = Mathf.Lerp(temp.a, end, timer);
            image.color = temp;

            if (image.color.a == end)
            {
                break;
            }
            yield return null;
        }
    }
}
