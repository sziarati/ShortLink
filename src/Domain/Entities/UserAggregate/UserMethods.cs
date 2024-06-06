using Domain.Entities.ValueObjects;
using Domain.Enums;
using System.Security.Cryptography;

namespace Domain.Entities.UserAggregate;

public partial class User
{
    public User(string userName, string email, string password)
    {
        CreateDate = DateTime.Now;
        UserName = userName;
        Email = email;
        Password = password;
    }

    public void Update(string userName, string email, string password, Address address)
    {
        EditDate = DateTime.Now;
        UserName = userName;
        Email = email;
        Password = password;
        Address = address;
    }

    public void SetStatus(UserStatus userStatus)
    {
        if (State != (byte)userStatus)
            State = (byte)userStatus;
    }

    public void AddShortLink(string url, DateTime expireDate)
    {
        string uniqueCode = ShortLink.GenerateCode(url);
        var shortLink = new ShortLink(userId: Id, url, uniqueCode, expireDate);

        if (_ShortLinks is null)
            _ShortLinks = [];
        
        _ShortLinks.Add(shortLink);       
    }

    public void RemoveShortLink(decimal shortLinkId)
    {
        var shortLink = ShortLinks?.FirstOrDefault(sl => sl.Id == shortLinkId);
        if (shortLink != null)
        {
            _ShortLinks?.Remove(shortLink);
        }
    }

    public void UpdateShortLink(decimal shortLinkId, string newUrl)
    {
        var shortLink = ShortLinks?.FirstOrDefault(sl => sl.Id == shortLinkId);
        if (shortLink != null)
        {
            string uniqueCode = ShortLink.GenerateCode(newUrl);
            shortLink.UpdateUrl(uniqueCode);
        }
    }

    public void CheckAndExpireShortLinks()
    {
        var expiredLinks = ShortLinks?.Where(sl => sl.IsExpired());
        foreach (var shortLink in expiredLinks ?? Enumerable.Empty<ShortLink>())
        {
            ExpireShortLink(shortLink);
        }
    }
    public void ExpireShortLink(ShortLink shortLink)
    {
        if (shortLink != null)
        {
            var shortLinkExpiredEvent = shortLink.Expire();
            if (shortLinkExpiredEvent != null)
            {
                AddEvent(shortLinkExpiredEvent);
            }
        }
    }
}