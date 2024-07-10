using Domain.Bases;
using Domain.Entities.ValueObjects;
using Domain.Enums;

namespace Domain.Entities.UserAggregate;

public partial class User
{
    public User()
    {

    }
    public User(string userName, Email email, Password password, Address address)
    {
        CreateDate = DateTime.Now;
        UserName = userName;
        Email = email;
        Password = password;
        Address = address;
        State = UserStatus.Active; 
    }

    public void Update(string userName, Email email, Password password, Address address)
    {
        EditDate = DateTime.Now;
        UserName = userName;
        Email = email;
        Password = password;
        Address = address;
    }
    public void ResetPassword(Password password)
    {
        Password = password;
    }
    public void SetStatus(UserStatus userStatus)
    {
        if (State != userStatus)
            State = userStatus;
    }

}