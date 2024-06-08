using System.Text;

namespace Domain.Entities.ShortLinkAggregate;

public partial class ShortLink
{
    public ShortLink(uint userId, string name, string OriginUrl)
    {
        UserId = userId;
        Name = name;
        CreateDate = DateTime.Now;
        ExpireDate = DateTime.Now.AddDays(ExpiryDays);
        IsExpired = false;

        string uniqueCode = GenerateCode(OriginUrl);
        UniqueCode = uniqueCode;
        
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
        return IsExpired || DateTime.Compare(ExpireDate, DateTime.Now) <= ExpiryDays;
    }

    //public void CheckAndExpireShortLinks()
    //{
    //    var expiredLinks = ShortLinks?.Where(sl => sl.IsExpired());
    //    foreach (var shortLink in expiredLinks ?? Enumerable.Empty<ShortLink>())
    //    {
    //        ExpireShortLink(shortLink);
    //    }
    //}

    public void ExpireShortLink(ShortLink shortLink)
    {
        if (shortLink != null)
        {

        }
    }

}
