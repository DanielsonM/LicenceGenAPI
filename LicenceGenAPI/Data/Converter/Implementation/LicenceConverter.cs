using LicenceGenAPI.Data.Converter.Contract;
using LicenceGenAPI.Data.VO;
using LicenceGenAPI.Models;

namespace LicenceGenAPI.Data.Converter.Implementation
{
    public class LicenceConverter : IParser<LicenceVO, LicenceModel>, IParser<LicenceModel, LicenceVO>
    {
        public LicenceModel Parse(LicenceVO objOrigin)
        {
            return new LicenceModel()
            {
                intId = objOrigin.intId,
                strUserName = objOrigin.strUserName,
                strStatus = objOrigin.strStatus,
                strLicenceKey = objOrigin.strLicenceKey,
                strDateRequest = objOrigin.strDateRequest,
                strDataExpiration = objOrigin.strDataExpiration,
                strObservation = objOrigin.strObservation
            };
        }

        public List<LicenceModel> Parse(List<LicenceVO> lstOrigin)
        {
            return lstOrigin.Select(item => Parse(item)).ToList();
        }

        public LicenceVO Parse(LicenceModel objOrigin)
        {
            return new LicenceVO()
            {
                intId = objOrigin.intId,
                strUserName = objOrigin.strUserName,
                strStatus = objOrigin.strStatus,
                strLicenceKey = objOrigin.strLicenceKey,
                strDateRequest = objOrigin.strDateRequest,
                strDataExpiration = objOrigin.strDataExpiration,
                strObservation = objOrigin.strObservation
            };
        }

        public List<LicenceVO> Parse(List<LicenceModel> lstOrigin)
        {
            return lstOrigin.Select(item => Parse(item)).ToList();
        }
    }
}
