import React, { createContext, useContext, useState } from 'react';
import type { ReactNode } from 'react';
interface AuthState {
  token: string | null;
  nombreUsuario: string | null;
  rol: string | null;
}

interface AuthContextType extends AuthState {
  iniciarSesion: (token: string, nombreUsuario: string, rol: string) => void;
  cerrarSesion: () => void;
  estaAutenticado: boolean;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [auth, setAuth] = useState<AuthState>({
    token: localStorage.getItem('token'),
    nombreUsuario: localStorage.getItem('nombreUsuario'),
    rol: localStorage.getItem('rol'),
  });

  const iniciarSesion = (token: string, nombreUsuario: string, rol: string) => {
    localStorage.setItem('token', token);
    localStorage.setItem('nombreUsuario', nombreUsuario);
    localStorage.setItem('rol', rol);
    setAuth({ token, nombreUsuario, rol });
  };

  const cerrarSesion = () => {
    localStorage.clear();
    setAuth({ token: null, nombreUsuario: null, rol: null });
  };

  return (
    <AuthContext.Provider value={{ ...auth, iniciarSesion, cerrarSesion, estaAutenticado: !!auth.token }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) throw new Error('useAuth debe usarse dentro de un AuthProvider');
  return context;
};