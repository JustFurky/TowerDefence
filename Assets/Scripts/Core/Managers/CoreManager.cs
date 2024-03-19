using System;
using UnityEngine;

namespace TD.Managers
{
    public class CoreManager : MonoBehaviour
    {
        /// <summary>
        /// Update events for all managers
        /// If Managers Subscribe this static action they can call update
        /// </summary>
        public static event Action UpdateTick;
        public static event Action FixedUpdateTick;

        public void UpdateFunc() => UpdateTick?.Invoke();
        public void FixedUpdateFunc() => FixedUpdateTick?.Invoke();

        private void Update() => UpdateTick?.Invoke();
        private void FixedUpdate() => FixedUpdateFunc();
    }
}
