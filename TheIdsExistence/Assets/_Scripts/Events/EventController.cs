using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour {

    [Header("Events")]
    [SerializeField] private Event[] _events;
    private int _nextEventIndex;
    private Event _currentEvent;

    [Header("Door Positioning")]
    [SerializeField] private float _xSpacing;
    private List<GameObject> _currentDoors;

    [Header("Story")]
    [SerializeField] private GameObject _storyCanvas;
    [SerializeField] private Image _stillImg;
    [SerializeField] private Text _dialogueTxt;

    private GameGod _gg;

    private void Start()
    {
        _currentDoors = new List<GameObject>();
        _gg = GetComponent<GameGod>();
        StartCoroutine(ActivateNextEvent());
    }

    public IEnumerator ActivateNextEvent()
    {
        if(_nextEventIndex >= _events.Length)
        {
            _gg.EndGame();
            yield return null;
        }

        DeactiveEvent(true);

        _currentEvent = _events[_nextEventIndex];
        _nextEventIndex++;

        StartCoroutine(DisplayCutScene(_currentEvent._stillImage, _currentEvent._dialogue));

        while(_storyCanvas.activeSelf)
            yield return null;

        CreateNewDoors(_currentEvent._availableDoorPrefabs);
    }

    public IEnumerator DisplayCutScene(Sprite image, string[] dialogue)
    {
        _storyCanvas.SetActive(true);
        _stillImg.sprite = image;

        for(int index = 0; index < _currentEvent._response.Length; index++)
        {
            bool isRead = false;
            _dialogueTxt.text = _currentEvent._response[FindResponseIndex()];

            while(!isRead)
            {
                if(Input.GetKeyDown(KeyCode.Return))
                    isRead = true;

                yield return null;
            }

            yield return null;


        }
        
        for(int index = 0; index < dialogue.Length; index++)
        {
            bool isRead = false;
            _dialogueTxt.text = dialogue[index];

            while(!isRead)
            {
                if(Input.GetKeyDown(KeyCode.Return))
                    isRead = true;

                yield return null;
            }

            yield return null;
        }

        _storyCanvas.SetActive(false);
        yield return null;
    }

    public void ReactivateEvent()
    {
        _currentEvent._eventIsActive = true;
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

        _gg._camAdjuster.Adjust();
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

    private int FindResponseIndex()
    {
        switch(_gg._activeGamemode)
        {
            case GameGod.GameMode.RAGE:
                return 0;

            case GameGod.GameMode.COMPASSION:
                return 1;

            case GameGod.GameMode.GREED:
                return 2;
        }

        return 0;
    }
}
