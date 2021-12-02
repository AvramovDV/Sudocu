using System;
using UnityEngine;

namespace Sudocu
{
    public class SceneMotor : MonoBehaviour
    {
        public event Action UpdateEvent = () => { };

        private void Update()
        {
            UpdateEvent.Invoke();
            Control();
        }

        private void Control()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                Application.Quit();
            }
        }
    }
}
