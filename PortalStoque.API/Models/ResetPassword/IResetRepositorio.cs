namespace PortalStoque.API.Models.ResetPassword
{
    interface IResetRepositorio
    {
        UserReset ValidaLogin(string login);
        bool LogResetMail(int IdUsuario, int codigo);
        bool ValidaCodigo(int codigo, int idUsuario);
        bool UpdatePassword(int idUsuario, int codigo, string password);
        bool ClearCodigo(int codigo, int idUsuario);
    }
}