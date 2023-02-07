namespace DotNetToGA4.Infrastructure.Models;

public class Item
{
    public string item_id { get; set; }
    public string item_name { get; set; }
    public string? affiliation { get; set; }
    public string? coupon { get; set; }
    public string? currency { get; set; }
    public double? discount { get; set; }
    public int? index { get; set; }
    public string? item_brand { get; set; }
    public string? item_category { get; set; }
    public string? item_category2 { get; set; }
    public string? item_category3 { get; set; }
    public string? item_category4 { get; set; }
    public string? item_category5 { get; set; }
    public string? item_list_id { get; set; }
    public string? item_list_name { get; set; }
    public string? item_variant { get; set; }
    public string? location_id { get; set; }
    public double? price { get; set; }
    public int? quantity { get; set; }

    public string? creative_name { get; set; }
    public string? creative_slot { get; set; }
    public string? promotion_id { get; set; }
    public string? promotion_name { get; set; }





}


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);





