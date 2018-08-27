using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    [SerializeField] private string[] _targetTags;

    [Range(0, 1)]
    [SerializeField] private float _sizeReductionFactor;

    public struct Range
    {
        public float max;
        public float min;
    }

    private Range _xRange;
    private Range _yRange;

    private void Start()
    {
    }

    public void Adjust()
    {
        for(int index = 0; index < _targetTags.Length; index++)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(_targetTags[index]);

            foreach(GameObject target in targets)
            {
                float xPos = target.transform.position.x;
                float yPos = target.transform.position.y;

                _xRange.max = _xRange.max < xPos ? xPos : _xRange.max;
                _xRange.min = _xRange.min > xPos ? xPos : _xRange.min;

                _yRange.max = _yRange.max < yPos ? yPos : _yRange.max;
                _yRange.min = _yRange.min > yPos ? yPos : _yRange.min;
            }

            Rect rect = new Rect(_xRange.min, _yRange.min, (_xRange.max - _xRange.min), (_yRange.max - _yRange.min));
            Camera.main.orthographicSize = rect.size.magnitude * _sizeReductionFactor;
        }
    }
}
