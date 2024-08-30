namespace ServiceApp.Application.Authentication;
public interface IFamilyExistService
{
    Task<bool> FamilyExists(string familyId);
}
