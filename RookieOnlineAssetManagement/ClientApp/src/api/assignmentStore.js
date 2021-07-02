import { makeAutoObservable, reaction, runInAction } from "mobx";
import consumer from "./consumer";
import { store } from "./store";

export default class AssignmentStore {
  assignmentRegistry = new Map();
  totalPages = 0;
  totalItems = 0;
  upperPageBound = 5;
  lowerPageBound = 0;
  assignmentPerPage = 10;
  filter = new Map()
    .set("State", [1, 2, 3])
    .set("AssignedDate", "")
    .set("QueryString", "")
    .set("currentIndex", 1);
  loading = false;
  loadingInitial = false;

  constructor() {
    makeAutoObservable(this);

    reaction(
      () => this.filter.keys(),
      () => {
        this.assignmentRegistry.clear();
        this.loadAssignments();
      }
    );

    reaction(
      () => this.totalItems,
      (totalItems) => {
        var lastRecord = this.totalPages * this.assignmentPerPage - totalItems;
        if (lastRecord < 0) {
          this.totalPages += 1;
        }

        if (lastRecord === this.assignmentPerPage) {
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

  get assignmentBySort() {
    // use splice to limit the length
    return Array.from(this.assignmentRegistry.values()).splice(
      0,
      this.assignmentPerPage
    );
  }

  setIndex = (n) => {
    // Use to trigger reaction
    const resetPredicate = () => {
      this.filter.delete("currentIndex");
    };
    resetPredicate();
    this.filter.set("currentIndex", n);
  };

  setFilter = (states) => {
    // Use to trigger reaction
    const resetPredicate = () => {
      this.filter.delete("State");
    };
    resetPredicate();
    this.filter.set("State", states);
    this.filter.set("currentIndex", 1);
  };
  setSearchQuery = (query) => {
    // Use to trigger reaction
    const resetPredicate = () => {
      this.filter.delete("QueryString");
    };
    resetPredicate();
    this.filter.set("QueryString", query);
    this.filter.set("currentIndex", 1);
  };
  setFilterDate = (date) => {
    // Use to trigger reaction
    const resetPredicate = () => {
      this.filter.delete("AssignedDate");
    };
    if (date != null) {
      resetPredicate();
      this.filter.set("AssignedDate", date !== "" ? this.formatDate(date) : "");
      this.filter.set("currentIndex", 1);
    }
  };

  get axiosParams() {
    const params = {};
    params["PageSize"] = this.assignmentPerPage;
    params["PageIndex"] = this.filter.get("currentIndex");
    this.filter.forEach((value, key) => {
      params[key] = value;
    });
    return params;
  }

  loadAssignments = async () => {
    this.setLoadingInitial(true);
    try {
      const assignments = await consumer.assignment.list(this.axiosParams);
      assignments.assignmentReadModelList.forEach((assignment) => {
        this.setAssignment(assignment);
      });
      runInAction(() => {
        this.totalPages = assignments.totalPages;
        this.totalItems = assignments.totalItems;
      });
      this.setLoadingInitial(false);
    } catch (err) {
      console.log(err);
      this.setLoadingInitial(false);
    }
  };

  loadAssignment = async (id) => {
    try {
      var assignment = await consumer.assignment.detail(id);
      return assignment;
    } catch (err) {
      console.log(err);
    }
  };

  loadEditAssignment = async (id) => {
    this.setLoadingInitial(true);
    try {
      let assignment = await consumer.assignment.editDetail(id);
      if (assignment) {
        this.setLoadingInitial(false);
        return assignment;
      }
      this.setLoadingInitial(false);
      return undefined;
    } catch (err) {
      console.log(err);
      this.setLoadingInitial(false);
    }
  };

  createAssignment = async (assignmentFormValues) => {
    try {
      var formData = new FormData();
      formData.append("UserId", assignmentFormValues.User.id);
      formData.append("AssestCode", assignmentFormValues.Asset.AssetCode);
      formData.append(
        "AssignedDate",
        this.formatDate(assignmentFormValues.AssignedDate)
      );
      formData.append("Note", assignmentFormValues.Note);
      const newAssignment = await consumer.assignment.create(formData);
      this.increaseTotalItems();
      runInAction(() => {
        store.assetStore.loadAssets();
        if (
          store.identityStore.account.fullname === assignmentFormValues.Fullname
        ) {
          store.identityStore.loadAssigns();
        }
        if (this.assignmentBySort.length >= 1) {
          newAssignment.assignedDate = this.formatDate(
            new Date(newAssignment.assignedDate)
          );
          var arr = this.assignmentBySort;
          arr.unshift(newAssignment);
          this.assignmentRegistry.clear();
          this.assignmentRegistry = new Map(
            arr.map((i) => [i.assignmentId, i])
          );
        } else {
          this.setAssignment(newAssignment);
        }
      });
    } catch (err) {
      throw err;
    }
  };

  updateAssignment = async (assignmentFormValues) => {
    try {
      var formData = new FormData();
      formData.append("AssignmentId", assignmentFormValues.AssignmentId);
      formData.append("UserId", assignmentFormValues.User.id);
      formData.append("AssestCode", assignmentFormValues.Asset.AssetCode);
      formData.append(
        "AssignedDate",
        this.formatDate(assignmentFormValues.AssignedDate)
      );
      formData.append("Note", assignmentFormValues.Note);
      var updatedAssignment = await consumer.assignment.update(formData);
      updatedAssignment.assignmentId = assignmentFormValues.AssignmentId;
      if (updatedAssignment.assignmentId) {
        if (this.assignmentBySort.length >= 1) {
          updatedAssignment.assignedDate = this.formatDate(
            new Date(updatedAssignment.assignedDate)
          );
          var arr = this.assignmentBySort.filter(
            (i) => i.assignmentId !== updatedAssignment.assignmentId
          );
          arr.unshift(updatedAssignment);
          runInAction(() => {
            store.assetStore.loadAssets();
            store.userStore.loadUsers();
            if (
              store.identityStore.account.fullname ===
              assignmentFormValues.Fullname
            ) {
              store.identityStore.loadAssigns();
            }
            this.assignmentRegistry.clear();
            this.assignmentRegistry = new Map(
              arr.map((i) => [i.assignmentId, i])
            );
          });
        } else {
          runInAction(() => {
            if (
              store.identityStore.account.fullname ===
              assignmentFormValues.Fullname
            ) {
              store.identityStore.loadAssigns();
            }
            store.assetStore.loadAssets();
            store.userStore.loadUsers();
          });
          this.setAssignment(updatedAssignment);
        }
      }
    } catch (err) {
      throw err;
    }
  };

  updateAssignmentState = async (id, state) => {
    try {
      if (this.assignmentRegistry.size >= 1) {
        let assignment = await this.loadAssignment(id);
        assignment.state = state;
        assignment.assignmentId = id;
        this.setAssignment(assignment);
      }
    } catch (err) {
      console.log(err);
    }
  };

  deleteAssignment = async (assignment) => {
    try {
      await consumer.assignment.delete(assignment.assignmentId);
      this.reduceTotalItems();
      runInAction(() => {
        this.removeAssignment(assignment.assignmentId);
        store.assetStore.loadAssets();
        store.userStore.loadUsers();
        store.identityStore.loadAssigns();
        store.modalStore.closeModal();
      });
    } catch (err) {
      console.log(err);
    }
  };

  requestReturn = async (id) => {
    this.setLoadingInitial(true);
    try {
      if (id) {
        let body = { assignmentId: id };
        await consumer.requestForReturning.request(body);
        runInAction(() => {
          this.assignmentRegistry.delete(id);
          this.loadAssignments();
          store.identityStore.assignRegistry.delete(id);
          store.identityStore.loadAssigns();
          store.returnStore.loadReturns();
          store.modalStore.closeModal();
        });
        this.setLoadingInitial(false);
      }
    } catch (err) {
      this.setLoadingInitial(false);
      console.log(err);
    }
  };

  getAssignment = (id) => {
    return this.assignmentRegistry.get(id);
  };

  removeAssignment = (id) => {
    this.assignmentRegistry.delete(id);
  };

  setAssignment = (assignment) => {
    assignment.assignedDate = this.formatDate(
      new Date(assignment.assignedDate)
    );
    this.assignmentRegistry.set(assignment.assignmentId, assignment);
  };

  setLoading = (state) => {
    this.loading = state;
  };

  setLoadingInitial = (state) => {
    this.loadingInitial = state;
  };

  formatDate(date) {
    return `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
  }
  compare = (a, b) => {
    if (a.assignedDate < b.assignedDate) {
      return 1;
    }
    if (a.assignedDate > b.assignedDate) {
      return -1;
    }
    return 0;
  };
}
