// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using DotNetToGA4;
using Microsoft.Extensions.Logging;
using DotNetToGA4.Application;
using DotNetToGA4.Domain.Models.Sales.Content;
using DotNetToGA4.Domain.Models.Sales;
using DotNetToGA4.Domain.Models.Sales.Cart;
using DotNetToGA4.Domain.Models.Sales.Checkout;

Console.WriteLine("Hello, World!");

//setup our DI
var serviceProvider = new ServiceCollection()
    .AddLogging(opt =>
    {
        opt.AddConsole();
        opt.SetMinimumLevel(LogLevel.Debug);
    })
    .AddDotNetToGA4()
    .BuildServiceProvider();

var logg = serviceProvider.GetService<ILogger<Program>>();
logg.LogInformation("Lets get going");

var gaService = serviceProvider.GetRequiredService<IGaEventService>();

gaService.OverRideDefualtRunAs(DotNetToGA4.Domain.Notifications.RunAs.DryRun);

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

Console.ReadLine();



