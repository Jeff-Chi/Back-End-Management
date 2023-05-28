using AutoMapper;
using Management.Domain;

namespace Management.Application
{
    public class RoleAppService : ApplicationService, IRoleAppService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleAppService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<RoleDto> CreateAsync(CreateRoleInputDto input)
        {
            int count = await _roleRepository.CountAsync(new GetRolesInput { Code = input.Code });
            if (count > 0)
            {
                throw new BusinessException("角色编码已存在!");
            }

            Role role = _mapper.Map(input, new Role(GenerateId()));

            await _roleRepository.InsertAsync(role, true);

            return _mapper.Map<RoleDto>(role);

        }

        public async Task DeleteAsync(long id)
        {
            var role = await _roleRepository.GetAsync(id);
            ValidateNotNull(role);
            await _roleRepository.DeleteAsync(role!);
        }

        public async Task<RoleDto> GetAsync(long id)
        {
            var role = await _roleRepository.GetAsync(id);
            ValidateNotNull(role);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<PageResultDto<RoleDto>> GetListAsync(GetRolesInputDto inputDto)
        {
            var input = _mapper.Map<GetRolesInput>(inputDto);
            int count = await _roleRepository.CountAsync(input);
            if (count == 0)
            {
                return new PageResultDto<RoleDto>();
            }

            var roles = await _roleRepository.GetListAsync(input);

            var roleDtos = _mapper.Map<List<RoleDto>>(roles);

            return new PageResultDto<RoleDto>()
            {
                TotalCount = count,
                Items = roleDtos
            };
        }

        public async Task UpdateAsync(long id, CreateRoleInputDto input)
        {
            var role = await _roleRepository.GetAsync(id);
            var roles = await _roleRepository.GetListAsync(new GetRolesInput { Code = input.Code,MaxResultCount = 2 });
            if (roles.Any(r => r.Id != id))
            {
                throw new BusinessException("角色编码已存在!");
            }

            await _roleRepository.UpdateAsync(role!,true);
        }
    }
}
