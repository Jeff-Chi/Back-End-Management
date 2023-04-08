using AutoMapper;
using Management.Domain;

namespace Management.Application
{
    public class UserAppService: ApplicationService,IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserAppService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PageResultDto<UserDto>> GetListAsync(QueryUsersDto input)
        {
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

        public async Task<UserDto?> GetAsync(long id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(CreateUserInputDto input)
        {
            User user = _mapper.Map(input, new User(GenerateId()));

            await _userRepository.InsertAsync(user,true);

            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateAsync(long id, CreateUserInputDto input)
        {
            var user = await _userRepository.GetAsync(id);
            if(user == null)
            {
                throw new Exception("未找到用户");
            }
            _mapper.Map(input, user);

            await _userRepository.UpdateAsync(user, true);
        }

        public async Task DeleteAsync(long id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user == null)
            {
                throw new Exception("未找到用户");
            }

            await _userRepository.DeleteAsync(user, true);
        }
    }
}
