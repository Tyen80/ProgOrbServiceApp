﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApp.Application.Users.GetAllFamilyUsers;
using ServiceApp.Application.Users.GetFamilyUsersByRole;
using ServiceApp.Domain.Abstractions;
using ServiceApp.Infrastructure.Users;

namespace ServiceApp.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FamilyMemberController : ControllerBase
{
    private readonly ISender _sender;

    public FamilyMemberController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<User>>>> GetFamilyMembersAsync()
    {
        var result = await _sender.Send(new GetAllFamilyUsersQuery());
        return Ok(result.Value);
    }

    [HttpGet("{role}")]
    public async Task<ActionResult<Result<List<User>>>> GetFamilyMembersByRoleAsync()
    {
        var result = await _sender.Send(new GetFamilyUserByRoleQuery());
        return Ok(result.Value);
    }
}
