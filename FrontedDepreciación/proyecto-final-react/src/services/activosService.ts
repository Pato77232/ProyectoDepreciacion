import { activosApi, reportesApi } from './api';

export interface Activo {
  id: number;
  nombre: string;
  categoria: string;
  costoAdquisicion: number;
  valorResidual: number;
  fechaAdquisicion: string;
  vidaUtilAnios: number;
  porcentajeAnual: number;
  estaActivo: boolean;
}

export interface ActivoRequest {
  nombre: string;
  categoria: string;
  costoAdquisicion: number;
  fechaAdquisicion: string;
}

export interface DepreciacionAnual {
  anio: number;
  mesesDepreciados: number;
  depreciacionDelAnio: number;
  depreciacionAcumulada: number;
  valorEnLibros: number;
}

export interface ReporteDepreciacion {
  activoId: number;
  nombreActivo: string;
  costoAdquisicion: number;
  valorResidual: number;
  tabla: DepreciacionAnual[];
}

export const obtenerActivos = async (): Promise<Activo[]> => {
  const response = await activosApi.get<Activo[]>('/Activos');
  return response.data;
};

export const crearActivo = async (dto: ActivoRequest): Promise<Activo> => {
  const response = await activosApi.post<Activo>('/Activos', dto);
  return response.data;
};

export const obtenerReporteDepreciacion = async (id: number): Promise<ReporteDepreciacion> => {
  const response = await reportesApi.get<ReporteDepreciacion>(`/Reportes/depreciacion/activo/${id}`);
  return response.data;
};