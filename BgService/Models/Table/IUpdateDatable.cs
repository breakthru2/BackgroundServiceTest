using System.Threading.Tasks;

namespace BgService.Models.Table
{
    public interface IUpdateDatable
    {
        Task RunUpdate();

        Task RunCSVUpdate();
    }
}