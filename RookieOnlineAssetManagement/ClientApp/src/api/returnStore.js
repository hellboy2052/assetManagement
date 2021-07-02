import { makeAutoObservable, reaction, runInAction } from "mobx";
import consumer from "./consumer";
import { store } from "./store";

export default class ReturnStore {
  returnRegistry = new Map();
  filter = new Map()
    .set("State", [1, 2])
    .set("ReturnedDate", "")
    .set("QueryString", "");
  loading = false;
  loadingInitial = false;

  constructor() {
    makeAutoObservable(this);

    reaction(
      () => this.filter.keys(),
      () => {
        this.returnRegistry.clear();
        this.loadReturns();
      }
    );
  }

  get listReturn() {
    return Array.from(this.returnRegistry.values());
  }

  setFilter = (state) => {
    // Use to trigger reaction
    const resetPredicate = () => {
      this.filter.delete("State");
    };
    resetPredicate();
    this.filter.set("State", state);
  };

  setFilterDate = (date) => {
    // Use to trigger reaction
    const resetPredicate = () => {
      this.filter.delete("ReturnedDate");
    };
    resetPredicate();
    this.filter.set("ReturnedDate", date !== "" ? this.formatDate(date) : "");
  };

  setQueryString = (search) => {
    // Use to trigger reaction
    const resetPredicate = () => {
      this.filter.delete("QueryString");
    };
    resetPredicate();
    this.filter.set("QueryString", search);
  };

  get axiosParams() {
    const params = {};
    this.filter.forEach((value, key) => {
      params[key] = value;
    });
    return params;
  }

  loadReturns = async () => {
    try {
      const returns = await consumer.requestForReturning.filterList(
        this.axiosParams
      );
      returns.forEach((request) => {
        this.setRequest(request);
      });
    } catch (err) {
      console.log(err);
    }
  };

  acceptRequest = async (id) => {
    try {
      let body = { returnId: id };
      await consumer.requestForReturning.updateState(body);
      runInAction(() => {
        this.loadReturns();
        store.userStore.loadUsers();
        store.assetStore.loadAssets();
        store.modalStore.closeModal();
      });
    } catch (err) {
      console.log(err);
    }
  };

  cancelRequest = async (returnAssign) => {
    try {
      await consumer.requestForReturning.delete(returnAssign.returnId);
      runInAction(() => {
        this.returnRegistry.delete(returnAssign.returnId);
        store.userStore.loadUsers();
        store.assetStore.loadAssets();
        store.assignmentStore.loadAssignments();
        if (store.identityStore.account.username === returnAssign.requestedBy) {
          store.identityStore.loadAssigns();
        }
        store.modalStore.closeModal();
      });
    } catch (err) {
      console.log(err);
    }
  };

  setRequest = (request) => {
    request.assignedDate = this.formatDate(new Date(request.assignedDate));
    if (request.returnedDate !== "0001-01-01T00:00:00") {
      request.returnedDate = this.formatDate(new Date(request.returnedDate));
    } else {
      request.returnedDate = "N/A";
    }
    this.returnRegistry.set(request.returnId, request);
  };

  formatDate(date) {
    return `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
  }
}
