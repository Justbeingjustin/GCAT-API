using AutoMapper;

namespace GCAT.API
{
    public class CryptoProfile : Profile
    {
        public CryptoProfile()
        {
            CreateMap<Models.ReportJobForCreation, Entities.ReportJob>();
        }
    }
}