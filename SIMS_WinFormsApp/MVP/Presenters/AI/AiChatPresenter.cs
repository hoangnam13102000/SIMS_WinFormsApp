using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SIMS_WinFormsApp.Services.AI;
using SIMS_WinFormsApp.Services.AI.Interfaces;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters.AI
{
    public sealed class AiChatPresenter : IDisposable
    {
        private readonly IAiChatView _view;
        private readonly IAiChatService _chatService;
        private bool _disposed;

        public AiChatPresenter(IAiChatView view, IAiChatService chatService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
            _view.SendRequested += OnSendRequested;
        }

        private async void OnSendRequested(string message)
        {
            if (_disposed || string.IsNullOrWhiteSpace(message)) return;

            _view.AppendUserMessage(message);
            _view.SetBusy(true);
            try
            {
                string response = await _chatService.SendAsync(message);
                _view.AppendBotMessage(response);
            }
            catch (AiChatException ex)
            {
                _view.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[AI] Chat request failed: " + ex.GetType().Name);
                _view.ShowError(Lang.Get("ai.error.unexpected"));
            }
            finally
            {
                if (!_disposed) _view.SetBusy(false);
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _view.SendRequested -= OnSendRequested;
        }
    }
}
