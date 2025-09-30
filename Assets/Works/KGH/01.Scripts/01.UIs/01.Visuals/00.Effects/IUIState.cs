using Cysharp.Threading.Tasks;

namespace UIs.Visuals.Effects
{
    public interface IUIState
    {
        string StateName { get; }
        void Initialize(VisualElement owner);
        UniTask PlayEffect();
    }
}