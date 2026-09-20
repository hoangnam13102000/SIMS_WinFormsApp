using System;
using SIMS_WinFormsApp.MVP.ViewModels;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    /// <summary>Hợp đồng View cho thanh Header của shell chính.</summary>
    public interface IHeaderView
    {
        void SetSubtitle(string subtitle);
        void SetUser(HeaderUserViewModel user);
        void SetUnreadCount(int count);

        event EventHandler ProfileClicked;
        event EventHandler LogoutClicked;
    }
}