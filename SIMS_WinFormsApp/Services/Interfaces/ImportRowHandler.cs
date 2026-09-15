using SIMS_WinFormsApp.Models.DTOs;

namespace SIMS_WinFormsApp.Services.Interfaces
{
    public delegate ImportRowResult ImportRowHandler(string[] cells, int rowNumber);
}