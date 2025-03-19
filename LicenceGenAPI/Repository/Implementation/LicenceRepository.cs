using LicenceGenAPI.Data.Converter.Implementation;
using LicenceGenAPI.Data.VO;
using LicenceGenAPI.DbConnection;
using LicenceGenAPI.Models;
using LicenceGenAPI.Repository.Service;

namespace LicenceGenAPI.Repository.Implementation
{
    public class LicenceRepository : ILicenceRepositoryService
    {
        private PostgreDbContext _context;
   
        public LicenceRepository(PostgreDbContext context)
        {
           _context = context;
        }

        public LicenceModel Create(LicenceModel objModel)
        {
            if (!Exists(objModel.intId))
            {
                try
                {
                    _context.Add(objModel);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return objModel;
        }

        public void Delete(int intId)
        {
            var objFound = _context.Licence.SingleOrDefault(item => item.intId.Equals(intId));

            if (objFound != null)
            {
                _context.Licence.Remove(objFound);
                _context.SaveChanges();
            }
        }
           

        public List<LicenceModel> FindAll()
        {
            return _context.Licence.ToList();
        }

        public LicenceModel FindById(int intId)
        {
            var objFound = _context.Licence.SingleOrDefault(item => item.intId.Equals(intId));
            if(objFound != null)
            {
                return objFound;
            }

            throw new Exception($"");
        }

        public LicenceModel FindByLicenceKey(string strLicenceKey)
        {
           var objFound = _context.Licence.SingleOrDefault(item => item.strLicenceKey.Equals(strLicenceKey));   

            if (objFound != null)
            {
                return objFound;
            }

            throw new Exception("");
        }

        public LicenceModel Update(LicenceModel objModel)
        {

            if(objModel == null)
                throw new NotImplementedException("");

            var objFound = _context.Licence.SingleOrDefault(item => item.intId.Equals(objModel.intId));

            if (objFound == null)
                throw new NotImplementedException("");
            
                try
                {
                    _context.Entry(objFound).CurrentValues.SetValues(objModel);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }

                return objModel;
        }

        private bool Exists(int intId)
        {
            return _context.Licence.Any(item => item.Equals(item.intId));
        }
    }
}
