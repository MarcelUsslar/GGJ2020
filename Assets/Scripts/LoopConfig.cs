using System;
using System.Collections.Generic;
using Events;
using UnityEngine;

[CreateAssetMenu(fileName = "LoopConfig", menuName = "Config/LoopConfig")]
public class LoopConfig : ScriptableObject
{
    [Serializable]
    private class EventMapping
    {
        public AbstractBasicTriggerView EventPrefab;
    }

    [SerializeField] private List<EventMapping> _events;
    [SerializeField] private float _shakeThreshold;
    [SerializeField] private float _requiredVolume;

    public int EventCount => _events.Count;
    public float ShakeThreshold => _shakeThreshold;
    public float RequiredVolume => _requiredVolume;
    
    public AbstractBasicTriggerView Prefab(int index)
    {
        return _events[index].EventPrefab;
    }
}