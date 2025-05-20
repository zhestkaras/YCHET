using Telegram.Bot;
using System;
using System.Threading.Tasks;
using System.Windows;

public class TelegramNotifier
{
    private readonly string _botToken;
    private readonly long _chatId;
    private readonly TelegramBotClient _botClient;

    public TelegramNotifier(string botToken, long chatId)
    {
        _botToken = botToken;
        _chatId = chatId;
        _botClient = new TelegramBotClient(_botToken);
    }

    public async Task SendNotificationAsync(string message)
    {
        try
        {
            await _botClient.SendMessage(_chatId, message);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка отправки: {ex.Message}");
        }
    }
}