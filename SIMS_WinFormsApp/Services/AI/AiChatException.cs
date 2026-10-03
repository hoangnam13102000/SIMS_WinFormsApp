using System;

namespace SIMS_WinFormsApp.Services.AI
{
    public sealed class AiChatException : Exception
    {
        public AiChatException(string message) : base(message)
        {
        }

        public AiChatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
