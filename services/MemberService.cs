using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.API.Data;
using DatingApp.API.Data.DTOs;
using DatingApp.API.Data.Entities;

namespace DatingApp.API.services
{
    public class MemberService : IMemberService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MemberService(DataContext context, IMapper mapper){
            _mapper = mapper;
            _context = context;
        }
        public MemberDto GetMemberByUserName(string UserName)
        {
            var user = _context.Users.FirstOrDefault(x=>x.Username == UserName);
            if(user == null) return null;
            return _mapper.Map<User, MemberDto>(user);
        }

        public List<MemberDto> GetMembers()
        {
            // return _context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToList();
            return _context.Users.Select(user => _mapper.Map<User, MemberDto>(user)).ToList();
        }
    }
}