using LicenceGenAPI.Data.VO;
using LicenceGenAPI.Models;

namespace LicenceGenAPI.Rules.Services
{
    public interface ILicenceRuleService
    {
        public LicenceVO Create(LicenceVO obj);

        public LicenceVO Update(LicenceVO obj);

        public void Delete(int intId);

        public List<LicenceVO> FindAll();

        public LicenceVO FindById(int intId);

        public LicenceVO FindByLicenceKey(string strLicenceKey);
    }
}
