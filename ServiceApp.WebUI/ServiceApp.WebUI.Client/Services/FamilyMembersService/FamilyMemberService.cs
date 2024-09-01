using ServiceApp.Application.Users;
using System.Net.Http.Json;

namespace ServiceApp.WebUI.Client.Services.FamilyMembersService;

public class FamilyMemberService : IFamilyMemberService
{
    private readonly HttpClient _http;

    public FamilyMemberService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<UsersDto>> GetAllUsersByFamilyId()
    {
        var response = await _http.GetFromJsonAsync<List<UsersDto>>("api/familymember");
        if (response == null)
        {
            return new List<UsersDto>();
        }
        return response;
    }

    public async Task<List<UsersDto>> GetFamilyMembersByRoleAsync()
    {
        var response = await _http.GetFromJsonAsync<List<UsersDto>>("api/familymember/role");
        if (response == null)
        {
            return new List<UsersDto>();
        }
        return response;
    }
}
