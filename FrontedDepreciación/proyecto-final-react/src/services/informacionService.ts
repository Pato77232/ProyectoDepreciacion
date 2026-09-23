import { informacionApi } from './api';

export interface InformacionDepreciacion {
  id: number;
  fecha: string;
  descripcion: string;
  areas: string;
}

export type InformacionDepreciacionRequest = Omit<InformacionDepreciacion, 'id'>;

export const obtenerInformacionDepreciacion = async (): Promise<InformacionDepreciacion[]> => {
  const response = await informacionApi.get<InformacionDepreciacion[]>('/Informacion');
  return response.data;
};

export const crearInformacionDepreciacion = async (
  informacion: InformacionDepreciacionRequest,
): Promise<InformacionDepreciacion> => {
  const response = await informacionApi.post<InformacionDepreciacion>('/Informacion', informacion);
  return response.data;
};