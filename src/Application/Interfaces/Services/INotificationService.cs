﻿namespace Application.Interfaces.Services;

public interface INotificationService
{
    public void SendNotification(string address, string message);
}