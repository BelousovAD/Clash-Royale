using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap;
using Rarity;
using Timer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Chest
{
    public class Chest : Item.Item
    {
        private const float MinRandomValue = 0f;
        
        private readonly float _totalChance;
        private bool _isLocked = true;
        private bool _isUnlocking;

        public Chest(ChestData data, int id = DefaultId)
            : base(data, id)
        {
            _totalChance = Chances.Sum(chance => chance.Percent);
            Timer = new CoroutineTimer(UnlockTime);
            Timer.TimeIsUp += Unlock;
            Timer.TimeChanged += Save;
        }

        public event Action LockStatusChanged;
        public event Action UnlockingStatusChanged;

        public IReadOnlyList<Chance> Chances => Data.Chances;

        public GameObject Prefab => Data.Prefab;

        public int UnlockTime => Data.UnlockTime;

        public bool IsLocked
        {
            get
            {
                return _isLocked;
            }

            private set
            {
                if (value != _isLocked)
                {
                    _isLocked = value;
                    Save();
                    LockStatusChanged?.Invoke();
                }
            }
        }

        public bool IsUnlocking
        {
            get
            {
                return _isUnlocking;
            }
            
            private set
            {
                if (value != _isUnlocking)
                {
                    _isUnlocking = value;
                    Save();
                    UnlockingStatusChanged?.Invoke();
                }
            }
        }

        public CoroutineTimer Timer { get; }

        private new ChestData Data => base.Data as ChestData;

        public override void Initialize(SavvyServicesProvider servicesProvider)
        {
            base.Initialize(servicesProvider);
            Timer.Initialize(servicesProvider);
        }

        public override void Dispose()
        {
            Timer.TimeChanged -= Save;
            Timer.TimeIsUp -= Unlock;
            base.Dispose();
        }

        public RarityType GetRandomRarity()
        {
            float take = Random.Range(MinRandomValue, _totalChance);
            float chanceSum = 0f;

            foreach (Chance chance in Chances)
            {
                chanceSum += chance.Percent;

                if (take < chanceSum)
                {
                    return chance.Rarity;
                }
            }

            return RarityType.Legendary;
        }

        public override void Load()
        {
            base.Load();
            SaveData saveData = Services.Preferences.LoadJson(Type + Subtype + Id, new SaveData()
            {
                IsLocked = true,
                IsUnlocking = false,
                Remain = Data.UnlockTime,
            });

            _isLocked = saveData.IsLocked;
            _isUnlocking = saveData.IsUnlocking;

            if (IsUnlocking)
            {
                Timer.Add(saveData.Remain);
            }
        }

        public void StartUnlocking()
        {
            if (IsUnlocking || IsLocked == false)
            {
                Debug.LogError($"Chest is already unlocked or is unlocking");
                return;
            }

            Timer.Add(Data.UnlockTime);
            IsUnlocking = true;
        }

        protected override void DeleteSaves()
        {
            base.DeleteSaves();
            Services.Preferences.DeleteKey(Type + Subtype + Id);
        }

        protected override void Save()
        {
            SaveData saveData = new ()
            {
                IsLocked = IsLocked,
                IsUnlocking = IsUnlocking,
                Remain = Timer.Time,
            };
            
            Services.Preferences.SaveJson(Type + Subtype + Id, saveData);
            base.Save();
        }

        private void Unlock()
        {
            IsLocked = false;
            IsUnlocking = false;
        }
        
        [Serializable]
        private struct SaveData
        {
            public bool IsLocked;
            public bool IsUnlocking;
            public int Remain;
        }
    }
}