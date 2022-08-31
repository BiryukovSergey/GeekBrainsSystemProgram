using System.Collections;
using UnityEngine;

namespace Code.Lesson1.Task1
{
    public class Worker : MonoBehaviour
    {
        private Unit _unit;
        private float _time;
        private bool _isWorking = false;

        private void Start()
        {
            _unit = new Unit(0);
            RecieveHealing();
        }

        private void RecieveHealing()
        {
            if (!_isWorking) StartCoroutine(Health());
        }

        private IEnumerator Health()
        {
            _time = 0;
            _isWorking = true;
            while (_unit.Health <= 100 && _unit.Health + 5 <= 100 && _time < 3)
            {
                _time += 0.5f;
                yield return new WaitForSeconds(0.5f);
                _unit.Health += 5;
                Debug.Log(_unit.Health);
            }

            _isWorking = false;
        }
    }
}