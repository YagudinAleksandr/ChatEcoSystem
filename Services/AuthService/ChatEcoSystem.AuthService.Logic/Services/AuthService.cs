using System;
using System.Threading.Tasks;
using ChatEcoSystem.Abstractions;
using ChatEcoSystem.AuthService.Abstractions;
using ChatEcoSystem.Contracts;
using Microsoft.Extensions.Options;

namespace ChatEcoSystem.AuthService.Logic
{
    internal class AuthService : IAuthService
    {
        #region CTOR
        /// <inheritdoc cref="IRepository{TKey, TEntity}"/>
        private readonly IRepository<Guid, User> _repository;

        /// <inheritdoc cref="JwtConfiguration"/>
        private readonly JwtConfiguration _jwtConfiguration;

        public AuthService(IRepository<Guid, User> repository, IOptions<JwtConfiguration> jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration.Value;
            _repository = repository;
        }
        #endregion


        public Task<ApiResponse<AuthResponseDto>> Auth(AuthRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
