using AutoMapper;
using TODO.Interfaces.User.DTO;
using TODO.Interfaces.User.Entities;

namespace TODO.BLL.User;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, GetUserDTO>();
    }
}