using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RdC.Application.Common.Pdf;
using RdC.Domain.Acheteurs;
using RdC.Domain.PaiementDates;
using RdC.Domain.PlanDePaiements;

namespace RdC.Infrastructure.Pdf
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
        private const string SignatureMarker = "SIGNATURE_FIELD_7X9P3";

        public byte[] GeneratePlanDePaiementPdf(
            PlanDePaiement plan,
            List<PaiementDate> paiementDates,
            Acheteur acheteur)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // Header
                    page.Header().AlignCenter().Text("Plan de Paiement").FontSize(20).Bold();

                    // SINGLE Content section
                    page.Content().Column(col =>
                    {
                        col.Spacing(20);

                        col.Item().PaddingTop(10).Text($"Acheteur : {acheteur.Nom} {acheteur.Prenom}").Bold();

                        col.Item().PaddingTop(25).Row(row =>
                        {
                            row.RelativeItem().Text($"Montant Total : {plan.MontantTotal} TND").Bold();
                            row.RelativeItem().Text($"Nombre d'échéances : {plan.NombreDeEcheances}").Bold();
                        });

                        // Factures Table
                        if (plan.Factures.Any())
                        {
                            col.Item().Text("Factures Concernées :").Bold().FontSize(14);
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(2);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("Référence").Bold();
                                    header.Cell().Element(CellStyle).Text("Montant Due (TND)").Bold();
                                    header.Cell().Element(CellStyle).Text("Date d'Écheance").Bold();
                                });

                                foreach (var facture in plan.Factures)
                                {
                                    table.Cell().Element(CellStyle).Text(facture.NumFacture);
                                    table.Cell().Element(CellStyle).Text($"{facture.MontantRestantDue} TND");
                                    table.Cell().Element(CellStyle).Text(facture.DateEcheance.ToString("dd/MM/yyyy"));
                                }
                            });
                        }

                        // Échéances Table
                        col.Item().Text("Échéances :").Bold().FontSize(14);
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Date d'Échéance").Bold();
                                header.Cell().Element(CellStyle).Text("Montant (TND)").Bold();
                            });

                            foreach (var echeance in paiementDates)
                            {
                                table.Cell().Element(CellStyle).Text(echeance.EcheanceDate.ToString("dd/MM/yyyy"));
                                table.Cell().Element(CellStyle).Text($"{echeance.MontantDeEcheance} TND");
                            }
                        });

                        // SIGNATURE BOX IN CONTENT
                        col.Item().PaddingTop(40).Row(row =>
                        {
                            row.RelativeItem(); // Spacer
                            row.ConstantItem(250).Column(sigCol =>
                            {
                                sigCol.Item().Text("Signature de l'Acheteur :").FontSize(12).Bold();

                                // Signature box container
                                sigCol.Item().Element(signatureContainer =>
                                {
                                    signatureContainer
                                        .Height(50)
                                        .Width(250)
                                        .Border(1)
                                        .Padding(5)
                                        .AlignCenter()
                                        .AlignMiddle()
                                        .Background(Colors.White)
                                        // Hidden marker (invisible to users)
                                        .Text(SignatureMarker)
                                        .FontColor(Colors.Transparent)
                                        .FontSize(1);
                                });

                                sigCol.Item().PaddingTop(5).AlignRight().Text("Date : ___________________");
                            });
                        });
                    });

                    IContainer CellStyle(IContainer container) =>
                        container.Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                });
            }).GeneratePdf();
        }
    }
}
