using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sudocu
{
    public static class UIExtentions
    {
        private static Dictionary<Button, TMP_Text> _cachedButtonText = new Dictionary<Button, TMP_Text>();
        private static Dictionary<Button, Image> _cachedButtonImage = new Dictionary<Button, Image>();

        public static TMP_Text GetText(this Button button)
        {
            if (!_cachedButtonText.ContainsKey(button))
            {
                TMP_Text text = button.GetComponentInChildren<TMP_Text>();
                _cachedButtonText.Add(button, text);
            }

            return _cachedButtonText[button];
        }

        public static Image GetImage(this Button button)
        {
            if (!_cachedButtonImage.ContainsKey(button))
            {
                Image image = button.GetComponent<Image>();
                _cachedButtonImage.Add(button, image);
            }

            return _cachedButtonImage[button];
        }

        public static void SetOn(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        public static void SetOff(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}
