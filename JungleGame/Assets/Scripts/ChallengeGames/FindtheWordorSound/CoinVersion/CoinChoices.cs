using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinChoices : MonoBehaviour
{
    public ActionWordEnum type;
    public int logIndex;
    public Transform PolorParent;
    public Transform Pos;
    private Vector3 Destination = new Vector3(-5.58f, .55f, 0f);
    private Vector3 Start = new Vector3(-10.58f, .55f, 0f);
    public Vector3 Origin;
    public Vector3 Position1;
    public Vector3 Position2;
    public Vector3 Position3;
    public Vector3 Position4;
    public Vector3 Position5;
    private Vector3 EndDestination = new Vector3(12f, .55f, 0f);
    private Vector3 scaleNormal = new Vector3(.67f, .67f, 0f);
    private Vector3 scaleSmall = new Vector3(.33f, .33f, 0f);
    public float moveSpeed = 5f;

    private Animator animator;
    private BoxCollider2D myCollider;
    private Image image;
    private bool audioPlaying;

    void Awake()
    {
        animator = GetComponent<Animator>();
        print(type.ToString());
        animator.Play(type.ToString());

        RectTransform rt = GetComponent<RectTransform>();
        myCollider = gameObject.AddComponent<BoxCollider2D>();
        myCollider.size = rt.sizeDelta;

        image = GetComponent<Image>();

    }

    void Update()
    {

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
        Debug.Log(currStart);
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
    public void MoveOrigin()
    {
        StartCoroutine(MoveUpDownRoutine(Origin, .5f));
    }
    public void PoloroidMoveIn()
    {
        StartCoroutine(MoveUpDownRoutine(Destination, .5f));
    }
    public void PoloroidMoveOut()
    {
        StartCoroutine(MoveUpDownRoutine(EndDestination, .5f));
    }
    public void PoloroidMoveBase()
    {
        StartCoroutine(MoveUpDownRoutine(Start, .5f));
    }
    public void PoloroindMovePos1()
    {
        StartCoroutine(firstMove());
    }
    public void PoloroindMovePos2()
    {
        StartCoroutine(secondMove());
    }
    public void PoloroindMovePos3()
    {
        StartCoroutine(thirdMove());
    }
    public void PoloroindMovePos4()
    {
        StartCoroutine(fourthMove());
    }
    public void PoloroindMovePos5()
    {
        StartCoroutine(fifthMove());
    }


    public void PoloroindMovePos1Last()
    {
        StartCoroutine(firstLast());
    }
    public void PoloroindMovePos2Last()
    {
        StartCoroutine(secondLast());
    }
    public void PoloroindMovePos3Last()
    {
        StartCoroutine(thirdLast());
    }
    public void PoloroindMovePos4Last()
    {
        StartCoroutine(fourthLast());
    }
    private IEnumerator firstMove()
    {
        StartCoroutine(MoveUpDownRoutine(Destination, .1f));
        yield return new WaitForSeconds(1f);

    }
    private IEnumerator secondMove()
    {
        StartCoroutine(MoveUpDownRoutine(Destination, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position2, .1f));

    }
    private IEnumerator thirdMove()
    {
        StartCoroutine(MoveUpDownRoutine(Destination, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position2, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position3, .1f));

    }
    private IEnumerator fourthMove()
    {
        StartCoroutine(MoveUpDownRoutine(Destination, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position2, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position3, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position4, .1f));

    }
    private IEnumerator fifthMove()
    {
        StartCoroutine(MoveUpDownRoutine(Destination, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position2, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position3, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position4, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position5, .1f));

    }

    /// <summary>
    /// ////////////////////////////////////////////////This is for the end swipe all the cards
    /// </summary>
    /// <param name="target"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator firstLast()
    {

        StartCoroutine(MoveUpDownRoutine(Position2, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position3, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position4, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position5, .1f));

    }
    private IEnumerator secondLast()
    {

        StartCoroutine(MoveUpDownRoutine(Position3, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position4, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position5, .1f));

    }
    private IEnumerator thirdLast()
    {

        StartCoroutine(MoveUpDownRoutine(Position4, .1f));
        yield return new WaitForSeconds(.12f);
        StartCoroutine(MoveUpDownRoutine(Position5, .1f));

    }
    private IEnumerator fourthLast()
    {

        StartCoroutine(MoveUpDownRoutine(Position5, .1f));
        yield return new WaitForSeconds(0f);
    }


    private IEnumerator MoveUpDownRoutine(Vector3 target, float time)
    {
        //Debug.Log("Here");
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
                transform.SetParent(PolorParent);
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
        animator.Play(type.ToString());
    }

    public void PlayPhonemeAudio()
    {
        if (!audioPlaying)
        {
            StartCoroutine(PlayPhonemeAudioRoutine());
        }
    }

    private IEnumerator PlayPhonemeAudioRoutine()
    {
        audioPlaying = true;
        AudioManager.instance.PlayPhoneme(type);
        yield return new WaitForSeconds(1f);
        audioPlaying = false;
    }

    public void ToggleVisibility(bool opt, bool smooth)
    {
        Debug.Log("Toggle");
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
