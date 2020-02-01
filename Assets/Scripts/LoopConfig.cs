using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoopConfig", menuName = "Config/LoopConfig")]
public class LoopConfig : ScriptableObject
{
    [Serializable]
    private class EventMapping
    {
        public InputDetection Trigger;
        public GameObject EventPrefab;
    }

    [SerializeField] private List<EventMapping> _events;
    [SerializeField] private float _shakeThreshold;

    public int EventCount => _events.Count;
    public float ShakeThreshold => _shakeThreshold;

    public InputDetection Trigger(int index)
    {
        return _events[index].Trigger;
    }

    public GameObject Prefab(int index)
    {
        return _events[index].EventPrefab;
    }
}