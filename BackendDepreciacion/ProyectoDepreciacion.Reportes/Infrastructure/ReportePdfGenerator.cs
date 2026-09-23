using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ProyectoDepreciacion.Reportes.Application.DTOs;

namespace ProyectoDepreciacion.Reportes.Infrastructure;

public class ReportePdfGenerator
{
    public byte[] Generar(ReporteDepreciacionDto reporte)
    {
        return Document.Create(documento => documento.Page(pagina =>
        {
            pagina.Size(PageSizes.A4);
            pagina.Margin(36);
            pagina.DefaultTextStyle(estilo => estilo.FontFamily("Arial").FontSize(9).FontColor("263238"));

            pagina.Header().Background("14202B").Padding(18).Column(encabezado =>
            {
                encabezado.Item().Text("REPORTE DE DEPRECIACION")
                    .FontSize(18).Bold().FontColor(Colors.White);
                encabezado.Item().Text("Sistema de Depreciacion de Activos")
                    .FontSize(9).FontColor("CFD8DC");
            });

            pagina.Content().PaddingTop(22).Column(contenido =>
            {
                contenido.Spacing(14);
                contenido.Item().Text(reporte.NombreActivo).FontSize(16).Bold().FontColor("14202B");

                contenido.Item().Table(tabla =>
                {
                    tabla.ColumnsDefinition(columnas =>
                    {
                        columnas.RelativeColumn();
                        columnas.RelativeColumn();
                        columnas.RelativeColumn();
                        columnas.RelativeColumn();
                    });

                    tabla.Cell().Background("E8EEF2").Padding(8).Text("Fecha de compra").Bold();
                    tabla.Cell().Padding(8).Text(FormatearFecha(reporte.FechaAdquisicion));
                    tabla.Cell().Background("E8EEF2").Padding(8).Text("Costo de adquisicion").Bold();
                    tabla.Cell().Padding(8).Text(FormatearMoneda(reporte.CostoAdquisicion));
                    tabla.Cell().Background("E8EEF2").Padding(8).Text("Valor residual").Bold();
                    tabla.Cell().Padding(8).Text(FormatearMoneda(reporte.ValorResidual));
                });

                contenido.Item().Table(tabla =>
                {
                    tabla.ColumnsDefinition(columnas =>
                    {
                        columnas.ConstantColumn(48);
                        columnas.ConstantColumn(48);
                        columnas.RelativeColumn();
                        columnas.RelativeColumn();
                        columnas.RelativeColumn();
                    });

                    tabla.Header(encabezado =>
                    {
                        encabezado.Cell().Background("233142").Padding(7).Text("Anio").Bold().FontColor(Colors.White);
                        encabezado.Cell().Background("233142").Padding(7).Text("Meses").Bold().FontColor(Colors.White);
                        encabezado.Cell().Background("233142").Padding(7).Text("Depreciacion del anio").Bold().FontColor(Colors.White);
                        encabezado.Cell().Background("233142").Padding(7).Text("Acumulada").Bold().FontColor(Colors.White);
                        encabezado.Cell().Background("233142").Padding(7).Text("Valor en libros").Bold().FontColor(Colors.White);
                    });

                    foreach (var fila in reporte.Tabla)
                    {
                        var fondo = fila.Anio % 2 == 0 ? "F1F5F9" : "FFFFFF";
                        tabla.Cell().Background(fondo).BorderBottom(1).BorderColor("DCE3E8").Padding(7).Text(fila.Anio.ToString());
                        tabla.Cell().Background(fondo).BorderBottom(1).BorderColor("DCE3E8").Padding(7).Text(fila.MesesDepreciados.ToString());
                        tabla.Cell().Background(fondo).BorderBottom(1).BorderColor("DCE3E8").Padding(7).AlignRight().Text(FormatearMoneda(fila.DepreciacionDelAnio));
                        tabla.Cell().Background(fondo).BorderBottom(1).BorderColor("DCE3E8").Padding(7).AlignRight().Text(FormatearMoneda(fila.DepreciacionAcumulada));
                        tabla.Cell().Background(fondo).BorderBottom(1).BorderColor("DCE3E8").Padding(7).AlignRight().Text(FormatearMoneda(fila.ValorEnLibros));
                    }
                });
            });

            pagina.Footer().AlignRight().Text(texto =>
            {
                texto.Span("Generado por Reportes | ");
                texto.CurrentPageNumber();
                texto.Span(" de ");
                texto.TotalPages();
            });
        })).GeneratePdf();
    }

    private static string FormatearMoneda(decimal valor) =>
        $"${valor:N2}";

    private static string FormatearFecha(DateTime fecha) =>
        fecha.ToString("dd/MM/yyyy");
}
