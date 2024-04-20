using Game.HorizontalSlidingArea.View;

namespace Game.HorizontalSlidingArea.Model
{
    public class HorizontalSlideModel
    {
        public float FractionSpeed { get; } = 0.001f;

        private float _currentScrollValue;

        private bool _isForward = true;

        private bool _isPlayerInteracting;

        private HorizontalSlideView _horizontalSlideView;

        private const float MIN_VALUE = 0f;

        private const float MAX_VALUE = 1f;

        public HorizontalSlideModel(HorizontalSlideView horizontalSlideView)
        {
            _horizontalSlideView = horizontalSlideView;
            _currentScrollValue = MIN_VALUE;
        }

        public float UpdateScrollValue()
        {
            if (!_isPlayerInteracting)
            {
                if (_isForward)
                {
                    _currentScrollValue += FractionSpeed;
                    if (_currentScrollValue >= MAX_VALUE)
                    {
                        _currentScrollValue = MAX_VALUE;
                        _isForward = false;
                    }
                }
                else
                {
                    _currentScrollValue -= FractionSpeed;
                    if (_currentScrollValue <= MIN_VALUE)
                    {
                        _currentScrollValue = MIN_VALUE;
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