using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data.DTOs;
using DatingApp.API.services;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/members")]
    public class MemberController : BaseController
    {
        public IMemberService _memberService { get; }
        public MemberController(IMemberService memberService){
            _memberService = memberService;

        }
        [HttpGet]
        public ActionResult<List<MemberDto>> Get()
        {
            
            return Ok(_memberService.GetMembers());
        }

        [HttpGet("{username}")]
        public ActionResult<string> Get(string username)
        {
            var us = _memberService.GetMemberByUserName(username);
            if (us==null) {
                return NotFound(username);
            }
            return Ok(us);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}