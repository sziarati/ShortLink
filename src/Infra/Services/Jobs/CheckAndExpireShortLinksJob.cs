using Domain.Interfaces.Repository.ShortLinks;
using Hangfire;

namespace Infra.Services.Jobs;

public class CheckAndExpireShortLinksJob(
    IRecurringJobManager recurringJobManager,
    IShortLinkRepository shortLinkRepository) : IJob
{
    private readonly IRecurringJobManager _recurringJobManager = recurringJobManager;
    private readonly IShortLinkRepository _shortLinkRepository = shortLinkRepository;

    public void RunJob()
    {
        _recurringJobManager.AddOrUpdate("CheckAndExpireShortLinksJob", () => CheckAndExpireShortLinks(), Cron.Daily);
    }

    public async Task CheckAndExpireShortLinks()
    {
        var expiredShortLinks = await _shortLinkRepository.GetAllExpiredShortLinksAsync();
        foreach (var item in expiredShortLinks)
        {
            item.ExpireShortLink();
        }
    }
}
