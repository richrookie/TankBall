﻿using UnityEngine;

[System.Serializable]
public class DataManager
{
    ///<summary>Manager생산할때 만들어짐</summary>
    public void Init()
    {
        GetData();
    }


    public bool UseHaptic
    {
        get => _useHaptic;
        set
        {
            _useHaptic = value;
            ES3.Save<bool>("Haptic", value);
        }
    }
    [SerializeField]
    private bool _useHaptic;

    public bool UseSound
    {
        get => _useSound;
        set
        {
            _useSound = value;
            ES3.Save<bool>("Sound", value);
            Managers.Sound.BgmOnOff(value);
        }
    }
    [SerializeField]
    private bool _useSound;


    private int _stageNum = 1;
    private int _maxStageNum = 1;
    public int StageNum
    {
        get => _stageNum;
        set
        {
            _stageNum = value;

            if (_stageNum > _maxStageNum)
                _stageNum = _maxStageNum;

            ES3.Save<int>("StageNum", _stageNum);
        }
    }


    public void SaveData()
    {
    }

    public void GetData()
    {
        UseHaptic = ES3.Load<bool>("Haptic", true);
        UseSound = ES3.Load<bool>("Sound", true);
        StageNum = ES3.Load<int>("StageNum", 1);
    }
}
