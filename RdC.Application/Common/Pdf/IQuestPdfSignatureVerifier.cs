namespace RdC.Application.Common.Pdf
{
    public interface IQuestPdfSignatureVerifier
    {
        Task<bool> HasValidSignature(byte[] planFile);
    }
}
