using AutoMapper;
using Management.Domain;
using System.Data;

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
        public async Task<RoleDto> CreateAsync(CreateRoleDto input)
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
            var dto = _mapper.Map<RoleDto>(role);
            dto.PermissionCodes = role!.RolePermissions.Select(rp => rp.PermissionCode).ToList();
            return dto;
        }

        public async Task<PageResultDto<RoleDto>> GetListAsync(GetRolesDto inputDto)
        {
            var input = _mapper.Map<GetRolesInput>(inputDto);
            input.IncludeRolePermission = true;

            int count = await _roleRepository.CountAsync(input);
            if (count == 0)
            {
                return new PageResultDto<RoleDto>();
            }

            var roles = await _roleRepository.GetListAsync(input);

            List<RoleDto> roleDtos = new();
            foreach (var item in roles)
            {
                var dto = _mapper.Map<RoleDto>(item);
                dto.PermissionCodes = item.RolePermissions.Select(rp => rp.PermissionCode).ToList();
            }

            return new PageResultDto<RoleDto>()
            {
                TotalCount = count,
                Items = roleDtos
            };
        }

        public async Task UpdateAsync(long id, CreateRoleDto input)
        {
            var role = await _roleRepository.GetAsync(id);
            var roles = await _roleRepository.GetListAsync(new GetRolesInput { Code = input.Code,MaxResultCount = 2 });
            if (roles.Any(r => r.Id != id))
            {
                throw new BusinessException("角色编码已存在!");
            }

            await _roleRepository.UpdateAsync(role!,true);
        }

        public async Task UpdateRolePermissionAsync(long id, List<string> permissionCodes)
        {
            Role? role = await _roleRepository.GetAsync(id, new GetRoleDetailsInput { IncludeRolePermission = true });
            ValidateNotNull(role);
            role!.RolePermissions = permissionCodes.Select(p => new RolePermission(id, p)).ToList();
            await _roleRepository.UpdateAsync(role, true);
        }

        public async Task<List<PermissionDto>> GetPermissionsAsync()
        {
            var permissions = await _roleRepository.GetPermissionsAsync();
            List<PermissionDto> dtos = new();
            foreach (var item in permissions.Where(p => p.ParentCode == null))
            {
                var dto = _mapper.Map<PermissionDto>(item);
                dto.Childrens = _mapper.Map<List<PermissionDto>>(permissions.Where(p => p.ParentCode == item.Code).ToList());
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
