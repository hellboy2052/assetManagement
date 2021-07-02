import { makeAutoObservable } from "mobx";

export default class ModalStore {
  modal = {
    open: false,
    body: null,
    data: null,
    size: null,
    firstLogin: false,
  };

  constructor() {
    makeAutoObservable(this);
  }

  openModal = (content, size, firstLogin = false) => {
    this.modal.open = true;
    this.modal.body = content;
    this.modal.size = size;
    this.modal.firstLogin = firstLogin;
  };

  openDetaiModal = (content, info) => {
    this.modal.open = true;
    this.modal.body = content;
    this.modal.data = info;
  };

  closeModal = () => {
    if (!this.modal.firstLogin) {
      this.modal.open = false;
      this.modal.body = null;
      this.modal.size = null;
    }
  };
  closeDetaiModal = () => {
    this.modal.open = false;
    this.modal.body = null;
    this.modal.data = null;
    this.modal.size = null;
  };
}
