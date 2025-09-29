using Code.Core.EventSystems;
using KWJ.Define;

namespace Works.JW.Events
{
    public static class FoodEvents
    {
        public static readonly FoodEatEvent FoodEatEvent = new FoodEatEvent();
    }

    public class FoodEatEvent : GameEvent
    {
        public FoodType foodType;
        public FoodState foodState;

        public FoodEatEvent Init(FoodType foodType, FoodState foodState)
        {
            this.foodState = foodState;
            this.foodType = foodType;
            return this;
        }
    }
}