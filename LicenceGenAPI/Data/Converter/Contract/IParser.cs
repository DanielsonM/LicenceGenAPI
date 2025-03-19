namespace LicenceGenAPI.Data.Converter.Contract
{
    public interface IParser<O, D>
    {
        D Parse(O objOrigin);
        List<D> Parse(List<O> lstOrigin);
    }
}
