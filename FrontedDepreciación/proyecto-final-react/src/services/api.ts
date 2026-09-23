import axios from 'axios';

const authApi = axios.create({
  baseURL: 'https://localhost:7040/api',
});

const activosApi = axios.create({
  baseURL: 'https://localhost:7097/api',
});

const attachToken = (config: any) => {
  const token = localStorage.getItem('token');
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
};

authApi.interceptors.request.use(attachToken);
activosApi.interceptors.request.use(attachToken);

export { authApi, activosApi };