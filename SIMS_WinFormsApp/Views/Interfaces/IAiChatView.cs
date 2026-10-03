using System;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IAiChatView
    {
        event Action<string> SendRequested;
        event Action CloseRequested;

        void AppendUserMessage(string message);
        void AppendBotMessage(string message);
        void SetBusy(bool busy);
        void ShowError(string message);
    }
}
