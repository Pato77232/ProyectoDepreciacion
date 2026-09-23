import { useEffect, useState } from 'react';
import { obtenerActivos, crearActivo, obtenerReporteDepreciacion } from '../services/activosService';
import type { Activo, ActivoRequest, ReporteDepreciacion } from '../services/activosService';
import { useAuth } from '../context/AuthContext';
import './Activos.css';

export const Activos = () => {
  const [activos, setActivos] = useState<Activo[]>([]);
  const [cargando, setCargando] = useState(true);
  const [error, setError] = useState('');
  const [reporte, setReporte] = useState<ReporteDepreciacion | null>(null);
  const { nombreUsuario, rol, cerrarSesion } = useAuth();

  const [nuevoActivo, setNuevoActivo] = useState<ActivoRequest>({
    nombre: '',
    categoria: 'Computo',
    costoAdquisicion: 0,
    fechaAdquisicion: '',
  });

  const cargarActivos = async () => {
    try {
      setCargando(true);
      const data = await obtenerActivos();
      setActivos(data);
    } catch (err) {
      setError('No se pudieron cargar los activos.');
    } finally {
      setCargando(false);
    }
  };

  useEffect(() => {
    cargarActivos();
  }, []);

  const handleCrear = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await crearActivo(nuevoActivo);
      setNuevoActivo({ nombre: '', categoria: 'Computo', costoAdquisicion: 0, fechaAdquisicion: '' });
      cargarActivos();
    } catch (err) {
      setError('Error al crear el activo. Verifica los datos o tus permisos.');
    }
  };

  const handleVerDepreciacion = async (id: number) => {
    try {
      const data = await obtenerReporteDepreciacion(id);
      setReporte(data);
    } catch (err) {
      setError('No se pudo cargar el reporte de depreciación.');
    }
  };

  return (
    <div className="activos-container">
      <div className="activos-header">
        <h1>Activos Fijos</h1>
        <div className="usuario-info">
          <span>{nombreUsuario} ({rol})</span>
          <button className="btn-cerrar-sesion" onClick={cerrarSesion}>Cerrar sesión</button>
        </div>
      </div>

      {error && <div className="mensaje-error">{error}</div>}

      <form className="form-crear-activo" onSubmit={handleCrear}>
        <input
          placeholder="Nombre"
          value={nuevoActivo.nombre}
          onChange={(e) => setNuevoActivo({ ...nuevoActivo, nombre: e.target.value })}
          required
        />
        <select
          value={nuevoActivo.categoria}
          onChange={(e) => setNuevoActivo({ ...nuevoActivo, categoria: e.target.value })}
        >
          <option value="Inmueble">Inmueble</option>
          <option value="Maquinaria">Maquinaria</option>
          <option value="Vehiculo">Vehículo</option>
          <option value="Computo">Cómputo</option>
        </select>
        <input
          type="number"
          placeholder="Costo"
          value={nuevoActivo.costoAdquisicion || ''}
          onChange={(e) => setNuevoActivo({ ...nuevoActivo, costoAdquisicion: Number(e.target.value) })}
          required
        />
        <input
          type="date"
          value={nuevoActivo.fechaAdquisicion}
          onChange={(e) => setNuevoActivo({ ...nuevoActivo, fechaAdquisicion: e.target.value })}
          required
        />
        <button className="btn-crear" type="submit">Crear Activo</button>
      </form>

      {cargando ? (
        <p>Cargando...</p>
      ) : (
        <table className="tabla-activos">
          <thead>
            <tr>
              <th>Id</th>
              <th>Nombre</th>
              <th>Categoría</th>
              <th>Costo</th>
              <th>Fecha adquisición</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {activos.map((a) => (
              <tr key={a.id}>
                <td>{a.id}</td>
                <td>{a.nombre}</td>
                <td>{a.categoria}</td>
                <td>${a.costoAdquisicion}</td>
                <td>{a.fechaAdquisicion?.split('T')[0]}</td>
                <td>
                  <button className="btn-ver-reporte" onClick={() => handleVerDepreciacion(a.id)}>
                    Ver depreciación
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      {reporte && (
        <div className="modal-overlay" onClick={() => setReporte(null)}>
          <div className="modal-reporte" onClick={(e) => e.stopPropagation()}>
            <div className="modal-header">
              <h2>{reporte.nombreActivo}</h2>
              <button className="btn-cerrar-modal" onClick={() => setReporte(null)}>✕</button>
            </div>
            <p className="modal-subtitulo">
              Costo: ${reporte.costoAdquisicion} — Valor residual: ${reporte.valorResidual}
            </p>
            <table className="tabla-activos">
              <thead>
                <tr>
                  <th>Año</th>
                  <th>Meses</th>
                  <th>Depreciación del año</th>
                  <th>Acumulada</th>
                  <th>Valor en libros</th>
                </tr>
              </thead>
              <tbody>
                {reporte.tabla.map((fila) => (
                  <tr key={fila.anio}>
                    <td>{fila.anio}</td>
                    <td>{fila.mesesDepreciados}</td>
                    <td>${fila.depreciacionDelAnio.toFixed(2)}</td>
                    <td>${fila.depreciacionAcumulada.toFixed(2)}</td>
                    <td>${fila.valorEnLibros.toFixed(2)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      )}
    </div>
  );
};