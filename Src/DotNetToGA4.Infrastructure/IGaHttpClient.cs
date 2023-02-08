using DotNetToGA4.Infrastructure.Models;

namespace DotNetToGA4.Infrastructure
{
    public interface IGaHttpClient
    {
        Task<Result> PostGaEvents(IEnumerable<Event> events, bool testEvents = false);
    }
}