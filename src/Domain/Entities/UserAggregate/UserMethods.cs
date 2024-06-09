using Domain.Entities.ValueObjects;
using Domain.Enums;

namespace Domain.Entities.UserAggregate;

public partial class User
{
    public User(string userName, Email email, Password password)
    {
        CreateDate = DateTime.Now;
        UserName = userName;
        Email = email;
        Password = password;
    }

    public void Update(string userName, Email email, Password password, Address address)
    {
        EditDate = DateTime.Now;
        UserName = userName;
        Email = email;
        Password = password;
        Address = address;
    }

    public void SetStatus(UserStatus userStatus)
    {
        if (State != userStatus)
            State = userStatus;
    }

}