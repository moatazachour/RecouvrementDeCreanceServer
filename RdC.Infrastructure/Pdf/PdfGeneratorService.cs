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

                    // Content
                    page.Content().Column(column =>
                    {
                        column.Spacing(20);

                        // Acheteur name
                        column.Item().PaddingTop(10).Text($"Acheteur : {acheteur.Nom} {acheteur.Prenom}").Bold();

                        // Basic Plan Info
                        column.Item().PaddingTop(25).Row(row =>
                        {
                            row.RelativeItem().Text($"Montant Total : {plan.MontantTotal} TND").Bold();
                            row.RelativeItem().Text($"Nombre d'échéances : {plan.NombreDeEcheances}").Bold();
                        });

                        // Factures Info Table
                        if (plan.Factures.Any())
                        {
                            column.Item().Text("Factures Concernées :").Bold().FontSize(14);
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1); // Référence
                                    columns.RelativeColumn(2); // Montant
                                    columns.RelativeColumn(2); // Date
                                });

                                // Header row
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

                                IContainer CellStyle(IContainer container) =>
                                    container.Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                            });
                        }

                        // Échéances Table
                        column.Item().Text("Échéances :").Bold().FontSize(14);
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2); // Date
                                columns.RelativeColumn(2); // Montant
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

                            IContainer CellStyle(IContainer container) =>
                                container.Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                        });

                        // Signature Box - Clear for OCR
                        column.Item().PaddingTop(50).Column(sig =>
                        {
                            sig.Item().Text("Signature de l'Acheteur :").FontSize(12).Bold();

                            sig.Item().Height(50).Border(1).Padding(5)
                                .AlignLeft().AlignBottom()
                                .Text(" "); // empty space for signature (for OCR)

                            sig.Item().PaddingTop(10).Text("Date : ___________________");
                        });
                    });

                    // Footer
                    page.Footer().AlignCenter().Text("Merci de respecter les échéances pour éviter toute pénalité.").Italic().FontSize(10);
                });
            }).GeneratePdf();
        }
    }
}
