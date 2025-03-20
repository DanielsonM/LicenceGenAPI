using LicenceGenAPI.Data.Converter.Implementation;
using LicenceGenAPI.Data.VO;
using LicenceGenAPI.DbConnection;
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

        public LicenceVO Create(LicenceVO objModel)
        {
            var objEntity = _converter.Parse(objModel);
            objEntity = _repository.Create(objEntity);

            return _converter.Parse(objEntity);
        }

        public void Delete(int intId)
        {
            _repository.Delete(intId);
        }

        public List<LicenceVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public LicenceVO FindById(int intId)
        {
            var objEntity = _converter.Parse(_repository.FindById(intId));
           return _converter.Parse(_converter.Parse(objEntity));
        }

        public LicenceVO FindByLicenceKey(string strLicenceKey)
        {
            var objEntity = _converter.Parse(_repository.FindByLicenceKey(strLicenceKey));
            return _converter.Parse(_converter.Parse(objEntity));
        }

        public LicenceVO Update(LicenceVO objModel)
        {
            var objEntity = _converter.Parse(objModel);
            objEntity = _repository.Update(objEntity);

            return _converter.Parse(objEntity);
        }
    }
}
