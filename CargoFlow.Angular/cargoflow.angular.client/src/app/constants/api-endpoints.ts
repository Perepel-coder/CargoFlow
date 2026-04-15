const BASE_URL = {
  CUSTOMER: 'customer',
  ORDER: 'order'
};

export const API_ENDPOINTS = {
  AUTH: {
    CREATE: `${BASE_URL.CUSTOMER}/authorization/create-customer/`,
    CHECK: `${BASE_URL.CUSTOMER}/authorization/check-customer/`,
  },

  ORDER: {
    HOME: `${BASE_URL.ORDER}/home`,
    CREATE: `${BASE_URL.ORDER}/home/create-order/`,
    GET_BY_CUSTOMER: `${BASE_URL.ORDER}/home/get-orders-by-customer/`,
    GET_BY_ID: `${BASE_URL.ORDER}/home/get-orders-by-id/`
  }
} as const;

export type ApiEndpoint = typeof API_ENDPOINTS;
