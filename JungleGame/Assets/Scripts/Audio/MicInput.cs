﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicInput : MonoBehaviour
{
    #region SingleTon

    public static MicInput instance;

    #endregion

    public static float MicLoudness;
    public static float MicLoudnessinDecibels;

    private string _device;
    public int micDeviceIndex;

    void Awake() 
    {
        if (instance == null)
            instance = this;
        
        //InitMic();
    }

    //mic initialization
    public void InitMic()
    {
        // print ("mic init!");
        if (_device == null)
        {
            if (Microphone.devices.Length > 0)
            {
                _device = Microphone.devices[0];
                micDeviceIndex = 0;
                GameManager.instance.SendLog("MicInput", "audio input device set to: " + _device);
            }
            else
            {   
                GameManager.instance.SendLog("MicInput", "no microphone devices found");
                return;
            }
        }
        _clipRecord = Microphone.Start(_device, true, 999, 44100);
        _isInitialized = true;
    }

    public void StopMicrophone()
    {
        // print ("mic stopped!");
        Microphone.End(_device);
        _isInitialized = false;
    }

    public void SwitchDevice(int num)
    {
        if (num >= 0 && num < Microphone.devices.Length)
        {
            _device = Microphone.devices[num];
            micDeviceIndex = num;
            GameManager.instance.SendLog("MicInput", "switching audio input device to: " + _device);
        }
    }


    AudioClip _clipRecord;
    AudioClip _recordedClip;
    int _sampleWindow = 128;

    // get data from microphone into audioclip
    float MicrophoneLevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

    //get data from microphone into audioclip
    float MicrophoneLevelMaxDecibels()
    {
        float db = 20 * Mathf.Log10(Mathf.Abs(MicLoudness));

        return db;
    }

    public float FloatLinearOfClip(AudioClip clip)
    {
        StopMicrophone();

        _recordedClip = clip;

        float levelMax = 0;
        float[] waveData = new float[_recordedClip.samples];

        _recordedClip.GetData(waveData, 0);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _recordedClip.samples; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

    public float DecibelsOfClip(AudioClip clip)
    {
        StopMicrophone();

        _recordedClip = clip;

        float levelMax = 0;
        float[] waveData = new float[_recordedClip.samples];

        _recordedClip.GetData(waveData, 0);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _recordedClip.samples; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }

        float db = 20 * Mathf.Log10(Mathf.Abs(levelMax));

        return db;
    }



    void Update()
    {
        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere
        MicLoudness = MicrophoneLevelMax();
        MicLoudnessinDecibels = MicrophoneLevelMaxDecibels();
    }

    bool _isInitialized;
    // // start mic when scene starts
    // void OnEnable()
    // {
    //    InitMic();
    // }

    // //stop mic when loading a new level or quit application
    // void OnDisable()
    // {
    //     StopMicrophone();
    // }

    // void OnDestroy()
    // {
    //     StopMicrophone();
    // }


    // make sure the mic gets started & stopped when application gets focused
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            // Debug.Log("Focus");

            if (!_isInitialized)
            {
                //Debug.Log("Init Mic");
                InitMic();
            }
        }
        if (!focus)
        {
            // Debug.Log("Pause");
            StopMicrophone();
            //Debug.Log("Stop Mic");
        }
    }
}