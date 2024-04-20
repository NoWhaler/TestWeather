using Game.HorizontalSlidingArea.View;

namespace Game.HorizontalSlidingArea.Model
{
    public class HorizontalSlideModel
    {
        public float FractionSpeed { get; set; } = 0.001f;

        private float _currentScrollValue;

        private bool _isForward = true;

        private bool _isPlayerInteracting;

        private HorizontalSlideView _horizontalSlideView;

        public HorizontalSlideModel(HorizontalSlideView horizontalSlideView)
        {
            _horizontalSlideView = horizontalSlideView;
            _currentScrollValue = 0f;
        }

        public float UpdateScrollValue()
        {
            if (!_isPlayerInteracting)
            {
                if (_isForward)
                {
                    _currentScrollValue += FractionSpeed;
                    if (_currentScrollValue >= 1f)
                    {
                        _currentScrollValue = 1f;
                        _isForward = false;
                    }
                }
                else
                {
                    _currentScrollValue -= FractionSpeed;
                    if (_currentScrollValue <= 0f)
                    {
                        _currentScrollValue = 0f;
                        _isForward = true;
                    }
                }
                
                return _currentScrollValue;
            }

            _currentScrollValue = _horizontalSlideView.CurrentScrollbarValue();
            return _horizontalSlideView.CurrentScrollbarValue();
        }

        public void SetPlayerInteraction(bool isInteracting)
        {
            _isPlayerInteracting = isInteracting;
        }
    }
}