using System;

namespace Dream.Wpf.Notifications;

public enum NotificationType
{
    None = 0,
    Notification = 1,
    Warning = 2,
    Error = 3
}

public sealed record Notification
{
    public NotificationType Type { get; init; } = NotificationType.None;
    public string? Title { get; init; }
    public string? Message { get; init; }
    public TimeSpan? Duration { get; init; } = TimeSpan.FromSeconds(5);
    public bool IsClosable { get; init; } = true;

    public static Notification CreateMessage(string message) => new()
    {
        Type = NotificationType.Notification,
        Title = "Уведомление",
        Message = message
    };
    public static Notification CreateMessage(string message, TimeSpan duration) => new()
    {
        Type = NotificationType.Notification,
        Title = "Уведомление",
        Message = message,
        Duration = duration
    };

    public static Notification CreateWarning(string message) => new()
    {
        Type = NotificationType.Warning,
        Title = "Предупреждение",
        Message = message
    };
    public static Notification CreateWarning(string message, TimeSpan duration) => new()
    {
        Type = NotificationType.Warning,
        Title = "Предупреждение",
        Message = message,
        Duration = duration
    };

    public static Notification CreateError(string message) => new()
    {
        Type = NotificationType.Error,
        Title = "Ошибка",
        Message = message
    };
    public static Notification CreateError(string message, TimeSpan duration) => new()
    {
        Type = NotificationType.Error,
        Title = "Ошибка",
        Message = message,
        Duration = duration
    };
}
