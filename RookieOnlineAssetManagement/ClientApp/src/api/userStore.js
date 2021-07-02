import { makeAutoObservable, reaction, runInAction } from "mobx";
import consumer from "./consumer";
import { store } from "./store";

export default class UserStore {
  userRegistry = new Map();
  tableUser = [];
  totalPages = 0;
  totalItems = 0;
  userPerPage = 10;
  upperPageBound = 5;
  lowerPageBound = 0;
  filter = new Map()
    .set("Type", ["Admin", "Staff"])
    .set("QueryString", "")
    .set("currentIndex", 1);
  loading = false;
  loadingInitial = false;

  constructor() {
    makeAutoObservable(this);

    reaction(
      () => this.filter.keys(),
      () => {
        this.userRegistry.clear();
        this.loadUsers();
      }
    );

    reaction(
      () => this.totalItems,
      (totalItems) => {
        var lastRecord = this.totalPages * this.userPerPage - totalItems;
        if (lastRecord < 0) {
          this.totalPages += 1;
        }

        if (lastRecord === this.userPerPage) {
          this.totalPages -= 1;
          if (this.filter.get("currentIndex") !== 1) {
            this.filter.set(
              "currentIndex",
              this.filter.get("currentIndex") - 1
            );
          }
        }
      }
    );
  }

  increaseTotalItems = () => {
    this.totalItems = this.totalItems + 1;
  };

  reduceTotalItems = () => {
    this.totalItems = this.totalItems - 1;
  };

  setUpperBound = (n) => {
    this.upperPageBound = n;
  };
  setLowerBound = (n) => {
    this.lowerPageBound = n;
  };

  get tableAxiosParam() {
    const params = {};
    params["PageSize"] = this.userPerPage;
    params["PageIndex"] = this.filter.get("currentIndex");
    this.filter.forEach((value, key) => {
      params[key] = value;
    });
    return params;
  }

  loadTableUsers = async () => {
    this.setLoadingInitial(true);
    try {
      const user = await consumer.user.filterList(this.tableAxiosParam);

      runInAction(() => {
        this.tableUser = user.userReadModelList;
      });
      this.setLoadingInitial(false);
    } catch (err) {
      console.log(err);
      this.setLoadingInitial(false);
    }
  };

  setTableSearchQuery = (search) => {
    const resetPredicate = () => {
      this.filter.delete("QueryString");
    };
    resetPredicate();
    this.filter.set("QueryString", search);
    this.filter.set("currentIndex", 1);
    this.loadTableUsers();
  };

  get userByNameSort() {
    // use splice to limit the length
    return Array.from(this.userRegistry.values()).splice(0, this.userPerPage);
  }

  get axiosParam() {
    const params = {};
    params["PageSize"] = this.userPerPage;
    params["PageIndex"] = this.filter.get("currentIndex");
    this.filter.forEach((value, key) => {
      params[key] = value;
    });
    return params;
  }

  setFilter = (types) => {
    const resetPredicate = () => {
      this.filter.delete("Type");
    };
    resetPredicate();
    this.filter.set("Type", types);
    this.filter.set("currentIndex", 1);
  };

  setSearchQuery = (search) => {
    const resetPredicate = () => {
      this.filter.delete("QueryString");
    };
    resetPredicate();
    this.filter.set("QueryString", search);
    this.filter.set("currentIndex", 1);
  };

  loadUsers = async () => {
    this.setLoadingInitial(true);
    try {
      const users = await consumer.user.filterList(this.axiosParam);
      users.userReadModelList.forEach((user) => {
        this.setUser(user);
      });
      runInAction(() => {
        this.totalPages = users.totalPages;
        this.totalItems = users.totalItems;
      });
      this.setLoadingInitial(false);
    } catch (err) {
      console.log(err);
      this.setLoadingInitial(false);
    }
  };

  getUser = (id) => {
    return this.userRegistry.get(id);
  };

  loadUser = async (id) => {
    let user = this.getUser(id);
    if (user) {
      return user;
    } else {
      this.setLoadingInitial(true);
      try {
        const users = await consumer.user.list();
        user = users.userReadModelList.find((x) => x.id === id);
        this.setLoadingInitial(false);
        return user;
      } catch (error) {
        console.log(error);
        this.setLoadingInitial(false);
      }
    }
  };

  createUser = async (userFormValues) => {
    try {
      var formData = new FormData();
      formData.append("FirstName", userFormValues.firstName);
      formData.append("LastName", userFormValues.lastName);
      formData.append("Gender", userFormValues.gender);
      formData.append("JoinedDate", this.formatDate(userFormValues.joinedDate));
      formData.append("DoB", this.formatDate(userFormValues.doB));
      formData.append("Type", userFormValues.type);
      formData.append("id", userFormValues.id);
      const newUser = await consumer.user.create(formData);
      this.increaseTotalItems();
      if (this.userByNameSort.length >= 1) {
        newUser.dateOfBirth = this.formatDate(new Date(newUser.dateOfBirth));
        newUser.joinedDate = this.formatDate(new Date(newUser.joinedDate));
        var arr = this.userByNameSort;
        arr.unshift(newUser);
        runInAction(() => {
          this.userRegistry.clear();
          this.userRegistry = new Map(arr.map((i) => [i.id, i]));
        });
      } else {
        this.setUser(newUser);
      }
    } catch (error) {
      throw error;
    }
  };

  updateUser = async (userFormValues) => {
    try {
      var formData = new FormData();
      formData.append("Id", userFormValues.id);
      formData.append("DateOfBirth", this.formatDate(userFormValues.doB));
      formData.append("Gender", userFormValues.gender);
      formData.append("JoinedDate", this.formatDate(userFormValues.joinedDate));
      formData.append("Type", userFormValues.type);
      let updatedUser = await consumer.user.update(formData);
      runInAction(() => {
        if (updatedUser.id) {
          if (this.userByNameSort.length >= 1) {
            updatedUser.dateOfBirth = this.formatDate(
              new Date(updatedUser.dateOfBirth)
            );
            updatedUser.joinedDate = this.formatDate(
              new Date(updatedUser.joinedDate)
            );
            var arr = this.userByNameSort.filter(
              (i) => i.id !== updatedUser.id
            );
            arr.unshift(updatedUser);
            this.userRegistry.clear();
            this.userRegistry = new Map(arr.map((i) => [i.id, i]));
          } else {
            this.setUser(updatedUser);
          }
        }
      });
    } catch (error) {
      throw error;
    }
  };

  formatDate(date) {
    return `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
  }

  deleteUser = async (id) => {
    try {
      await consumer.user.disable(id);
      this.reduceTotalItems();
      runInAction(() => {
        this.userRegistry.delete(id);
        store.assignmentStore.loadAssignments();
        store.modalStore.closeModal();
      });
    } catch (err) {
      console.log(err);
    }
  };

  setUser = (user) => {
    user.dateOfBirth = this.formatDate(new Date(user.dateOfBirth));
    user.joinedDate = this.formatDate(new Date(user.joinedDate));
    this.userRegistry.set(user.id, user);
  };

  setIndex = (n) => {
    const resetPredicate = () => {
      this.filter.delete("currentIndex");
    };
    resetPredicate();
    this.filter.set("currentIndex", n);
  };

  setLoadingInitial = (state) => {
    this.loadingInitial = state;
  };

  setLoading = (state) => {
    this.loading = state;
  };

  compare = (a, b) => {
    if (a.staffCode < b.staffCode) {
      return 1;
    }
    if (a.staffCode > b.staffCode) {
      return -1;
    }
    return 0;
  };
}
