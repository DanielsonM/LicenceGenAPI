using LicenceGenAPI.Models;

namespace LicenceGenAPI.Rules.Services
{
    public interface ILicenceRuleService
    {
        public LicenceModel Create(LicenceModel obj);

        public LicenceModel Update(LicenceModel obj);

        public void Delete(int intId);

        public List<LicenceModel> FindAll();

        public LicenceModel FindById(int intId);

        public LicenceModel FindByLicenceKey(string strLicenceKey);
    }
}
