using ProyectoDepreciación.Domain.Entities;

namespace ProyectoDepreciación.Domain.Services;

public record DepreciacionAnual(int Anio, int MesesDepreciados, decimal DepreciacionDelAnio,
                                  decimal DepreciacionAcumulada, decimal ValorEnLibros);

public class CalculadoraDepreciacion
{
    public List<DepreciacionAnual> GenerarTabla(Activo activo)
    {
        var resultado = new List<DepreciacionAnual>();
        decimal baseDepreciable = activo.CostoAdquisicion - activo.ValorResidual;
        decimal depreciacionMensual = (baseDepreciable * (activo.PorcentajeAnual / 100m)) / 12;
        int mesesVidaUtilTotal = activo.VidaUtilAnios * 12;

        int mesesAcumulados = 0;
        decimal depreciacionAcumulada = 0;
        int anioActual = activo.FechaAdquisicion.Year;
        int mesCursor = activo.FechaAdquisicion.Month;

        while (mesesAcumulados < mesesVidaUtilTotal)
        {
            int mesesDisponiblesEnAnio = 13 - mesCursor;
            int mesesFaltantes = mesesVidaUtilTotal - mesesAcumulados;
            int mesesEsteAnio = Math.Min(mesesDisponiblesEnAnio, mesesFaltantes);

            decimal depreciacionEsteAnio = depreciacionMensual * mesesEsteAnio;
            if (mesesAcumulados + mesesEsteAnio == mesesVidaUtilTotal)
                depreciacionEsteAnio = baseDepreciable - depreciacionAcumulada;

            depreciacionAcumulada += depreciacionEsteAnio;
            mesesAcumulados += mesesEsteAnio;

            resultado.Add(new DepreciacionAnual(anioActual, mesesEsteAnio,
                Math.Round(depreciacionEsteAnio, 2), Math.Round(depreciacionAcumulada, 2),
                Math.Round(activo.CostoAdquisicion - depreciacionAcumulada, 2)));

            anioActual++;
            mesCursor = 1;
        }

        return resultado;
    }
}