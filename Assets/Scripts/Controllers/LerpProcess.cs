using System;
using UnityEngine;

namespace Sudocu
{
    public class LerpProcess
    {
        private SceneMotor _sceneMotor;

        private Action<float> _action;
        private float _speed;
        private float _timer;
        private bool _isBusy;

        public LerpProcess()
        {
            _sceneMotor = GameObject.FindObjectOfType<SceneMotor>();
        }

        public void StartProcess(Action<float> action, float time)
        {
            if (!_isBusy)
            {
                _action = action;
                _speed = 1f / time;
                _timer = 0f;
                _sceneMotor.UpdateEvent += Execute;
                _isBusy = true;
            }
        }

        private void Execute()
        {
            if (_timer < 1f)
            {
                _timer += Time.deltaTime * _speed;
                _action.Invoke(_timer);
            }
            else
            {
                _action.Invoke(1f);
                EndProcess();
            }
        }

        private void EndProcess()
        {
            _sceneMotor.UpdateEvent -= Execute;
            _isBusy = false;
            _speed = 0f;
            _timer = 0f;
        }
    }
}
