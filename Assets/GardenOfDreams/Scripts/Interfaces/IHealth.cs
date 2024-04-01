using System;

namespace Assets.GardenOfDreams.Scripts.Interfaces
{
    public interface IHealth
    {
        public float MaxHealth { get; }
        public float CurrentHealth { get; }
        public Action OnHealthChange { get; set; }
    }
}