using Code.Core.EventSystems;
using Foods;

namespace Core.EventSystem
{
    public static class PurchaseEvents
    {
        public static PurchaseEvent PurchaseEvent = new PurchaseEvent();
        public static UseMoneyEvent UseMoneyEvent = new UseMoneyEvent();
    }
    public class PurchaseEvent : GameEvent
    {
        public FoodSO food;
        public int count;

        public PurchaseEvent Initialize(FoodSO food, int count)
        {
            this.food = food;
            this.count = count;
            return this;
        }
    }
    public class UseMoneyEvent : GameEvent
    {
        public int amount;

        public UseMoneyEvent Initialize(int amount)
        {
            this.amount = amount;
            return this;
        }
    }
}