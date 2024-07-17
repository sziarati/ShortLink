using Domain.Events.ShortLinkExpired;
using System.Text;

namespace Domain.Entities.ShortLinkAggregate;

public partial class ShortLink
{
    public ShortLink(string name, string originUrl, int userId)
    {
        UserId = userId;
        Name = name;
        CreateDate = DateTime.Now;
        ExpireDate = DateTime.Now.AddDays(ExpiryDays);
        IsExpired = false;

        string uniqueCode = GenerateCode(originUrl);
        UniqueCode = uniqueCode;
        OriginUrl = originUrl;
    }
    
    public static string GenerateCode(string url)
    {

        if (url is null)
        {
            throw new ArgumentNullException(nameof(url));
        }

        var urlBytes = Encoding.UTF8.GetBytes(url);
        var urlBase64 = Convert.ToBase64String(urlBytes);
        var ShortLink = urlBase64.Length >= Length ? urlBase64.Substring(0, Length) : urlBase64;

        return ShortLink;
    }

    public bool IsShortLinkExpired()
    {
        return IsExpired || IsExpiredBasedOnExpiryDay();
    }

    public bool IsExpiredBasedOnExpiryDay()
    {
        return DateTime.Compare(CreateDate, DateTime.Now) <= ExpiryDays;
    }

    public void ExpireShortLink()
    {
        if (!IsExpired)
        {
            ExpireDate = DateTime.Now;
            IsExpired = true;
            AddEvent(new ShortLinkExpiredEvent(UniqueCode, User.UserName, User.Email.Value));
        }
    }
    public void CheckAndExpireShortLink()
    {
        if (DateTime.Compare(CreateDate, DateTime.Now) <= ExpiryDays)
        {
            ExpireShortLink();
        }
    }
}