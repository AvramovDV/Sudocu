using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sudocu
{
    public class SelectionValueView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _selectionGroup;
        [SerializeField] private GridType _gridType;

        [Inject] private SelectedCellModel _selectedCellModel;
        [Inject] private GameModel _gameModel;

        private List<Button> _buttonsList;

        private void Awake()
        {
            SetupButtonsList();
        }

        private void OnEnable()
        {
            SubscribeButtons();

            _selectedCellModel.ChangedSelectedCell += _selectionGroup.SetOn;
            _gameModel.GridDict[_gridType].ChangeActiveGridEvent += _selectionGroup.SetOff;
        }

        private void OnDisable()
        {
            UnsubscribeButtons();

            _selectedCellModel.ChangedSelectedCell -= _selectionGroup.SetOn;
            _gameModel.GridDict[_gridType].ChangeActiveGridEvent -= _selectionGroup.SetOff;
        }

        private void SetupButtonsList() => _buttonsList = gameObject.GetComponentsInChildren<Button>().ToList();

        private void SubscribeButtons()
        {
            for (int i = 0; i < _buttonsList.Count; i++)
            {
                int id = i;
                _buttonsList[i].onClick.AddListener(() =>
                {
                    Vector2Int xy = _selectedCellModel.SelectedCell;
                    _gameModel.GridDict[_gridType].SetActiveGridMember(xy.x, xy.y, id);
                    _selectionGroup.SetOff();
                });
            }
        }

        private void UnsubscribeButtons()
        {
            for (int i = 0; i < _buttonsList.Count; i++)
            {
                _buttonsList[i].onClick.RemoveAllListeners();
            }
        }
    }
}
