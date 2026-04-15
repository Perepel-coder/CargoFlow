/*const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7140';

const PROXY_CONFIG = [
  {
    context: [
      "/customer",
      "/order",
    ],
    target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
*/

const { env } = require('process');

// Проверяем переменные окружения
const target = env.ASPNETCORE_HTTPS_PORT
  ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
  : env.ASPNETCORE_URLS
    ? env.ASPNETCORE_URLS.split(';')[0]
    : 'http://localhost:5172'; // Ваш HTTPS порт из launchSettings.json

const PROXY_CONFIG = [
  {
    context: [
      "/customer",
      "/order",
    ],
    target,
    secure: false, // Важно: игнорируем проверку сертификата для локальной разработки
    changeOrigin: true,
    logLevel: "debug",
    // Добавьте эти настройки для работы с HTTPS
    headers: {
      "Connection": "keep-alive"
    },
    proxyTimeout: 60000,
    timeout: 60000
  }
]

module.exports = PROXY_CONFIG;
