using RdC.Application.Common.Pdf;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Geometry;
using UglyToad.PdfPig.Graphics;

namespace RdC.Infrastructure.Pdf
{
    public class QuestPdfSignatureVerifier : IQuestPdfSignatureVerifier
    {
        private const string SignatureMarker = "SIGNATURE_FIELD_7X9P3";
        private const double BoxWidth = 250;
        private const double BoxHeight = 50;
        private const double BoxPadding = 5;

        public async Task<bool> HasValidSignature(byte[] planFile)
        {
            using var document = PdfDocument.Open(planFile);
            var page = document.GetPage(1);

            // 1. Finding the signature
            var marker = page.GetWords()
            .FirstOrDefault(w => w.Text == SignatureMarker);

            if (marker == null) return false;

            // 2. Define signature box area
            var signatureBox = new PdfRectangle(
                marker.BoundingBox.Centroid.X - (BoxWidth / 2) + BoxPadding,
                marker.BoundingBox.Centroid.Y - (BoxHeight / 2) + BoxPadding,
                marker.BoundingBox.Centroid.X + (BoxWidth / 2) - BoxPadding,
                marker.BoundingBox.Centroid.Y + (BoxHeight / 2) - BoxPadding
            );

            // 3. Check for any marks in the signature area
            return page.GetAnnotations()
            .Any(a => signatureBox.Contains(a.Rectangle))
            || page.GetImages()
            .Any(i => signatureBox.IntersectsWith(i.Bounds));
        }
    }
}
