using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace States
{
    public class ColorSourceGraphic : MonoBehaviour
    {
        public TMP_Text Text;
        public Image Image;
        public SpriteRenderer Sprite;

        private void Awake()
        {
            Text = GetComponent<TMP_Text>();
            Image = GetComponent<Image>();
            Sprite = GetComponent<SpriteRenderer>();
        }

        public void SetColor(Color color)
        {
            if (Text != null)
                Text.color = color;
            if (Image != null)
                Image.color = color;
            if (Sprite != null)
                Sprite.color = color;
        }
    }
}