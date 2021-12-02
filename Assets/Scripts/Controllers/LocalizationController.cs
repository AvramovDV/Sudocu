using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sudocu
{
    public class LocalizationController
    {
        private readonly LocalizationLibrary _library;
        private Language _currentLanguage;

        private Dictionary<string, string> _languageDict = new Dictionary<string, string>();

        public LocalizationController(LocalizationLibrary library)
        {
            _library = library;

            LoadLanguage();
        }

        public event Action ChangeLanguageEvent = () => { };

        public Language CurrentLanguage
        {
            set
            {
                _currentLanguage = value;
                ChangeLanguage();
            }
        }

        public void SwitchLanguage()
        {
            CurrentLanguage = _currentLanguage == Language.English ? Language.Russian : Language.English;
        }

        public string GetValue(string key)
        {
            if (_languageDict.ContainsKey(key))
            {
                return _languageDict[key];
            }
            else
            {
                Debug.Log($"[LocalizationController] Key {key} does not exist");
                return null;
            }
        }

        private void LoadLanguage()
        {
            CurrentLanguage = (Language)PlayerPrefs.GetInt(Constants.LANGUAGEPREFSKEY, (int)Constants.DEFAULTLANGUAGE);
        }

        private void ChangeLanguage()
        {
            PlayerPrefs.SetInt(Constants.LANGUAGEPREFSKEY, (int)_currentLanguage);
            PlayerPrefs.Save();

            SetUpDict();

            ChangeLanguageEvent.Invoke();
        }

        private void SetUpDict()
        {
            _languageDict.Clear();

            LocalizationSet set = _library.GetLocalization(_currentLanguage);

            for (int i = 0; i < set.WordList.Count; i++)
            {
                _languageDict.Add(set.WordList[i].Key, set.WordList[i].Value);
            }
        }
    }
}
