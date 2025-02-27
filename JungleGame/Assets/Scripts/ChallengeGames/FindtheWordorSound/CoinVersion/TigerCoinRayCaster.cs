using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TigerCoinRayCaster : MonoBehaviour
{
    public static TigerCoinRayCaster instance;


    public bool isOn = false;
    public float moveSpeed;

    private GameObject selectedObject = null;
    [SerializeField] private Transform selectedObjectParent;

    private bool polaroidAudioPlaying = false;
    private Transform currentPolaroid;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        // return if off, else do thing
        if (!isOn)
            return;

        // drag select coin while mouse 1 down
        if (Input.GetMouseButton(0) && selectedObject)
        {
            Vector3 mousePosWorldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosWorldSpace.z = 0f;

            Vector3 pos = Vector3.Lerp(selectedObject.transform.position, mousePosWorldSpace, 1 - Mathf.Pow(1 - moveSpeed, Time.deltaTime * 60));
            selectedObject.transform.position = pos;
        }
        else if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            // send raycast to check for bag
            var pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (var result in raycastResults)
                {
                    if (result.gameObject.transform.CompareTag("Bag"))
                    {
                        TigerCoinGameManager.instance.ReturnToPos(selectedObject);
                        TigerCoinGameManager.instance.EvaluateWaterCoin(selectedObject.GetComponent<UniversalCoinImage>());
                    }
                }
            }

            TigerCoinGameManager.instance.ReturnToPos(selectedObject);
            selectedObject = null;
        }

        if (Input.GetMouseButtonDown(0))
        {

            var pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (var result in raycastResults)
                {
                    if (result.gameObject.transform.CompareTag("UniversalCoin"))
                    {
                        selectedObject = result.gameObject;
                        selectedObject.gameObject.transform.SetParent(selectedObjectParent);
                        TigerCoinGameManager.instance.PlayCoinAudio(selectedObject.GetComponent<UniversalCoinImage>());
                    }
                    else if (result.gameObject.transform.CompareTag("Polaroid"))
                    {
                        if (currentPolaroid != null)
                            currentPolaroid.GetComponent<Polaroid>().LerpScale(1f, 0.1f);
                        
                        currentPolaroid = result.gameObject.transform;
                        // play audio
                        StartCoroutine(PlayPolaroidAudio(currentPolaroid.GetComponent<Polaroid>().challengeWord.audio));
                    }
                }
            }
        }
    }

    private IEnumerator PlayPolaroidAudio(AudioClip audio)
    {
        if (polaroidAudioPlaying)
        {
            AudioManager.instance.StopTalk();
        }

        currentPolaroid.GetComponent<Polaroid>().LerpScale(1.1f, 0.1f);
        polaroidAudioPlaying = true;

        AudioManager.instance.PlayTalk(audio);
        yield return new WaitForSeconds(audio.length + 0.1f);

        currentPolaroid.GetComponent<Polaroid>().LerpScale(1f, 0.1f);
        polaroidAudioPlaying = false;
    }
}
