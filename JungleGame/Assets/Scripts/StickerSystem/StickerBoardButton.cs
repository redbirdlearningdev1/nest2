using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class StickerBoardButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool interactable;
    private bool isPressed = false;

    public Animator stickerboardAnimator;

    /* 
    ################################################
    #   POINTER METHODS
    ################################################
    */

    private bool isOver = false;

    void OnMouseOver()
    {
        // skip if not interactable
        if (!interactable)
            return;
        
        if (!isOver)
        {
            isOver = true;
            GetComponent<LerpableObject>().LerpScale(new Vector2(1.1f, 1.1f), 0.1f);
        }
    }

    void OnMouseExit()
    {
        if (isOver)
        {
            isOver = false;
            GetComponent<LerpableObject>().LerpScale(new Vector2(1f, 1f), 0.1f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // return if not interactable
        if (!interactable || !isOver)
            return;

        if (!isPressed)
        {
            isPressed = true;
            GetComponent<LerpableObject>().LerpScale(new Vector2(0.9f, 0.9f), 0.1f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPressed && isOver)
        {
            // play audio blip
            AudioManager.instance.PlayFX_oneShot(AudioDatabase.instance.NeutralBlip, 1f);

            isPressed = false;
            GetComponent<LerpableObject>().LerpScale(new Vector2(1f, 1f), 0.1f);

            // open sticker board menu
            OpenStickerBoardMenu();
        }
    }

    public void OpenStickerBoardMenu()
    {
        stickerboardAnimator.Play("StickerBoardClicked");
        StickerSystem.instance.OpenStickerBoards();
    }
}
