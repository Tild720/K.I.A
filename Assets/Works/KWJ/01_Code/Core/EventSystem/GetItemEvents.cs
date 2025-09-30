using Code.Core.EventSystems;
using Foods;
using KWJ.Interactable.PickUpable;

namespace Core.EventSystem
{
    public static class GetItemEvents
    {
        public static GetItemEvent GetItemEvent = new GetItemEvent();
    }
    public class GetItemEvent : GameEvent
    {
        public Ingredient ingredient;

        public GetItemEvent Initialize(Ingredient ingredient)
        {
            this.ingredient = ingredient;
            return this;
        }
    }
}