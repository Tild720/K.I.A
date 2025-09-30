using Code.Core.EventSystems;
using Foods;
using KWJ.Interactable.PickUpable;

namespace Core.EventSystem
{
    public static class GetCookingToolEvents
    {
        public static GetCookingToolEvent GetCookingToolEvent = new GetCookingToolEvent();
    }
    public class GetCookingToolEvent : GameEvent
    {
        public PickUpable pickUpable;
        public int count;

        public GetCookingToolEvent Initialize(PickUpable pickUpable, int count)
        {
            this.pickUpable = pickUpable;
            this.count = count;
            return this;
        }
    }
}