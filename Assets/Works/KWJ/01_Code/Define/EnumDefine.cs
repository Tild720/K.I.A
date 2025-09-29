using System;

namespace KWJ.Define
{
    [Flags]
    public enum CookingType
    {
        None = 0,
        
        Boilable = 1 << 0, //삶기
        Bakeable = 1 << 1, //굽기
        Heatable = 1 << 2, //데우기
        
        Max = 1 << 3,
    }
    public enum CookingState
    {
        None = -1,
        
        Insufficient, //부족함
        Moderate, //적당함
        Excessive, //과함
        
        Max,
    }
    
    public enum IngredientType
    {
        None = 0,
        
        Bread,
        Patty,
        Lettuce,
        Tomato,
        LowQualityMeat,
        Broccoli,
        Potato,
        Mushroom,
        
        Max,
    }
    
    public enum FoodState
    {
        None = 0,
        
        Good,
        Normal,
        Bad,
                
        Max,
    }
    
    public enum FoodType
    {
        None = 0,
        
        //액체 음식
        Soup,
        Porridge,
        
        //고체 음식
        LowQualityMeat,
        Toast,
        Sandwich,
        //Burger,
        Beef,
        
        Max,
    }
}