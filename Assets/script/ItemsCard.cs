using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ItemType { 
    None,
    shoppingBasket,
    outModel,
    Cube,
    Ball,
    Beer,
    Toast,
    AppleJuice,
    BubbleGum,
    Butter,
    Beans,
    Coffee,
    CandyBar,
    Soup,
    Tuna,
    Cereal,
    ChickenNuggets,
    Cholocate,
    Cookies,
    CreameFraiche,
    CreamyBun,
    Eggs,
    FishSteak,
    FrenchFries,
    FrozenChicken,
    FrozenCulets,
    FrozenMeat,
    FrozenSalmon,
    IceCream,
    Jam,
    Ketchup,
    LunchMeat,
    Macaroni,
    Mayonnaise,
    Milk,
    Mustard,
    Noodles,
    PackBeer,
    Pasta,
    PeanutButter,
    PotatoChips,
    Pudding,
    Salami,
    SaltCrackers,
    Soda,
    SodaBottle,
    Sushi,
    WhippedCream,
}



public class ItemsCard : MonoBehaviour
{
    public ItemType itemType;

    public void OnClick() {
        HandManager.Instance.AddItem(itemType);
    }
}
