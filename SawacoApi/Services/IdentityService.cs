using SawacoApi.Domain.Repositories;
using SawacoApi.Domain.Services;
using SawacoApi.Resources.Identity;

namespace SawacoApi.Services
{
    public class IdentityService : IIdentityService
    {
        public IIdentityRepository _identityRepository {  get; set; }

        public IdentityService(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        
    }
}
