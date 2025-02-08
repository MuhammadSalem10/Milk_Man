
namespace MilkMan.Application.Interfaces;


    public enum RegisterResult
    {
        Success,
        EmailInUse,
        InvalidModel
    }

    public enum LoginResult
    {
        Success,
        InvalidCredentials,
        LockedOut
    }

    public enum AddToRoleResult
    {
        Success,
        IdentityNotFound,
        AlreadyExists
    }

 