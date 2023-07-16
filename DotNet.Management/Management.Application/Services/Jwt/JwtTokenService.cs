using Management.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Management.Application
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtTokenOptions _jwtTokenOptions;
        private readonly Random _rand;
        private readonly IAesProtector _aesProtector;
        private readonly IMemoryCache _memoryCache;
        public JwtTokenService(
            IOptions<JwtTokenOptions> jwtTokenOptions, 
            IAesProtector aesProtector, IMemoryCache memoryCache)
        {
            _jwtTokenOptions = jwtTokenOptions.Value;
            _aesProtector = aesProtector;
            _memoryCache = memoryCache;
            _rand = new Random(Environment.TickCount);
        }

        public JwtTokenDto CreateToken(long userId, IEnumerable<Claim> claims)
        {
            string accessToken = CreateAccessToken(claims);
            string refreshToken = CreateRefreshToken(userId);

            return new JwtTokenDto()
            {
                AccessToken = accessToken,
                AccessTokenExpiration = _jwtTokenOptions.AccessTokenExpiration,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = _jwtTokenOptions.RefreshTokenExpiration
            };
        }

        public long GetUserIdByRefreshToken(string refreshToken)
        {
            if (_memoryCache.TryGetValue(refreshToken, out _))
            {
                throw new BusinessException("The refreshToken is invalid");
            }
            long expireStamp = 0;
            long userId = 0;
            try
            {
                byte[] data = Convert.FromBase64String(refreshToken);
                data = _aesProtector.Decrypt(data);

                byte[] usefulData = new byte[16];
                Buffer.BlockCopy(data, 0, usefulData, 0, 16);
                byte[] calHash = HashSign.Sign("MD5", usefulData);

                BinaryReader reader = new BinaryReader(new MemoryStream(data));
                userId = reader.ReadInt32();
                expireStamp = reader.ReadInt64();
                int randNum = reader.ReadInt32();
                int hashLen = reader.ReadInt32();
                byte[] hash = reader.ReadBytes(hashLen);
                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != calHash[i])
                    {
                        throw new Exception();
                    }
                }
            }
            catch
            {
                throw new BusinessException("The refreshToken is invalid");
            }

            DateTimeOffset expireDateTime = DateTimeOffset.FromUnixTimeMilliseconds(expireStamp);
            if (expireDateTime < DateTimeOffset.UtcNow)
            {
                throw new BusinessException("The refreshToken has expired");
            }
            TimeSpan ts = DateTimeOffset.UtcNow - expireDateTime;

            _memoryCache.Set(refreshToken, "1", ts);
            return userId;
        }

        private string CreateAccessToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenOptions.SecurityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var utcNow = DateTime.UtcNow;

            var securityToken = new JwtSecurityToken(
                _jwtTokenOptions.Issuer,
                _jwtTokenOptions.Audience,
                claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(_jwtTokenOptions.AccessTokenExpiration),
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return accessToken;
        }

        private string CreateRefreshToken(long userId)
        {
            long expireStamp = DateTimeOffset.UtcNow.AddSeconds(_jwtTokenOptions.RefreshTokenExpiration).ToUnixTimeMilliseconds();
            MemoryStream memStream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memStream);
            writer.Write(userId);
            writer.Write(expireStamp);
            writer.Write(_rand.Next());
            byte[] usefulData = memStream.ToArray();
            byte[] hash = HashSign.Sign("MD5", usefulData);
            writer.Write(hash.Length);
            writer.Write(hash);
            writer.Flush();
            byte[] keyData = _aesProtector.Encrypt(memStream.ToArray());
            string token = Convert.ToBase64String(keyData);
            writer.Close();
            return token;
        }
    }
}
