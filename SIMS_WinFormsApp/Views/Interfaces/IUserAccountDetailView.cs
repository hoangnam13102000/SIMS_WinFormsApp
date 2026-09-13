using System;
using SIMS_WinFormsApp.MVP.ViewModels;

namespace SIMS_WinFormsApp.Views.Interfaces
{

    public interface IUserAccountDetailView
    {
        event EventHandler CloseRequested;
       
        void Render(UserAccountDetailViewModel viewModel);

        void CloseView();
    }
}