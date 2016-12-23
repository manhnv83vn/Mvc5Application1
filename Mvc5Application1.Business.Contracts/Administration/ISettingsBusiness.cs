using Mvc5Application1.Data.Model;

namespace Mvc5Application1.Business.Contracts.Administration
{
    public interface ISettingsBusiness
    {
        Settings GetSettings();

        void Update(Settings settings);
    }
}