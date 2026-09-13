using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.MVP.ViewModels;

namespace SIMS_WinFormsApp.Services.Interfaces
{
    public interface IUserAccountDetailViewModelBuilder
    {
        UserAccountDetailViewModel Build(UserDetailDto user);
    }
}