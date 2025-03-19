using LicenceGenAPI.Controllers;
using LicenceGenAPI.Data.Converter.Implementation;
using LicenceGenAPI.Data.VO;
using LicenceGenAPI.DbConnection;
using LicenceGenAPI.Models;
using LicenceGenAPI.Repository.Service;
using LicenceGenAPI.Rules.Services;

namespace LicenceGenAPI.Rules.Implementation
{
    public class LicenceRule : ILicenceRuleService
    {
        private readonly ILicenceRepositoryService _repository;
        private readonly LicenceConverter _converter;

        public LicenceRule(PostgreDbContext context, ILicenceRepositoryService repository)
        {
            _repository = repository;
            _converter = new LicenceConverter();
        }

        public LicenceModel Create(LicenceModel objModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(int intId)
        {
            _repository.Delete(intId);
        }

        public List<LicenceModel> FindAll()
        {
            return _repository.FindAll();
        }

        public LicenceModel FindById(int intId)
        {
           return _repository.FindById(intId);
        }

        public LicenceModel FindByLicenceKey(string strLicenceKey)
        {
            return _repository.FindByLicenceKey(strLicenceKey);
        }

        public LicenceModel Update(LicenceModel objModel)
        {
            return _repository.Update(objModel);
        }
    }
}
