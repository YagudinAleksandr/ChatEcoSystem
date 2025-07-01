using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ChatEcoSystem.SharedLib.Abstractions;
using ChatEcoSystem.SharedLib.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChatEcoSystem.Auth.Logic
{
    internal class AuthService : IAuthService
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IRepository<User, Guid> _repository;
        private readonly IMapper _mapper;

        public AuthService(IOptions<JwtConfiguration> options, IRepository<User, Guid> repository, IMapper mapper)
        {
            _jwtConfiguration = options.Value;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseApiResponse<UserAuthResponseDto>> Authenticate(UserAuthRequestDto userAuth)
        {
            var response = new BaseApiResponse<UserAuthResponseDto>();

            var users = await _repository.GetAll();
            var user = users.FirstOrDefault(x => x.Email == userAuth.Email && x.Password == userAuth.Password);

            if (user == null)
            {
                response.Errors.Add("Пользователь не найден");
                response.StatusCode = 404;
                return response;
            }

            response.StatusCode = 200;
            response.Body = new UserAuthResponseDto
            {
                Token = GenerateJwtToken(userAuth.Email),
                User = _mapper.Map<UserDto>(user)
            };

            return response;
        }

        /// <summary>
        /// Генерация токена
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <returns>Токен</returns>
        private string GenerateJwtToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, email)
        };

            var token = new JwtSecurityToken(
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
