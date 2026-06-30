using AutoMapper;
using TODO.Interfaces.Auth.DTO;
using TODO.Interfaces.User.Entities;

namespace TODO.BLL.Auth;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<SignUpDTO, UserEntity>();
    }
}