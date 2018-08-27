using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Event {

    [Header("Doors")]
    [SerializeField] private GameObject[] _availableDoorPrefabs;

    [Header("Scene")]
    [SerializeField] private Image _stillImage;
    [SerializeField] private string[] _dialogue;

    [HideInInspector] public bool _eventIsActive;
    [HideInInspector] public EventController _myEventController;

    // Use this for initialization
    public void BeginEvent(EventController eventController)
    {
        _myEventController = eventController;
        _myEventController.CreateNewDoors(_availableDoorPrefabs);
        _eventIsActive = true;
    }

    public void ExitEvent()
    {
        _eventIsActive = false;
    }
}
