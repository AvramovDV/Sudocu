using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sudocu
{
    public class GridScreenView : BaseMenuView
    {
        [SerializeField] private GameObject _buttonsObject;

        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _helpButton;
        [SerializeField] private Button _backButton;

        [SerializeField] private TMP_Text _victoryText;

        [SerializeField] private Color _accessableColor;
        [SerializeField] private Color _unAccessableColor;

        [SerializeField] private GridType _gridType;
        [SerializeField] private float _selectionTime;

        [Inject] private LocalizationController _localizationController;
        [Inject] private GameModel _gameModel;
        [Inject] private SelectedCellModel _selectedCellModel;

        private List<Button> _buttons;

        private LerpProcess _lerpProcess;

        protected override void OnEnable()
        {
            base.OnEnable();

            SubscribeGameButtons();

            _gameModel.GridDict[_gridType].ChangeActiveGridEvent += UpdateView;
            _gameModel.GridDict[_gridType].ChangeActiveGridMemberEvent += UpdateMemeberView;
            _gameModel.GridDict[_gridType].EndEvent += EndGame;

            _resetButton.onClick.AddListener(() => _gameModel.GridDict[_gridType].GenerateGrid());
            _restartButton.onClick.AddListener(() => _gameModel.GridDict[_gridType].ResetActiveGrid());
            _helpButton.onClick.AddListener(() => _gameModel.GridDict[_gridType].ShowRandomMember());
            _backButton.onClick.AddListener(() => ChangeMenu(MenuType.MainMenu));

            _localizationController.ChangeLanguageEvent += SetTexts;

            _lerpProcess = new LerpProcess();
            SetTexts();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            UnsubscribeGameButtons();

            _gameModel.GridDict[_gridType].ChangeActiveGridEvent -= UpdateView;
            _gameModel.GridDict[_gridType].ChangeActiveGridMemberEvent -= UpdateMemeberView;
            _gameModel.GridDict[_gridType].EndEvent -= EndGame;

            _resetButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
            _helpButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();

            _localizationController.ChangeLanguageEvent -= SetTexts;
        }

        private void Awake()
        {
            SetupButtonsList();
        }

        private void SetupButtonsList() => _buttons = _buttonsObject.GetComponentsInChildren<Button>().ToList();

        private void SubscribeGameButtons()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                Vector2Int xy = OneToTwo(i, _gameModel.GridDict[_gridType].MaxValue);
                _buttons[i].onClick.AddListener(() =>
                {
                    _gameModel.GridDict[_gridType].IncreaseActiveGridMember(xy.x, xy.y);
                    _selectedCellModel.SetSelectedCell(xy);
                });
            }
        }

        private void UnsubscribeGameButtons()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].onClick.RemoveAllListeners();
            }
        }

        private void UpdateView()
        {
            int[,] grid = _gameModel.GridDict[_gridType].GetActiveGrid();
            bool[,] accessGrid = _gameModel.GridDict[_gridType].GetAccessGrid();
            int size = grid.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int index = TwoToOne(new Vector2(j, i), size);
                    _buttons[index].GetText().text = grid[i, j] > 0 ? grid[i, j].ToString() : string.Empty;
                    _buttons[index].GetImage().color = accessGrid[i, j] ? _accessableColor : _unAccessableColor;
                }
            }

            _victoryText.enabled = false;
        }

        private void UpdateMemeberView(int x, int y)
        {
            int[,] grid = _gameModel.GridDict[_gridType].GetActiveGrid();
            int size = _gameModel.GridDict[_gridType].MaxValue;
            int index = TwoToOne(new Vector2(x, y), size);
            _buttons[index].GetText().text = grid[y, x] > 0 ? grid[y, x].ToString() : string.Empty;
            Image image = _buttons[index].GetImage();
            _lerpProcess.StartProcess(
                (t) =>
            {
                image.color = Color.Lerp(_unAccessableColor, _accessableColor, t);
            }, _selectionTime);
        }

        private void EndGame()
        {
            int[,] grid = _gameModel.GridDict[_gridType].GetActiveGrid();
            int size = grid.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int index = TwoToOne(new Vector2(j, i), size);
                    _buttons[index].GetText().text = grid[i, j].ToString();
                    _buttons[index].GetImage().color = _unAccessableColor;
                }
            }

            _victoryText.enabled = true;
        }

        private Vector2Int OneToTwo(int a, int size)
        {
            int y = Mathf.FloorToInt(a / size);
            int x = a - (size * y);
            Vector2Int res = new Vector2Int(x, y);
            return res;
        }

        private int TwoToOne(Vector2 a, int size)
        {
            int res = (int)(a.x + (size * a.y));
            return res;
        }

        private void SetTexts()
        {
            _resetButton.GetText().text = _localizationController.GetValue("reset");
            _restartButton.GetText().text = _localizationController.GetValue("restart");
            _helpButton.GetText().text = _localizationController.GetValue("help");
            _backButton.GetText().text = _localizationController.GetValue("back");
            _victoryText.text = _localizationController.GetValue("victory");
        }
    }
}
