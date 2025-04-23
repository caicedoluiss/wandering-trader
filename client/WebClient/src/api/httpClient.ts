import axios from "axios";
// import { JwtLocalStorageKey, AuthorizationHeader } from "~/utils/constants";

const httpClient = axios.create({
    baseURL: import.meta.env.VITE_TUCITA_API_URL,
    headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
    },
});

httpClient.interceptors.request.use(function (config) {
    // var jwt = localStorage.getItem(JwtLocalStorageKey);

    // if (!jwt && config.headers[AuthorizationHeader]) {
    //     delete config.headers[AuthorizationHeader];
    // } else if (jwt) {
    //     config.headers[AuthorizationHeader] = `Bearer ${jwt}`;
    // }

    return config;
});

httpClient.interceptors.response.use(
    function (response) {
        return Promise.resolve(response.data);
    },
    function (error) {
        console.debug("interceptor response error", error);

        if (!error.response) {
            return Promise.reject({
                code: "no_response",
                message: error.message,
            });
        }

        if (error.response.status === 401) {
            return Promise.reject({
                code: "unauthorized",
                message: "unauthorized",
            });
        }

        if (error.response.status === 403) {
            return Promise.reject({ code: "forbidden", message: "forbidden" });
        }

        if (error.response.status === 404) {
            return Promise.reject({ code: "not_found", message: "not found" });
        }

        if (error.response.status === 405) {
            return Promise.reject({
                code: "not_allowed",
                message: "not allowed",
            });
        }

        if (error.response.status >= 500) {
            return Promise.reject({
                code: "server_error",
                message: "internal server error",
            });
        }

        // if (!error.response.data) {
        //   return Promise.reject({ code: "no_data", message: error.message });
        // }

        if (
            error.response.data.message &&
            !error.response.data.validationErrors
        ) {
            return Promise.reject({
                code: "bad_request",
                message: error.response.data.message,
            });
        }

        if (error.response.data.validationErrors) {
            return Promise.reject({
                code: "validation_errors",
                validationErrors: error.response.data.validationErrors,
                message: error.response.data.message,
            });
        }

        return Promise.reject({ code: "no_message", message: error.message });
    }
);

export default httpClient;
