using Application.Notification;
using Domain.Interfaces.Repository.ShortLinks;
using Hangfire;

namespace Infra.Services.Jobs;

public class CheckAndExpireShortLinksJob(
    IRecurringJobManager recurringJobManager,
    IShortLinkRepository shortLinkRepository,
    INotificationService notificationService) : IJob
{
    private readonly IRecurringJobManager _recurringJobManager = recurringJobManager;
    private readonly IShortLinkRepository _shortLinkRepository = shortLinkRepository;
    private readonly INotificationService _notificationService = notificationService;

    public void RunJob()
    {
        _recurringJobManager.AddOrUpdate("CheckAndExpireShortLinksJob", () => CheckAndExpireShortLinks(), Cron.Minutely);
    }

    public async Task CheckAndExpireShortLinks()
    {
        var expiredShortLinks = await _shortLinkRepository.GetAllExpiredShortLinksAsync();
        foreach (var item in expiredShortLinks)
        {
            var email = item.User.Email;
            await _notificationService.Notify(email.Value, $"dear {item.User.UserName} your link {item.UniqueCode} has been expired.", NotificationType.Email);
        }
    }
}
