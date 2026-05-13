using System;

namespace Dream.Wpf.Notifications;

public enum NotificationType
{
    Message,
    Warning,
    Error
}

public sealed record Notification : NotificationBase
{
    public static string DefaultMessageTitle { get; set; } = "Уведомление";
    public static string DefaultWarningTitle { get; set; } = "Внимание";
    public static string DefaultErrorTitle { get; set; } = "Ошибка";
    public static TimeSpan? DefaultDuration { get; set; } = TimeSpan.FromSeconds(4);

    public NotificationType NotificationType { get; init; }

    public static Notification CreateMessage(string message)                                                                => Create(NotificationType.Message, DefaultMessageTitle, message, DefaultDuration);
    public static Notification CreateMessage(string message, TimeSpan? duration)                                            => Create(NotificationType.Message, DefaultMessageTitle, message, duration);

    public static Notification CreateWarning(string message)                                                                => Create(NotificationType.Warning, DefaultWarningTitle, message, DefaultDuration);
    public static Notification CreateWarning(string message, TimeSpan? duration)                                            => Create(NotificationType.Warning, DefaultWarningTitle, message, duration);

    public static Notification CreateError(string message)                                                                  => Create(NotificationType.Error, DefaultErrorTitle, message, DefaultDuration);
    public static Notification CreateError(string message, TimeSpan? duration)                                              => Create(NotificationType.Error, DefaultErrorTitle, message, duration);

    public static Notification Create(NotificationType notificationType, string title, string message)                      => Create(notificationType, title, message, DefaultDuration);
    public static Notification Create(NotificationType notificationType, string title, string message, TimeSpan? duration)  => new()
    {
        NotificationType = notificationType,
        Title = title,
        Message = message,
        Duration = duration
    };
}
