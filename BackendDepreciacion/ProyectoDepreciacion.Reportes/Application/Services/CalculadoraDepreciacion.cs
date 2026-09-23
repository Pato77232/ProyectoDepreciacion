using ProyectoDepreciacion.Reportes.Application.DTOs;

namespace ProyectoDepreciacion.Reportes.Application.Services;

public class CalculadoraDepreciacion
{
    public List<DepreciacionAnualDto> GenerarTabla(ActivoDto activo)
    {
        var resultado = new List<DepreciacionAnualDto>();
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

            resultado.Add(new DepreciacionAnualDto
            {
                Anio = anioActual,
                MesesDepreciados = mesesEsteAnio,
                DepreciacionDelAnio = Math.Round(depreciacionEsteAnio, 2),
                DepreciacionAcumulada = Math.Round(depreciacionAcumulada, 2),
                ValorEnLibros = Math.Round(activo.CostoAdquisicion - depreciacionAcumulada, 2)
            });

            anioActual++;
            mesCursor = 1;
        }

        return resultado;
    }
}
