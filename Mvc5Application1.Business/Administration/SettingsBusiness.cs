using Mvc5Application1.Business.Contracts.Administration;
using Mvc5Application1.Data.Model;
using System.Linq;

namespace Mvc5Application1.Business.Administration
{
    public class SettingsBusiness : ISettingsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Settings> _setttingsRepository;

        public SettingsBusiness(IUnitOfWork unitOfWork, IRepository<Settings> setttingsRepository)
        {
            _unitOfWork = unitOfWork;
            _setttingsRepository = setttingsRepository;
        }

        public Settings GetSettings()
        {
            return _setttingsRepository.GetAll().First();
        }

        public void Update(Settings settings)
        {
            _setttingsRepository.Update(settings);
            _unitOfWork.SaveChanges();
        }
    }
}