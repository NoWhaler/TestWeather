using UnityEngine;
using UnityEngine.UI;

namespace Game.ProgressBar
{
    public class LoadingBar: MonoBehaviour
    {
        [SerializeField] private Image _progressBarFill;

        public void UpdateFraction(float fraction)
        {
            _progressBarFill.fillAmount = Mathf.Clamp(fraction / 0.9f, 0, 1);
        }
    }
}