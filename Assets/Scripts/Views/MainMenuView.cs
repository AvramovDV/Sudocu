using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sudocu
{
    public class MainMenuView : BaseMenuView
    {
        [SerializeField] private Button _easyButton;
        [SerializeField] private Button _mediumButton;
        [SerializeField] private Button _hardButton;
        [SerializeField] private Button _languageButton;

        [SerializeField] private TMP_Text _easyButtonText;
        [SerializeField] private TMP_Text _mediumButtonText;
        [SerializeField] private TMP_Text _hardButtonText;
        [SerializeField] private TMP_Text _languageButtonText;

        [Inject] private LocalizationController _localizationController;
        [Inject] private GameModel _gameModel;

        protected override void OnEnable()
        {
            base.OnEnable();

            _easyButton.onClick.AddListener(() =>
            {
                _gameModel.GridDict[GridType.Small].GenerateGrid();
                ChangeMenu(MenuType.EasyMenu);
            });

            _mediumButton.onClick.AddListener(() =>
            {
                _gameModel.GridDict[GridType.Medium].GenerateGrid();
                ChangeMenu(MenuType.MediumMenu);
            });

            _hardButton.onClick.AddListener(() =>
            {
                _gameModel.GridDict[GridType.Big].GenerateGrid();
                ChangeMenu(MenuType.HardMenu);
            });

            _languageButton.onClick.AddListener(() => { _localizationController.SwitchLanguage(); });

            _localizationController.ChangeLanguageEvent += SetTexts;

            ChangeMenu(MenuType.MainMenu);
            SetTexts();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _easyButton.onClick.RemoveAllListeners();
            _mediumButton.onClick.RemoveAllListeners();
            _hardButton.onClick.RemoveAllListeners();
            _languageButton.onClick.RemoveAllListeners();

            _localizationController.ChangeLanguageEvent -= SetTexts;
        }

        private void SetTexts()
        {
            _easyButtonText.text = _localizationController.GetValue("easy");
            _mediumButtonText.text = _localizationController.GetValue("medium");
            _hardButtonText.text = _localizationController.GetValue("hard");
            _languageButtonText.text = _localizationController.GetValue("language");
        }
    }
}
