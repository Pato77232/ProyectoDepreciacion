import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { login } from '../services/authService';
import { useAuth } from '../context/AuthContext';
import './Login.css';

export const Login: React.FC = () => {
  const [usuario, setUsuario] = useState('');
  const [password, setPassword] = useState('');
  const [mostrarPassword, setMostrarPassword] = useState(false);
  const [rememberMe, setRememberMe] = useState(false);
  const [error, setError] = useState('');
  const [cargando, setCargando] = useState(false);

  const { iniciarSesion } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!usuario || !password) {
      setError('Por favor complete todos los campos.');
      return;
    }

    setError('');
    setCargando(true);

    try {
      const respuesta = await login({ nombreUsuario: usuario, password });
      iniciarSesion(respuesta.token, respuesta.nombreUsuario, respuesta.rol);
      navigate('/activos');
    } catch (err: any) {
      if (err.response?.status === 401) {
        setError('Usuario o contraseña incorrectos.');
      } else {
        setError('Ocurrió un error al conectar con el servidor.');
      }
    } finally {
      setCargando(false);
    }
  };

  return (
    <div className="split-login-container">
      {/* SECCIÓN IZQUIERDA: FORMULARIO */}
      <div className="login-form-section">
        <div className="form-content">
          <h2 className="form-title">BIENVENIDO</h2>
          <p className="form-subtitle">Por favor ingrese sus credenciales para acceder</p>

          {error && <div className="error-banner">{error}</div>}

          <form onSubmit={handleSubmit}>
            <div className="input-group">
              <label htmlFor="usuario">Usuario / Correo electrónico</label>
              <input
                type="text"
                id="usuario"
                value={usuario}
                onChange={(e) => setUsuario(e.target.value)}
                placeholder="shirley123@uta.edu.ec"
                autoComplete="off"
              />
            </div>

            <div className="input-group">
              <label htmlFor="password">Contraseña</label>
              <div className="password-wrapper">
                <input
                  type={mostrarPassword ? 'text' : 'password'}
                  id="password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  placeholder="••••••••••••"
                />
                <button
                  type="button"
                  className="eye-icon"
                  onClick={() => setMostrarPassword(!mostrarPassword)}
                  aria-label="Mostrar u ocultar contraseña"
                >
                  {mostrarPassword ? (
                    <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                      <path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z" />
                      <circle cx="12" cy="12" r="3" />
                    </svg>
                  ) : (
                    <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                      <path d="M17.94 17.94A10.07 10.07 0 0 1 12 20c-7 0-11-8-11-8a18.45 18.45 0 0 1 5.06-5.94M9.9 4.24A9.12 9.12 0 0 1 12 4c7 0 11 8 11 8a18.5 18.5 0 0 1-2.16 3.19m-6.72-1.07a3 3 0 1 1-4.24-4.24" />
                      <line x1="1" y1="1" x2="23" y2="23" />
                    </svg>
                  )}
                </button>
              </div>
            </div>
                  <p style={{ marginTop: '1.5rem', fontSize: '0.85rem', color: '#9ea2ae' }}>
  ¿No tienes cuenta? <a href="/registro" style={{ color: '#fff' }}>Regístrate</a>
</p>
            <div className="form-options">
              <label className="remember-me">
                <input
                  type="checkbox"
                  checked={rememberMe}
                  onChange={(e) => setRememberMe(e.target.checked)}
                />
                <span>Recordarme</span>
              </label>
              <a href="#forgot" className="forgot-link">
                ¿Olvidó su contraseña?
              </a>
            </div>

            <button type="submit" className="btn-submit" disabled={cargando}>
              {cargando ? 'INGRESANDO...' : 'INICIAR SESIÓN'}
            </button>
          </form>
        </div>
      </div>

      {/* SECCIÓN DERECHA: LOGO E ILUSTRACIÓN DE ACTIVOS FIJOS */}
      <div className="login-visual-section">
        <div className="visual-content">
          <div className="assets-logo-container">
            <svg viewBox="0 0 500 400" className="assets-svg" xmlns="http://www.w3.org/2000/svg">
              <circle cx="250" cy="200" r="170" fill="#1e2025" stroke="#333740" strokeWidth="2" />

              <g className="asset-building" transform="translate(130, 90)">
                <rect x="0" y="20" width="80" height="180" rx="4" fill="#0f1012" stroke="#ffffff" strokeWidth="3" />
                <line x1="40" y1="20" x2="40" y2="200" stroke="#333740" strokeWidth="2" />
                <rect x="12" y="40" width="18" height="22" rx="2" fill="#ffffff" />
                <rect x="50" y="40" width="18" height="22" rx="2" fill="#ffffff" />
                <rect x="12" y="80" width="18" height="22" rx="2" fill="#ffffff" />
                <rect x="50" y="80" width="18" height="22" rx="2" fill="#ffffff" />
                <rect x="12" y="120" width="18" height="22" rx="2" fill="#ffffff" />
                <rect x="50" y="120" width="18" height="22" rx="2" fill="#ffffff" />
                <rect x="30" y="160" width="20" height="40" rx="2" fill="#8a8d93" />
              </g>

              <g className="asset-vehicle" transform="translate(220, 180)">
                <path d="M 10 50 L 40 20 L 110 20 L 140 50 L 170 50 C 180 50, 185 60, 185 70 L 185 85 L 0 85 L 0 65 C 0 55, 5 50, 10 50 Z" fill="#16181c" stroke="#ffffff" strokeWidth="3" />
                <path d="M 45 25 L 75 25 L 75 48 L 25 48 Z" fill="#ffffff" />
                <path d="M 82 25 L 105 25 L 125 48 L 82 48 Z" fill="#ffffff" />
                <circle cx="45" cy="85" r="20" fill="#0f1012" stroke="#ffffff" strokeWidth="3" />
                <circle cx="45" cy="85" r="8" fill="#ffffff" />
                <circle cx="140" cy="85" r="20" fill="#0f1012" stroke="#ffffff" strokeWidth="3" />
                <circle cx="140" cy="85" r="8" fill="#ffffff" />
              </g>

              <g className="asset-computer" transform="translate(110, 210)">
                <rect x="0" y="0" width="100" height="65" rx="6" fill="#0f1012" stroke="#ffffff" strokeWidth="3" />
                <rect x="8" y="8" width="84" height="49" rx="2" fill="#2a2d34" />
                <path d="M 20 40 L 40 20 L 55 35 L 75 15 L 88 40" fill="none" stroke="#ffffff" strokeWidth="2.5" />
                <path d="M 40 65 L 60 65 L 65 80 L 35 80 Z" fill="#ffffff" />
                <rect x="25" y="80" width="50" height="5" rx="2" fill="#ffffff" />
              </g>
            </svg>
          </div>

          <h1 className="system-title-cursive">Sistema de Depreciación de Activos</h1>
        </div>
      </div>
    </div>
  );
};