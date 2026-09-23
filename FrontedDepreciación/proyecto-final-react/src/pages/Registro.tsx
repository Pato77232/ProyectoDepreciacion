import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { registrar } from '../services/authService';
import './Login.css'; // reutilizamos el mismo estilo del login

export const Registro = () => {
  const [nombreUsuario, setNombreUsuario] = useState('');
  const [password, setPassword] = useState('');
  const [confirmarPassword, setConfirmarPassword] = useState('');
  const [rol, setRol] = useState('Contador');
  const [error, setError] = useState('');
  const [exito, setExito] = useState(false);
  const [cargando, setCargando] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (!nombreUsuario || !password) {
      setError('Complete todos los campos.');
      return;
    }
    if (password !== confirmarPassword) {
      setError('Las contraseñas no coinciden.');
      return;
    }
    if (password.length < 8) {
      setError('La contraseña debe tener al menos 8 caracteres.');
      return;
    }

    setCargando(true);
    try {
      await registrar({ nombreUsuario, password, rol });
      setExito(true);
      setTimeout(() => navigate('/login'), 1500);
    } catch (err: any) {
      if (err.response?.status === 400) {
        setError(err.response.data?.mensaje || 'El usuario ya existe.');
      } else {
        setError('Error al conectar con el servidor.');
      }
    } finally {
      setCargando(false);
    }
  };

  return (
    <div className="split-login-container">
      <div className="login-form-section">
        <div className="form-content">
          <h2 className="form-title">CREAR CUENTA</h2>
          <p className="form-subtitle">Regístrate para acceder al sistema</p>

          {error && <div className="error-banner">{error}</div>}
          {exito && (
            <div className="error-banner" style={{ color: '#4ade80', borderColor: 'rgba(74,222,128,0.3)', backgroundColor: 'rgba(74,222,128,0.1)' }}>
              Usuario creado correctamente. Redirigiendo...
            </div>
          )}

          <form onSubmit={handleSubmit}>
            <div className="input-group">
              <label htmlFor="nombreUsuario">Usuario</label>
              <input
                type="text"
                id="nombreUsuario"
                value={nombreUsuario}
                onChange={(e) => setNombreUsuario(e.target.value)}
                autoComplete="off"
              />
            </div>

            <div className="input-group">
              <label htmlFor="rol">Rol</label>
              <select
                id="rol"
                value={rol}
                onChange={(e) => setRol(e.target.value)}
                style={{ width: '100%', padding: '0.8rem 0.2rem', background: 'transparent', border: 'none', borderBottom: '2px solid #2a2d35', color: '#fff' }}
              >
                <option value="Contador">Contador</option>
                <option value="Auditor">Auditor</option>
                <option value="Admin">Admin</option>
              </select>
            </div>

            <div className="input-group">
              <label htmlFor="password">Contraseña</label>
              <input
                type="password"
                id="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>

            <div className="input-group">
              <label htmlFor="confirmarPassword">Confirmar contraseña</label>
              <input
                type="password"
                id="confirmarPassword"
                value={confirmarPassword}
                onChange={(e) => setConfirmarPassword(e.target.value)}
              />
            </div>

            <button type="submit" className="btn-submit" disabled={cargando}>
              {cargando ? 'CREANDO...' : 'REGISTRARSE'}
            </button>
          </form>

          <p style={{ marginTop: '1.5rem', fontSize: '0.85rem', color: '#9ea2ae' }}>
            ¿Ya tienes cuenta? <Link to="/login" style={{ color: '#fff' }}>Inicia sesión</Link>
          </p>
        </div>
      </div>

      <div className="login-visual-section">
        <div className="visual-content">
          <h1 className="system-title-cursive">Sistema de Depreciación de Activos</h1>
        </div>
      </div>
    </div>
  );
};