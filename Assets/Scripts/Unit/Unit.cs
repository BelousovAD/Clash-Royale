using System;
using FSM;
using UnityEngine;

namespace Unit
{
    public class Unit : MonoBehaviour
    {
        public const float MinRadius = 0f;
        
        [Header("Only for towers")]
        [SerializeField][Min(MinRadius)] private float _radius;
        
        private StateMachine _stateMachine;
        private UnitType _type;

        public event Action Initialized;
        public event Action TypeChanged;

        public Health Health { get; private set; }

        public float Radius
        {
            get => _radius;
            private set => _radius = value < MinRadius ? MinRadius : value;
        }

        public IStateSwitcher StateSwitcher => _stateMachine;

        public UnitType Type
        {
            get
            {
                return _type;
            }

            private set
            {
                _type = value;
                TypeChanged?.Invoke();
            }
        }

        private void Update() =>
            _stateMachine?.Update(Time.deltaTime);

        private void LateUpdate() =>
            _stateMachine?.LateUpdate(Time.deltaTime);

        private void FixedUpdate() =>
            _stateMachine?.FixedUpdate(Time.fixedTime);

        public void Initialize(int health, float radius = MinRadius)
        {
            Health = new Health(health);
            Radius = radius;
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            Initialized?.Invoke();
        }

        public void SetType(UnitType type) =>
            Type = type;
    }
}