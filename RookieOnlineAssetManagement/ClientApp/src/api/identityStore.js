import { makeAutoObservable, reaction, runInAction } from "mobx";
import { history } from "..";
import { AssignmentStateOptions } from "../components/option/AssignmentStateOption";
import consumer from "./consumer";
import { store } from "./store";

export default class IdentityStore {
  currentLocation = null;
  assignRegistry = new Map();
  account = null;
  loadingInitial = false;
  appLoaded = false;

  constructor() {
    makeAutoObservable(this);
    reaction(
      () => this.account,
      (account) => {
        if (account.isDisabled) {
          this.logout();
        }
      }
    );
  }

  get isLoggedIn() {
    return !!this.account;
  }

  get assignList() {
    return Array.from(this.assignRegistry.values());
  }

  setLocation = () => {
    this.currentLocation = history.location.pathname.split("/")[1];
  };

  setAccount = async () => {
    try {
      const acc = await consumer.account.current();
      runInAction(() => (this.account = acc));
    } catch (err) {
      console.log(err);
    }
  };

  changePassword = async (changePwd) => {
    try {
      const acc = await consumer.account.changePassword(changePwd);
      console.log(acc);
      runInAction(() => (this.account = acc));
      store.modalStore.closeModal();
    } catch (error) {
      throw error;
    }
  };
  resetPassword = async (newpwd) => {
    try {
      const body = {
        currentPassword: "",
        newPassword: newpwd,
      };
      const acc = await consumer.account.resetPassword(body);
      runInAction(() => {
        this.account = acc;
        store.modalStore.modal.firstLogin = false;
        store.modalStore.closeModal();
      });
    } catch (error) {
      throw error;
    }
  };

  loadAssigns = async () => {
    this.setLoadingInitial(true);
    try {
      const assigns = await consumer.account.listAssign();
      assigns.forEach((assign) => {
        this.setAssign(assign);
      });
      this.setLoadingInitial(false);
    } catch (err) {
      this.setLoadingInitial(false);
      console.log(err);
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

  respondAssign = async (id, state) => {
    store.modalStore.closeModal();
    this.setLoadingInitial(true);
    try {
      let updateState = {
        assignmentId: id,
        assignmentState: state,
      };
      // Call API to handle Database
      await consumer.account.respond(updateState);
      // Adjust data similar to Database

      if (id && state === 1) {
        // Accepted
        var updatedAssign = this.getAssign(id);
        updatedAssign.state = AssignmentStateOptions.find(
          (i) => i.value === state
        ).text;
        var arr = this.assignList.filter((i) => i.assignmentId !== id);
        arr.unshift(updatedAssign);

        runInAction(() => {
          this.assignRegistry.clear();
          this.assignRegistry = new Map(arr.map((i) => [i.assignmentId, i]));
          if (this.account.role === "Admin") {
            store.userStore.loadUsers();
            store.assetStore.loadAssets();
            store.assignmentStore.loadAssignments();
          }
        });
      } else {
        // Declined
        runInAction(() => {
          this.assignRegistry.delete(id);
          if (this.account.role === "Admin") {
            store.userStore.loadUsers();
            store.assetStore.loadAssets();
            store.assignmentStore.loadAssignments();
          }
        });
      }
      this.setLoadingInitial(false);
    } catch (err) {
      this.setLoadingInitial(false);
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
          this.assignRegistry.delete(id);
          this.loadAssigns();
          if (this.account.role === "Admin") {
            store.returnStore.loadReturns();
            store.assignmentStore.assignmentRegistry.delete(id);
            store.assignmentStore.loadAssignments();
          }
          store.modalStore.closeModal();
        });
      }
      this.setLoadingInitial(false);
    } catch (err) {
      this.setLoadingInitial(false);
      console.log(err);
    }
  };

  getAssign = (id) => {
    return this.assignRegistry.get(id);
  };

  setAssign = (assign) => {
    assign.assignedDate = this.formatDate(new Date(assign.assignedDate));
    this.assignRegistry.set(assign.assignmentId, assign);
  };

  firstLogin = async () => {
    try {
      const acc = await consumer.account.firstLogin();
      runInAction(() => (this.account = acc));
    } catch (error) {
      console.log(error);
    }
  };

  logout = async () => {
    try {
      store.modalStore.closeModal();
      this.appLoaded = false;
      await consumer.account.logout();
      runInAction(() => {
        this.account = null;
      });
      window.location.reload();
    } catch (err) {
      console.log(err);
    }
  };

  formatDate(date) {
    return `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
  }

  setLoadingInitial = (state) => {
    this.loadingInitial = state;
  };

  setAppLoaded = () => {
    this.appLoaded = true;
  };
}
