using CoroutineShells;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed class SampleTextAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private List<TextSample> _samples;
    [SerializeField] private bool _isPlayingByAwake;

    private UniqueCoroutine _routine;
    private int _currentSampleIndex;
    private TextSample CurrentSample => _samples[_currentSampleIndex];

    public void StartAnimation()
    {
        if (!_routine.IsActive && _samples != null && _samples.Count > 0)
        {
            _currentSampleIndex = 0;
            _routine.Start();
        }
    }

    public void StopAnimation()
    {
        _routine.Stop();
    }

    private void Awake()
    {
        _routine = new UniqueCoroutine(this, () => Animation());
    }

    private void OnEnable()
    {
        StartAnimation();
    }

    private void OnDisable()
    {
        StopAnimation();
    }

    private IEnumerator Animation()
    {
        while (true)
        {
            _textField.text = CurrentSample.Text;
            yield return new WaitForSeconds(CurrentSample.DurationInSeconds);
            MoveNext();
        }
    }

    private void MoveNext()
    {
        _currentSampleIndex += 1;
        if (_currentSampleIndex >= _samples.Count)
        {
            _currentSampleIndex = 0;
        }
    }
}
