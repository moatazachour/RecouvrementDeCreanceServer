namespace RdC.Api.Controllers.Litiges
{
    public record UploadLitigeJustificatifRequest(
        int LitigeID,
        List<IFormFile> Files);
}
