using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Event {

    [Header("Doors")]
    public GameObject[] _availableDoorPrefabs;

    [Header("Scene")]
    public Sprite _stillImage;
    [TextArea]
    public string[] _dialogue;
    [TextArea]
    public string[] _response;

    [HideInInspector] public bool _eventIsActive;
    [HideInInspector] public EventController _myEventController;
}
