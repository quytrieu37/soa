using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data.DTOs;

namespace DatingApp.API.services
{
    public interface IMemberService
    {
        List<MemberDto> GetMembers();
        MemberDto GetMemberByUserName(string UserName);
    }
}