using DotNetToGA4.Domain.Models.Sales.Cart;
using DotNetToGA4.Domain.Models.Sales.Checkout;
using DotNetToGA4.Domain.Models.Sales.Content;
using DotNetToGA4.Domain.Models.Sales;
using Microsoft.AspNetCore.Mvc;
using DotNetToGA4.Application;

namespace WebApiTest.Controllers;

[ApiController]
[Route("[controller]")]
public class GaTestController : ControllerBase
{
    [HttpGet("lessDataTest")]
    public async Task<IActionResult> LogToGa(IGaEventService gaService)
    {
        await gaService.Send(new ViewCategorie("CategoriePageNameTestId", "pageId", new CoreProduct[]
        {
            new CoreProduct("code1", "testProductName"),
            new CoreProduct("code2", "testProductName2"),
            new CoreProduct("code3", "testProductName3"),
            new CoreProduct("code4", "testProductName4"),
            new CoreProduct("code5", "testProductName5"),
        }));

        await gaService.Send(new ClickProduct("nok", 10, new CoreProduct("code2", "testProductName2")));
        await gaService.Send(new ViewProduct("nok", 10, new CoreProduct("code2", "testProductName2")));

        await gaService.Send(new AddToCart("nok", 10, new CartProduct("code2", "testProductName2", 1, 10)));

        await gaService.Send(new BeginCheckout("nok", 10, "", new CartProduct[] { new CartProduct("code2", "testProductName2", 1, 10) }));

        await gaService.Send(new AddPayment("nok", 10, "", "vipps", new CartProduct[] { new CartProduct("code2", "testProductName2", 1, 10) }));

        await gaService.Send(new AddShipping("nok", 10, "", "PostNord", new CartProduct[] { new CartProduct("code2", "testProductName2", 1, 10) }));

        await gaService.Send(new Purchase("CoreIdFromOtherSystem", "nok", 10, "", 10, 0, new CartProduct[] { new CartProduct("code2", "testProductName2", 1, 10) }));

        return Ok();
    }

    [HttpGet("MoreDataTest")]
    public async Task<IActionResult> LogToGa(IGaEventService gaService,[FromQuery] int nrOfRuns)
    {
        for (int i = 0; i < nrOfRuns; i++)
        {
            await gaService.Send(new ViewCategorie("CategoriePageNameTestId", "pageId", new CoreProduct[]
            {
                new CoreProduct("code1", "testProductName"),
                new CoreProduct("code2", "testProductName2"),
                new CoreProduct("code3", "testProductName3"),
                new CoreProduct("code4", "testProductName4"),
                new CoreProduct("code5", "testProductName5"),
            }));

            await gaService.Send(new ClickProduct("nok", 10, new CoreProduct("code2", "testProductName2")));
            await gaService.Send(new ViewProduct("nok", 10, new CoreProduct("code2", "testProductName2")));

            await gaService.Send(new AddToCart("nok", 10, new CartProduct("code2", "testProductName2", 1, 10)));

            await gaService.Send(new BeginCheckout("nok", 10, "", new CartProduct[] { new CartProduct("code2", "testProductName2", 1, 10) }));

            await gaService.Send(new AddPayment("nok", 10, "", "vipps", new CartProduct[] { new CartProduct("code2", "testProductName2", 1, 10) }));

            await gaService.Send(new AddShipping("nok", 10, "", "PostNord", new CartProduct[] { new CartProduct("code2", "testProductName2", 1, 10) }));

            await gaService.Send(new Purchase("CoreIdFromOtherSystem", "nok", 10, "", 10, 0, new CartProduct[] { new CartProduct("code2", "testProductName2", 1, 10) }));
        }


        return Ok();
    }
}
