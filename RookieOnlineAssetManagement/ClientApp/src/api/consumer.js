import axios from "axios";
import qs from "qs";

const sleep = (deplay) => {
  return new Promise((resolve) => {
    setTimeout(resolve, deplay);
  });
};

axios.interceptors.request.use((config) => {
  return config;
});

axios.interceptors.response.use(
  (res) => {
    if (process.env.NODE_ENV === "development") {
      sleep(1000);
    }
    return res;
  },
  async (error) => {
    const { data, status } = error.response;
    switch (status) {
      case 403:
        window.location.reload();
        break;
      case 401:
        window.location.href = "/Identity/Account/Login?returnUrl=/";
        break;
      case 400:
        if (data.errors) {
          const modalStateErrors = [];

          for (const key in data.errors) {
            if (data.errors[key]) {
              modalStateErrors.push(data.errors[key]);
            }
          }
          throw modalStateErrors.flat();
        } else {
          throw [data].flat();
        }
      default:
        break;
    }
  }
);

const responseBody = (response) => response.data;

const request = {
  get: (url) => axios.get("/api/" + url).then(responseBody),
  post: (url, body) => axios.post("/api/" + url, body).then(responseBody),
  put: (url, body) => axios.put("/api/" + url, body).then(responseBody),
  del: (url) => axios.delete("/api/" + url).then(responseBody),
};

const user = {
  list: () => request.get("users?PageIndex=1&PageSize=10"),
  filterList: (params) =>
    axios
      .get(`/api/users/filter`, {
        params: {
          Type: params.Type,
          PageSize: params.PageSize,
          PageIndex: params.PageIndex,
          QueryString: params.QueryString,
        },
        paramsSerializer: (params) => {
          return qs.stringify(params, { arrayFormat: "repeat" });
        },
      })
      .then(responseBody),
  create: (userFormValues) => request.post("users", userFormValues),
  update: (userFormValues) =>
    request.put(`users/${userFormValues.get("Id")}`, userFormValues),
  disable: (id) => request.del("Users/" + id),
};

const account = {
  current: () => request.get("Account"),
  changePassword: (body) => request.put("Account/changepassword", body),
  resetPassword: (body) => request.put("Account/resetpassword", body),
  logout: () => request.post("Account/Logout", {}),
  firstLogin: () => request.get("Account/FirstLogin"),
  listAssign: () => request.get("Assignments/user"),
  respond: (updateState) => request.post("Assignments/state", updateState),
};

const category = {
  list: () => request.get("Categories"),
  create: (data) => request.post("Categories", data),
};

const asset = {
  list: () => request.get("assets?PageIndex=1&PageSize=10"),
  detail: (assetCode) => request.get(`assets/${assetCode}`),
  filterList: (params) =>
    axios
      .get(`/api/Assets/filter`, {
        params: {
          Category: params.Category,
          PageSize: params.PageSize,
          States: params.States,
          PageIndex: params.PageIndex,
          QueryString: params.QueryString,
        },
        paramsSerializer: (params) => {
          return qs.stringify(params, { arrayFormat: "repeat" });
        },
      })
      .then(responseBody),
  create: (assetFormValues) => request.post("assets", assetFormValues),
  update: (assetFormValues) =>
    request.put(`assets/${assetFormValues.get("AssetCode")}`, assetFormValues),
  delete: (id) => request.del("assets/" + id),
};

const report = {
  list: () => request.get("reports"),
};

const assignment = {
  list: (params) =>
    axios
      .get(`api/Assignments`, {
        params: {
          QueryString: params.QueryString,
          AssignedDate: params.AssignedDate,
          States: params.State,
          PageIndex: params.PageIndex,
          PageSize: params.PageSize,
        },
        paramsSerializer: (params) => {
          return qs.stringify(params, { arrayFormat: "repeat" });
        },
      })
      .then(responseBody),
  detail: (id) => request.get(`Assignments/${id}`),
  editDetail: (id) => request.get(`Assignments/edit/${id}`),
  create: (assignmentFormValues) =>
    request.post("Assignments", assignmentFormValues),
  update: (assignmentFormValues) =>
    request.put(
      `Assignments/${assignmentFormValues.get("AssignmentId")}`,
      assignmentFormValues
    ),
  delete: (id) => request.del("Assignments/" + id),
};

const requestForReturning = {
  list: () => request.get("Returnings"),
  filterList: (params) =>
    axios
      .get(`api/Returnings/filter`, {
        params: {
          QueryString: params.QueryString,
          ReturnedDate: params.ReturnedDate,
          States: params.State,
        },
        paramsSerializer: (params) => {
          return qs.stringify(params, { arrayFormat: "repeat" });
        },
      })
      .then(responseBody),
  delete: (id) => request.del("Returnings/" + id),
  updateState: (id) => request.post("Returnings/state", id),
  request: (body) => request.post("Returnings", body),
};

const consumer = {
  user,
  account,
  category,
  asset,
  assignment,
  requestForReturning,
  report,
};

export default consumer;
