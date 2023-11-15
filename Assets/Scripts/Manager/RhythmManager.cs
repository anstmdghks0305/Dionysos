using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RhythmManager : Singleton<RhythmManager>
{
   [SerializeField] private float rhythmSyncValue;
   public float RhythmSyncValue
    {
        get => rhythmSyncValue;

        set
        {
            rhythmSyncValue = (float)Math.Round(Mathf.Clamp(value, 0.1f, 1f), 3);
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        RhythmSyncValue = 0.75f;
    }
}
