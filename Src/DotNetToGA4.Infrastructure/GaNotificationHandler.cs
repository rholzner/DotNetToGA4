using DotNetToGA4.Domain.Models.Content;
using DotNetToGA4.Domain.Models.Sales.Campaign;
using DotNetToGA4.Domain.Models.Sales.Cart;
using DotNetToGA4.Domain.Models.Sales.Checkout;
using DotNetToGA4.Domain.Models.Sales.Content;
using DotNetToGA4.Domain.Models.System;
using DotNetToGA4.Domain.Notifications;
using DotNetToGA4.Infrastructure.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace DotNetToGA4.Infrastructure;

public class GaNotificationHandler : INotificationHandler<GaNotification>
{
    private readonly IGaHttpClient gaHttpClient;
    private readonly ILogger<GaNotificationHandler> logger;
    private readonly InfrastructureSetting infrastructureSetting;

    public GaNotificationHandler(IGaHttpClient gaHttpClient, ILogger<GaNotificationHandler> logger, InfrastructureSetting infrastructureSetting)
    {
        this.gaHttpClient = gaHttpClient;
        this.logger = logger;
        this.infrastructureSetting = infrastructureSetting;
    }



    public async Task Handle(GaNotification notification, CancellationToken cancellationToken)
    {
        List<Event> events = new List<Event>();
        MapToGaEventTyp(notification, events);

        if (notification.Run == RunAs.DryRun || notification.Run == RunAs.notset)
        {
            foreach (var item in events)
            {
                logger.LogInformation("GaNotificationHandler:Handle:DryRun: {data}", JsonSerializer.Serialize(item, infrastructureSetting.GaJsonSerializerOptions));
            }
            return;
        }

        bool testRun = notification.Run == RunAs.TestRun;

        var r = await gaHttpClient.PostGaEvents(events, testRun);
        if (r.success && testRun)
        {
            logger.LogInformation("GaNotificationHandler:Handle:Sent {0} with Ok response", events.Count);
            return;
        }
        else if (testRun)
        {
            logger.LogError("GaNotificationHandler:Handle:Sent Failed to send {0} with error: {1}", events.Count, r.msg);
            return;
        }

        logger.LogInformation("GaNotificationHandler:Handle: done sending {0} events", events.Count);
    }

    internal static void MapToGaEventTyp(GaNotification notification, List<Event> events)
    {
        foreach (var item in notification.Events)
        {
            switch (item)
            {
                case Search search:
                    events.Add(GaEventBuilder.Search(search.SearchText));
                    break;
                case SignUp signup:
                    events.Add(GaEventBuilder.SignUp(signup.SignUpTo));
                    break;
                case Share share:
                    events.Add(GaEventBuilder.Share(share.SharedAs, share.TypOfItemShared, share.Id));
                    break;
                case Login login:
                    events.Add(GaEventBuilder.login(login.LoginWithSystem));
                    break;
                case ViewCampaignArea clickCampaign when clickCampaign is not null:
                    {
                        var campaingProducts = new List<Item>();
                        var hasAreaDataOnRoot = false;
                        var hasCampaingOnRoot = false;

                        if (!string.IsNullOrEmpty(clickCampaign?.CampaignName) && !string.IsNullOrEmpty(clickCampaign?.CampaignId))
                        {
                            hasCampaingOnRoot = true;
                        }

                        if (!string.IsNullOrEmpty(clickCampaign?.AreaName) && !string.IsNullOrEmpty(clickCampaign?.AreaId))
                        {
                            hasAreaDataOnRoot = true;
                        }

                        for (int i = 0; i < clickCampaign.Products.Count(); i++)
                        {
                            var product = clickCampaign.Products[i];

                            if (!hasAreaDataOnRoot && string.IsNullOrEmpty(product.AreaName) && string.IsNullOrEmpty(product.AreaId))
                            {
                                throw new DotNetToGA4Exception($"Error on product nr {i} - missing Area data on product or added it on root object");
                            }

                            if (!hasCampaingOnRoot && string.IsNullOrEmpty(product.CampaignName) && string.IsNullOrEmpty(product.CampaignId))
                            {
                                throw new DotNetToGA4Exception($"Error on product nr {i} - missing Campaign data on product or added it on root object");
                            }

                            campaingProducts.Add(new Item() { item_id = product.Id, item_name = product.Name, promotion_id = product.CampaignId, promotion_name = product.CampaignName });
                        }

                        if (hasAreaDataOnRoot && hasCampaingOnRoot)
                        {
                            events.Add(GaEventBuilder.ViewPromotion(campaingProducts, clickCampaign.AreaId, clickCampaign.AreaName, clickCampaign.CampaignName, clickCampaign.CampaignId));
                            continue;
                        }
                        events.Add(GaEventBuilder.ViewPromotion(campaingProducts));
                        break;
                    }

                case ClickCampaignArea clickCampaignArea:
                    {
                        var campaingProducts = new List<Item>();
                        var hasAreaDataOnRoot = false;
                        var hasCampaingOnRoot = false;

                        if (!string.IsNullOrEmpty(clickCampaignArea?.CampaignName) && !string.IsNullOrEmpty(clickCampaignArea?.CampaignId))
                        {
                            hasCampaingOnRoot = true;
                        }

                        if (!string.IsNullOrEmpty(clickCampaignArea?.AreaName) && !string.IsNullOrEmpty(clickCampaignArea?.AreaId))
                        {
                            hasAreaDataOnRoot = true;
                        }

                        for (int i = 0; i < clickCampaignArea.Products.Count(); i++)
                        {
                            var product = clickCampaignArea.Products[i];

                            if (!hasAreaDataOnRoot && string.IsNullOrEmpty(product.AreaName) && string.IsNullOrEmpty(product.AreaId))
                            {
                                throw new DotNetToGA4Exception($"Error on product nr {i} - missing Area data on product or added it on root object");
                            }

                            if (!hasCampaingOnRoot && string.IsNullOrEmpty(product.CampaignName) && string.IsNullOrEmpty(product.CampaignId))
                            {
                                throw new DotNetToGA4Exception($"Error on product nr {i} - missing Campaign data on product or added it on root object");
                            }

                            campaingProducts.Add(new Item() { item_id = product.Id, item_name = product.Name, promotion_id = product.CampaignId, promotion_name = product.CampaignName });
                        }

                        if (hasAreaDataOnRoot && hasCampaingOnRoot)
                        {
                            events.Add(GaEventBuilder.SelectPromotion(campaingProducts, clickCampaignArea.AreaId, clickCampaignArea.AreaName, clickCampaignArea.CampaignName, clickCampaignArea.CampaignId));
                            continue;
                        }
                        events.Add(GaEventBuilder.SelectPromotion(campaingProducts));
                        break;
                    }
                case AddToWishlist addToWishlist:
                    {
                        var products = new List<Item>();
                        foreach (var cartProduct in addToWishlist.Products)
                        {
                            products.Add(new Item() { item_id = cartProduct.Id, item_name = cartProduct.Name, quantity = cartProduct.Quantity, price = cartProduct.Price, discount = cartProduct.Discount, coupon = cartProduct.Coupon });
                        }

                        events.Add(GaEventBuilder.AddToWishlist(addToWishlist.Currency, addToWishlist.Value, products));
                        break;
                    }

                case RemoveFromCart removeFromCart:
                    {
                        var products = new List<Item>();
                        foreach (var cartProduct in removeFromCart.Products)
                        {
                            products.Add(new Item() { item_id = cartProduct.Id, item_name = cartProduct.Name, quantity = cartProduct.Quantity, price = cartProduct.Price, discount = cartProduct.Discount, coupon = cartProduct.Coupon });
                        }

                        events.Add(GaEventBuilder.RemoveFromCart(removeFromCart.Currency, removeFromCart.Value, products));
                        break;
                    }

                case ViewCart viewCart:
                    {
                        var products = new List<Item>();
                        foreach (var cartProduct in viewCart.Products)
                        {
                            products.Add(new Item() { item_id = cartProduct.Id, item_name = cartProduct.Name, quantity = cartProduct.Quantity, price = cartProduct.Price, discount = cartProduct.Discount, coupon = cartProduct.Coupon });
                        }

                        events.Add(GaEventBuilder.ViewCart(viewCart.Currency, viewCart.Value, products));
                        break;
                    }
                case AddToCart addToCart:
                    {
                        var products = new List<Item>();
                        foreach (var cartProduct in addToCart.Products)
                        {
                            products.Add(new Item() { item_id = cartProduct.Id, item_name = cartProduct.Name, quantity = cartProduct.Quantity, price = cartProduct.Price, discount = cartProduct.Discount, coupon = cartProduct.Coupon });
                        }

                        events.Add(GaEventBuilder.AddToCart(addToCart.Currency, addToCart.Value, products));
                        break;
                    }
                case AddPayment addPayment:
                    {
                        var products = new List<Item>();
                        foreach (var cartProduct in addPayment.Products)
                        {
                            products.Add(new Item() { item_id = cartProduct.Id, item_name = cartProduct.Name, quantity = cartProduct.Quantity, price = cartProduct.Price, discount = cartProduct.Discount, coupon = cartProduct.Coupon });
                        }

                        events.Add(GaEventBuilder.AddPaymentInfo(addPayment.Currency, addPayment.Value, products, addPayment.Coupon, addPayment.PaymentType));
                        break;
                    }
                case AddShipping addShipping:
                    {
                        var products = new List<Item>();
                        foreach (var cartProduct in addShipping.Products)
                        {
                            products.Add(new Item() { item_id = cartProduct.Id, item_name = cartProduct.Name, quantity = cartProduct.Quantity, price = cartProduct.Price, discount = cartProduct.Discount, coupon = cartProduct.Coupon });
                        }

                        events.Add(GaEventBuilder.AddShippingInfo(addShipping.Currency, addShipping.Value, products, addShipping.Coupon, addShipping.ShippingType));
                        break;
                    }

                case BeginCheckout addCheckout:
                    {
                        var products = new List<Item>();
                        foreach (var cartProduct in addCheckout.Products)
                        {
                            products.Add(new Item() { item_id = cartProduct.Id, item_name = cartProduct.Name, quantity = cartProduct.Quantity, price = cartProduct.Price, discount = cartProduct.Discount, coupon = cartProduct.Coupon });
                        }
                        events.Add(GaEventBuilder.BeginCheckout(addCheckout.Currency, addCheckout.Value, products, addCheckout.Coupon));
                        break;
                    }
                case Refund refund:
                    {
                        var products = new List<Item>();
                        foreach (var cartProduct in refund.Products)
                        {
                            products.Add(new Item() { item_id = cartProduct.Id, item_name = cartProduct.Name, quantity = cartProduct.Quantity, price = cartProduct.Price, discount = cartProduct.Discount, coupon = cartProduct.Coupon });
                        }
                        events.Add(GaEventBuilder.Refund(refund.Currency, refund.TransactionId, refund.Value, products, refund.Shipping, refund.Tax));
                        break;
                    }
                //Purchase
                case EndCheckout endCheckout:
                    {
                        var products = new List<Item>();
                        foreach (var cartProduct in endCheckout.Products)
                        {
                            products.Add(new Item() { item_id = cartProduct.Id, item_name = cartProduct.Name, quantity = cartProduct.Quantity, price = cartProduct.Price, discount = cartProduct.Discount, coupon = cartProduct.Coupon });
                        }
                        events.Add(GaEventBuilder.Purchase(endCheckout.Currency, endCheckout.TransactionId, endCheckout.Value, products, endCheckout.Shipping, endCheckout.Tax));
                        break;
                    }

                case ClickCategorie clickCategorie:
                    {
                        var products = new List<Item>();

                        foreach (var coreProduct in clickCategorie.CoreProducts)
                        {
                            products.Add(new Item() { item_id = coreProduct.Id, item_name = coreProduct.Name });
                        }

                        events.Add(GaEventBuilder.SelectItem(clickCategorie.PageId, clickCategorie.PageName, products));
                        break;
                    }
                case ClickProduct clickProduct:
                    {
                        var products = new List<Item>
                        {
                            new Item() { item_id = clickProduct.Product.Id, item_name = clickProduct.Product.Name }
                        };

                        events.Add(GaEventBuilder.SelectItem(clickProduct.Product.Id, clickProduct.Product.Name, products));
                        break;
                    }
                case ViewCategorie viewCategorie:
                    {
                        var products = new List<Item>();

                        foreach (var coreProduct in viewCategorie.CoreProducts)
                        {
                            products.Add(new Item() { item_id = coreProduct.Id, item_name = coreProduct.Name });
                        }

                        events.Add(GaEventBuilder.ViewItemList(viewCategorie.PageId, viewCategorie.PageName, products));
                        break;
                    }
                case ViewProduct viewProduct:
                    {
                        var products = new List<Item>
                        {
                            new Item() { item_id = viewProduct.Product.Id, item_name = viewProduct.Product.Name }
                        };

                        events.Add(GaEventBuilder.ViewItem(viewProduct.Currency, viewProduct.Value, products));
                        break;
                    }

            }
        }
    }


}

public class DotNetToGA4Exception : Exception
{
    public DotNetToGA4Exception(string error = "") : base(error)
    {

    }
}



