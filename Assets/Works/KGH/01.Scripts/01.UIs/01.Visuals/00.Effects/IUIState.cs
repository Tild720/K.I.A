namespace UIs.Visuals.Effects
{
    public interface IUIState
    {
        string StateName { get; }
        void Initialize(VisualElement owner);
        void PlayEffect();
    }
}