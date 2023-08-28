using AutoMapper;
using Management.Domain;

namespace Management.Application
{
    public class AppAutoMapperProfile: Profile
    {
        public AppAutoMapperProfile()
        {
            #region Identity

            CreateMap<GetUsersDto, GetUsersInput>();
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            CreateMap<GetRolesDto, GetRolesInput>();
            CreateMap<Role, RoleDto>();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();


            CreateMap<Permission, PermissionDto>();

            #endregion
        }
    }
}
