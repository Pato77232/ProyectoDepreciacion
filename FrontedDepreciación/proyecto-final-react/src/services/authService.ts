import api from './api';

export interface LoginRequest {
  nombreUsuario: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  nombreUsuario: string;
  rol: string;
}

export const login = async (credenciales: LoginRequest): Promise<LoginResponse> => {
  const response = await api.post<LoginResponse>('/Auth/login', credenciales);
  return response.data;
};

export interface RegistroRequest {
  nombreUsuario: string;
  password: string;
  rol: string;
}

export const registrar = async (dto: RegistroRequest): Promise<void> => {
  await api.post('/Auth/registro', dto);
};