using System;
using System.Collections;
using UnityEngine;

namespace Code.Lesson1.Task1
{
    public class Worker : MonoBehaviour
    {
        private Unit _unit;
        private float _time;
        public bool IsWorking = false;

        private void Start()
        {
            _unit = new Unit(0);

            if (!IsWorking) StartCoroutine(RecieveHealing());
        }

        public IEnumerator RecieveHealing()
        {
            _time = 0;
            IsWorking = true;
            while (_unit.Health <= 100 && _unit.Health + 5 <= 100 && _time < 3)
            {
                _time += 0.5f;
                yield return new WaitForSeconds(0.5f);
                _unit.Health += 5;
                Debug.Log(_unit.Health);
            }

            IsWorking = false;
        }
    }
}