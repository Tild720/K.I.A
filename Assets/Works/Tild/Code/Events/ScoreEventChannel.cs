using System.Collections.Generic;
using Code.Chat;
using Code.Core.EventSystems;

namespace Works.Tild.Code.Events
{
    public static class ScoreEventChannel
    {
        public static ResultEvent ResultEvent = new ResultEvent();
    }
    


    public class ResultEvent : GameEvent
    {
        public int Money;
        public int Used;
        public int Health;
        public int HealthFixed;
        public int Died;

        public ResultEvent Initializer(int money, int used, int health, int healthFixed, int died)
        {
            Money = money;
            Used = used;
            Health = health;
            HealthFixed = healthFixed;
            Died = died;
            return this;
        }
    }

}