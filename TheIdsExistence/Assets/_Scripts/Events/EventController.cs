using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    [Header("Events")]
    [SerializeField] private Event[] _events;
    private int _nextEventIndex;

    [Header("Door Positioning")]
    [SerializeField] private float _xSpacing;
    private List<GameObject> _currentDoors;

    private GameGod _gg;

    private void Start()
    {
        _currentDoors = new List<GameObject>();
        _gg = GetComponent<GameGod>();
        ActivateNextEvent();
    }

    public void ActivateNextEvent()
    {
        if(_nextEventIndex >= _events.Length)
        {
            _gg.EndGame();
            return;
        }

        DeactiveEvent(true);

        _events[_nextEventIndex].BeginEvent(this);

        _nextEventIndex++;
    }

    public void ReactivateEvent()
    {
        _events[_nextEventIndex - 1]._eventIsActive = true;
        EnableCurrentDoors();
    }

    public void DeactiveEvent(bool destroy)
    {
        for(int index = 0; index < _currentDoors.Count; index++)
        {
            if(destroy)
                Destroy(_currentDoors[index]);
            else
                _currentDoors[index].SetActive(false);
        }

        if(destroy)
            _currentDoors = new List<GameObject>();
    }

    public void CreateNewDoors(GameObject[] doors)
    {
        for(int index = 0; index < doors.Length; index++)
        {
            GameObject go = Instantiate(doors[index], transform.position, transform.rotation);
            Debug.Log(go);
            _currentDoors.Add(go);
        }

        int currentDoorAmount = _currentDoors.Count;

        for(int index = 0; index < currentDoorAmount; index++)
        {
            Vector3 currentPos = _currentDoors[index].transform.position;

            float xPos = index * _xSpacing;
            xPos -= (currentDoorAmount - 1) * (_xSpacing / 2);
            xPos = currentDoorAmount == 0 ? 0 : xPos;

            currentPos = new Vector3(xPos, currentPos.y);

            _currentDoors[index].transform.position = currentPos;
        }
    }

    public void EnableCurrentDoors()
    {
        foreach(GameObject go in _currentDoors)
        {
            bool enable = !go.GetComponent<Door>()._activated;

            if(enable)
                go.SetActive(true);
        }
    }
}
