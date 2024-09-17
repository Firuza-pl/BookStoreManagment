using AutoMapper;
using Library.Application.ViewModel.Members;
using Library.Domain.Entites.MemberAggregate;

namespace LibraryManagment.Mapper
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, GetMemberDTO>().ReverseMap();
        }
    }
}
