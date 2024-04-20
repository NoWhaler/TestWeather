using Game.HorizontalSlidingArea.Model;

namespace Game.HorizontalSlidingArea.Presenter
{
    public class HorizontalSlidePresenter
    {
        private HorizontalSlideModel _horizontalSlideModel;

        public HorizontalSlidePresenter(HorizontalSlideModel horizontalSlideModel)
        {
            _horizontalSlideModel = horizontalSlideModel;
        }

        public float UpdateScrollValue()
        {
            return _horizontalSlideModel.UpdateScrollValue();
        }

        public void SetPlayerInteraction(bool isPLayerInteracting)
        {
            _horizontalSlideModel.SetPlayerInteraction(isPLayerInteracting);
        }
    }
}