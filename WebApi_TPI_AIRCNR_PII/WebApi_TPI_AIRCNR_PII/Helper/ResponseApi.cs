namespace WebApi_TPI_AIRCNR_PII.Helper
{
    public record ResponseApi(
        int code,
        string message,
        object? entity = null);
}
