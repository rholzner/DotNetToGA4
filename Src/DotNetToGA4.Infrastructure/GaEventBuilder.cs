using DotNetToGA4.Infrastructure.Models;

namespace DotNetToGA4.Infrastructure;
public record Result(bool success, string msg);

/// <summary>
/// https://developers.google.com/analytics/devguides/collection/protocol/ga4/reference/events#add_payment_info 
/// </summary>
public static class GaEventBuilder
{
    public const string ClientId = "";
    public static Event AddPaymentInfo(string currency, double value, IEnumerable<Item> items, string? coupon = null, string? paymentType = null)
    {
        return new Event()
        {
            Name = "add_payment_info",
            Params = new Params()
            {
                Currency = currency,
                Value = value,
                Coupon = coupon,
                PaymentType = paymentType,
                Items = items
            }
        };
    }

    public static Event AddShippingInfo(string currency, double value, IEnumerable<Item> items, string? coupon = null, string? shippingTier = null)
    {
        return new Event()
        {
            Name = "add_shipping_info",
            Params = new Params()
            {
                Currency = currency,
                Value = value,
                Coupon = coupon,
                ShippingTier = shippingTier,
                Items = items
            }
        };
    }

    public static Event AddToCart(string currency, double value, IEnumerable<Item> items)
    {
        return new Event()
        {
            Name = "add_to_cart",
            Params = new Params()
            {
                Currency = currency,
                Value = value,
                Items = items
            }
        };
    }

    public static Event AddToWishlist(string currency, double value, IEnumerable<Item> items)
    {
        return new Event() { Name = "add_to_wishlist", Params = new Params() { Currency = currency, Value = value, Items = items } };
    }

    public static Event BeginCheckout(string currency, double value, IEnumerable<Item> items, string? coupon = null)
    {
        return new Event()
        {
            Name = "begin_checkout",
            Params = new Params()
            {
                Currency = currency,
                Value = value,
                Coupon = coupon,
                Items = items
            }
        };
    }

    public static Event EarnVirtualCurrency(string virtualCurrencyName, double value)
    {
        return new Event()
        {
            Name = "earn_virtual_currency",
            Params = new Params()
            {
                VirtualCurrencyName = virtualCurrencyName,
                Value = value
            }
        };
    }


    public static Event GenerateLead(string currency, double value)
    {
        return new Event()
        {
            Name = "generate_lead",
            Params = new Params()
            {
                Currency = currency,
                Value = value
            }
        };
    }

    public static Event JoinGroup(string groupId)
    {
        return new Event()
        {
            Name = "join_group",
            Params = new Params()
            {
                GroupId = groupId
            }
        };
    }

    public static Event LevelUp(int lvl, string character)
    {
        return new Event() { Name = "level_up", Params = new Params() { Level = lvl, Character = character } };
    }


    public static Event login(string method)
    {
        return new Event() { Name = "login", Params = new Params() { Method = method } };
    }

    public static Event PostScore(int score, int lvl, string character)
    {
        return new Event()
        {
            Name = "post_score",
            Params = new Params()
            {
                Score = score,
                Level = lvl,
                Character = character
            }
        };
    }

    public static Event Purchase(string currency, string transaction_id, double value, IEnumerable<Item> items, double? shipping = null, double? tax = null)
    {
        return new Event() { Name = "purchase", Params = new Params() { Currency = currency, TransactionId = transaction_id, Value = value, Shipping = shipping, Tax = tax, Items = items } };
    }

    public static Event Refund(string currency, string transaction_id, double value, IEnumerable<Item> items, double? shipping = null, double? tax = null)
    {
        return new Event() { Name = "refund", Params = new Params() { Currency = currency, TransactionId = transaction_id, Value = value, Shipping = shipping, Tax = tax, Items = items } };
    }

    public static Event RemoveFromCart(string currency, double value, IEnumerable<Item> items)
    {
        return new Event() { Name = "remove_from_cart", Params = new Params() { Currency = currency, Value = value, Items = items } };
    }


    public static Event Search(string searchTerm)
    {
        return new Event() { Name = "search", Params = new Params() { SearchTerm = searchTerm } };
    }

    public static Event SelectContent(string contentType, string itemId)
    {
        return new Event() { Name = "select_content", Params = new Params() { ContentType = contentType, ItemId = itemId } };
    }

    public static Event SelectItem(string item_list_id, string item_list_name, IEnumerable<Item> items)
    {
        return new Event() { Name = "select_item", Params = new Params() { ItemListId = item_list_id, ItemListName = item_list_name, Items = items } };
    }

    public static Event SelectPromotion(string promotionId, string promotionName, IEnumerable<Item> items, string? creativeName = null, string? creativeSlot = null)
    {
        return new Event() { Name = "select_promotion", Params = new Params() { PromotionId = promotionId, PromotionName = promotionName, CreativeName = creativeName, CreativeSlot = creativeSlot, Items = items } };
    }

    public static Event Share(string method, string contentType, string itemId)
    {
        return new Event() { Name = "share", Params = new Params() { Method = method, ContentType = contentType, ItemId = itemId } };
    }

    public static Event SignUp(string method)
    {
        return new Event() { Name = "sign_up", Params = new Params() { Method = method } };
    }

    public static Event SpendVirtualCurrency(double value, string virtualCurrencyName, string itemName)
    {
        return new Event() { Name = "spend_virtual_currency", Params = new Params() { Value = value, VirtualCurrencyName = virtualCurrencyName, ItemName = itemName } };
    }

    public static Event TutorialBegin()
    {
        return new Event()
        {
            Name = "tutorial_begin"
        };
    }

    public static Event TutorialComplete()
    {
        return new Event() { Name = "tutorial_complete" };
    }

    public static Event UnlockAchievement(string achievementId)
    {
        return new Event() { Name = "unlock_achievement", Params = new Params() { AchievementId = achievementId } };
    }

    public static Event ViewCart(string currency, double value, IEnumerable<Item> items)
    {
        return new Event()
        {
            Name = "view_cart",
            Params = new Params()
            {
                Currency = currency,
                Value = value,
                Items = items
            }
        };
    }

    public static Event ViewItem(string currency, double value, IEnumerable<Item> items)
    {
        return new Event() { Name = "view_item", Params = new Params() { Currency = currency, Value = value, Items = items } };
    }

    public static Event ViewItemList(string itemListId, string itemListName, IEnumerable<Item> items)
    {
        return new Event() { Name = "view_item_list", Params = new Params() { ItemListId = itemListId, ItemId = itemListName, Items = items } };
    }

    public static Event ViewPromotion(string promotionId, string promotionName, IEnumerable<Item> items, string? creativeName = null, string? creativeSlot = null)
    {
        return new Event() { Name = "view_promotion", Params = new Params() { PromotionId = promotionId, PromotionName = promotionName, CreativeName = creativeName, CreativeSlot = creativeSlot, Items = items } };
    }

    public static Event ViewSearchResults(string searchTerm, IEnumerable<Item> items)
    {
        return new Event() { Name = "view_search_results", Params = new Params() { SearchTerm = searchTerm, Items = items } };
    }

}

