using LicenceGenAPI.Models;

namespace LicenceGenAPI.Repository.Service
{
    public interface ILicenceRepositoryService
    {
        public LicenceModel Create(LicenceModel obj);

        public LicenceModel Update(LicenceModel obj);

        public void Delete(int intId);

        public List<LicenceModel> FindAll();

        public LicenceModel FindById(int intId);

        public LicenceModel FindByLicenceKey(string strLicenceKey);
    }
}