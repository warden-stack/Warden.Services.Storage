using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Warden.Services.Storage.Repositories;
using Warden.Services.WardenChecks.Shared.Dto;

namespace Warden.Services.Storage.Services
{
    public class CheckResultService : ICheckResultService
    {
        private readonly ICheckResultRepository _wardenCheckResultRootRepository;

        public CheckResultService(ICheckResultRepository wardenCheckResultRootRepository)
        {
            _wardenCheckResultRootRepository = wardenCheckResultRootRepository;
        }

        public async Task ValidateAndAddAsync(string userId, Guid organizationId, Guid wardenId,
            object checkResult)
        {
            if (checkResult == null)
            {
                throw new ArgumentNullException(nameof(checkResult),
                    "Warden check result can not be null.");
            }

            var serializedResult = JsonConvert.SerializeObject(checkResult);
            var result = JsonConvert.DeserializeObject<WardenCheckResultDto>(serializedResult);
            var rootResult = new CheckResultDto
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                OrganizationId = organizationId,
                WardenId = wardenId,
                CreatedAt = DateTime.UtcNow,
                Result = result
            };
            await _wardenCheckResultRootRepository.AddAsync(rootResult);
        }
    }
}