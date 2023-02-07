using System.Text.Json.Serialization;

namespace DotNetToGA4.Infrastructure.Models;

public class Params
{
    public string? Currency { get; set; }
    public double? Value { get; set; }
    public string? Coupon { get; set; }

    [JsonPropertyName("payment_type")]
    public string? PaymentType { get; set; }
    [JsonPropertyName("virtual_currency_name")]
    public string VirtualCurrencyName { get; set; }

    [JsonPropertyName("group_id")]
    public string GroupId { get; set; }
    public int? Level { get; set; }
    public string? Character { get; set; }
    public int? Score { get; set; }
    public string? Method { get; set; }

    [JsonPropertyName("transaction_id")]
    public string? TransactionId { get; set; }
    public double? Shipping { get; set; }
    public double? Tax { get; set; }

    [JsonPropertyName("search_term")]
    public string? SearchTerm { get; set; }

    [JsonPropertyName("content_type")]
    public string? ContentType { get; set; }

    [JsonPropertyName("item_id")]
    public string? ItemId { get; set; }

    [JsonPropertyName("item_list_id")]
    public string? ItemListId { get; set; }

    [JsonPropertyName("item_list_name")]
    public string? ItemListName { get; set; }

    [JsonPropertyName("creative_name")]
    public string? CreativeName { get; set; }

    [JsonPropertyName("creative_slot")]
    public string? CreativeSlot { get; set; }

    [JsonPropertyName("promotion_id")]
    public string? PromotionId { get; set; }
    [JsonPropertyName("promotion_name")]
    public string? PromotionName { get; set; }

    [JsonPropertyName("item_name")]
    public string? ItemName { get; set; }
    [JsonPropertyName("achievement_id")]
    public string? AchievementId { get; set; }
    [JsonPropertyName("shipping_tier")]
    public string? ShippingTier { get; set; }

    public IEnumerable<Item> Items { get; set; }


}


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);





