using AutoMapper;
using Management.Domain;

namespace Management.Application
{
    public class AppAutoMapperProfile: Profile
    {
        public AppAutoMapperProfile()
        {
            #region Identity

            CreateMap<User, UserDto>();
            CreateMap<CreateUserInputDto, User>();

            CreateMap<Role, RoleDto>();
            CreateMap<CreateRoleInputDto, Role>();

            CreateMap<Permission, PermissionDto>();

            #endregion
        }
    }
}
