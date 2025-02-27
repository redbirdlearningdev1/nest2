﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    public static Bag instance;

    private int currBag = 0;
    private const int maxBag = 5;


    [Header("Objects")]
    [SerializeField] private Image bag;
    [SerializeField] private Image shadow;

    [Header("Images")]
    [SerializeField] private List<Sprite> bagSprites;
    [SerializeField] private List<Sprite> shadowSprites;

    void Awake()
    {   
        if (instance == null)
        {
            instance = this;
        }

        bag.sprite = bagSprites[currBag];
        shadow.sprite = shadowSprites[currBag];
    }

    public void UpgradeBag()
    {
        if (currBag < maxBag)
        {
            currBag++;
        }

        StartCoroutine(UpgradeBagRoutine());
        
        // play coin drop sound effect
        AudioManager.instance.PlayCoinDrop();
        // play right choice sound effect
        AudioManager.instance.PlayFX_oneShot(AudioDatabase.instance.RightChoice, 1f);
    }

    private IEnumerator UpgradeBagRoutine()
    {
        GetComponent<LerpableObject>().LerpScale(new Vector2(1.2f, 1.2f), 0.2f);
        bag.sprite = bagSprites[currBag];
        shadow.sprite = shadowSprites[currBag];
        yield return new WaitForSeconds(0.2f);
        GetComponent<LerpableObject>().LerpScale(new Vector2(1f, 1f), 0.2f);
    }
    

    public void DowngradeBag()
    {
        if (currBag > 0)
        {
            currBag--;
        }

        bag.sprite = bagSprites[currBag];
        shadow.sprite = shadowSprites[currBag];

        // play wrong choice sound effect
        AudioManager.instance.PlayFX_oneShot(AudioDatabase.instance.WrongChoice, 1f);
    }

    public void ToggleScaleAndWiggle(bool opt)
    {
        if (opt)
        {
            GetComponent<LerpableObject>().LerpScale(new Vector2(1.2f, 1.2f), 0.2f);
            GetComponent<WiggleController>().StartWiggle();
        }
        else
        {
            GetComponent<LerpableObject>().LerpScale(new Vector2(1f, 1f), 0.2f);
            GetComponent<WiggleController>().StopWiggle();
        }
    }
}
