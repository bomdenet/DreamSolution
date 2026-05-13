using System;

namespace Dream.Wpf.Notifications;

public sealed record NotificationSystemError(Exception Error) : NotificationBase
{
    public static string DefaultTitle { get; set; } = "Произошла неизвестная ошибка";

    public static NotificationSystemError Create(Exception Error, string message) => Create(Error, DefaultTitle, message);
    public static NotificationSystemError Create(Exception Error, string title, string message) => new(Error)
    {
        Title = title,
        Message = message,
        Duration = null,
        IsClosable = true
    };
}
