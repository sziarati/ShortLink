
using Domain.Bases;
using Domain.Events;
using System;
using System.Text;

namespace Domain.Entities.UserAggregate;

public class ShortLink : BaseEntity
{
    private static readonly int Length = 10;
    private static readonly string Domain = "short.link";
    private static readonly int ExpiryDays = 2;

    public DateTime ExpireDate { get; private set; }

    public string Name { get; private set; }
    public string OriginUrl { get; private set; }
    public string UniqueCode { get; private set; }

    public decimal UserId { get; private set; }
    public User User { get; private set; }
    public bool _IsExpired { get; private set; }

    public ShortLink(decimal userId, string originUrl, string uniqueCode, DateTime expireDate)
    {
        UserId = userId;
        OriginUrl = originUrl;
        UniqueCode = uniqueCode;
        ExpireDate = expireDate;
        _IsExpired = false;
    }

    public void UpdateUrl(string newUniqueCode)
    {
        UniqueCode = newUniqueCode;
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

        ShortLink = $"http://{Domain}/{ShortLink}";

        return ShortLink;
    }

    public bool IsExpired()
    {
        return _IsExpired || DateTime.Compare(ExpireDate, DateTime.Now) <= ExpiryDays;
    }

    public ShortLinkExpiring Expire()
    {
        if (IsExpired())
        {
            _IsExpired = true;
            return new ShortLinkExpiring { ShortLink = this };
        }

        return null;
    }
}