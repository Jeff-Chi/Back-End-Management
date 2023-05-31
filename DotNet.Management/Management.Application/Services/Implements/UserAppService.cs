using AutoMapper;
using Management.Domain;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Management.Application
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;
        public UserAppService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IMapper mapper,
            IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<PageResultDto<UserDto>> GetListAsync(GetUsersInputDto inputDto)
        {
            var input = _mapper.Map<GetUsersInput>(inputDto);
            int count = await _userRepository.CountAsync(input);
            if (count == 0)
            {
                return new PageResultDto<UserDto>();
            }

            List<User> users = await _userRepository.GetListAsync(input);

            List<UserDto> userDtos = _mapper.Map<List<UserDto>>(users);

            return new PageResultDto<UserDto>()
            {
                TotalCount = count,
                Items = userDtos
            };
        }

        public async Task<UserDto> GetAsync(long id)
        {
            var user = await _userRepository.GetAsync(id);

            ValidateNotNull(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(CreateUserInputDto input)
        {
            User user = _mapper.Map(input, new User(GenerateId()));

            await _userRepository.InsertAsync(user, true);

            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateAsync(long id, UpdateUserInputDto input)
        {
            var user = await _userRepository.GetAsync(id);

            ValidateNotNull(user);
            _mapper.Map(input, user);

            await _userRepository.UpdateAsync(user!, true);
        }

        public async Task DeleteAsync(long id)
        {
            var user = await _userRepository.GetAsync(id);

            ValidateNotNull(user);

            await _userRepository.DeleteAsync(user!, true);
        }

        public async Task<JwtTokenDto> LoginAsync(UserLoginDto userLoginDto)
        {
            User? user = await _userRepository.GetAsync(userLoginDto.UserName, userLoginDto.Password);
            if (user == null)
            {
                throw new BusinessException("用户名或密码错误!");
            }

            user.LastLoginTime = DateTime.Now;
            await _userRepository.UpdateAsync(user, true);
            return CreateTokenDto(user);
        }

        public async Task<List<RoleDto>> GetRolesAsync(long userId)
        {
            User? user = await _userRepository.GetAsync(userId);

            ValidateNotNull(user);
            var roles = _roleRepository.GetListAsync(userId);

            return _mapper.Map<List<RoleDto>>(roles);
        }

        public async Task UpdateUserRolesAsync(long userId, List<long> roleIds)
        {
            User? user = await _userRepository.GetAsync(userId, new GetUserDetailsInput { IncludeUserRoles = true });

            ValidateNotNull(user);

            user!.UserRoles = roleIds.Select(r => new UserRole(userId, r)).ToList();
            await _userRepository.UpdateAsync(user, true);
        }

        public async Task<CurrentUserDto> GetCurrentUserAsync(CurrentUserContext currentUserContext)
        {
            User? user = await _userRepository.GetAsync(currentUserContext.Id!.Value,
                new GetUserDetailsInput { IncludeUserRoles = true });

            ValidateNotNull(user);

            CurrentUserDto currentUserDto = new()
            {
                UserName = user!.UserName,
                NickName = user.NickName
            };

            List<Permission> permissions = await _roleRepository.GetPermissionsAsync(user.UserRoles.Select(ur => ur.RoleId).ToList());

            currentUserDto.Permissions = _mapper.Map<List<PermissionDto>>(permissions);

            return currentUserDto;
        }

        #region private methods

        private JwtTokenDto CreateTokenDto(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString(),ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString(), ClaimValueTypes.String)
            };

            return _jwtTokenService.CreateToken(user.Id, claims);
        }

        #endregion

    }
}
