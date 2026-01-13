using System;
using System.Collections;
using Bootstrap;
using UnityEngine;

namespace Timer
{
    public class CoroutineTimer
    {
        private const int Min = 0;
        private const int Second = 1;
        private readonly WaitForSeconds _delay = new (Second);

        private Coroutine _coroutine;
        private SavvyServicesProvider _services;
        private int _time;

        public CoroutineTimer(int max) =>
            Max = max;

        public event Action TimeChanged;
        public event Action TimeIsUp;

        public int Max { get; }

        public int Time
        {
            get
            {
                return _time;
            }

            private set
            {
                if (value != _time)
                {
                    _time = Mathf.Clamp(value, Min, Max);
                    TimeChanged?.Invoke();
                }
            }
        }

        public bool IsTimeUp => Time == Min;

        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        public void Add(int seconds)
        {
            _services.CoroutineRunner.StopCoroutine(_coroutine);
            Time += seconds;
            _coroutine = _services.CoroutineRunner.StartCoroutine(Countdown());
        }

        public void Stop()
        {
            _services.CoroutineRunner.StopCoroutine(_coroutine);
            Time = Min;
        }

        private IEnumerator Countdown()
        {
            while (Time > Min)
            {
                yield return _delay;

                Time--;
            }

            TimeIsUp?.Invoke();
        }
    }
}