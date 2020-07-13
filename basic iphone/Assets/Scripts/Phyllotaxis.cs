using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour
{
    public float _degree, _scale;
    public int _numberStart;
    public int _maxIteration;
    public int _stepSize;

    public bool _useLerping;
    public float _intervalLerp;
    private bool _isLerping;
    private Vector3 _startPos, _endPos;
    private float _timeStartedLerping;

    private int _number;
    private int _currIteration;
    private TrailRenderer _trailRenderer;
    private Vector2 CalculatePhyllotaxis(float degree, float scale, int number)
    {
        double angle = number * (degree * Mathf.Deg2Rad);

        float r = scale * Mathf.Sqrt(number);

        float x = r * (float)System.Math.Cos(angle);
        float y = r * (float)System.Math.Sin(angle);

        Vector2 vec2 = new Vector2(x, y);

        return vec2;
    }

    private Vector2 _phyllotaxisPosition;

    void StartLerping()
    {
        _isLerping = true;
        _timeStartedLerping = Time.time;
        _phyllotaxisPosition = CalculatePhyllotaxis(_degree, _scale, _number);
        _startPos = this.transform.localPosition;
        _endPos = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
    }

    // Start is called before the first frame update
    void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();

        _number = _numberStart;

        transform.localPosition = CalculatePhyllotaxis(_degree, _scale, _number);

        if (_useLerping)
        {
            StartLerping();
        }
    }

    // Update is called once per frame
    private void FixedUpdate() 
    {
        if (_useLerping)
        {
            if (_isLerping)
            {
                float timeSinceStarted = Time.time - _timeStartedLerping;
                float percentageComplete = timeSinceStarted / _intervalLerp;
                transform.localPosition = Vector3.Lerp(_startPos, _endPos, percentageComplete);
                if (percentageComplete >= 0.99f)
                {
                    transform.localPosition = _endPos;
                    _number += _stepSize;
                    _currIteration++;
                    if (_currIteration <= _maxIteration)
                    {
                        StartLerping();
                    }
                    else
                    {
                        _isLerping = false;
                    }
                }
            }
        }
        else
        {
            _phyllotaxisPosition = CalculatePhyllotaxis(_degree, _scale, _number);
            transform.localPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
            _number += _stepSize;
            _currIteration++;
        }
    }
}
