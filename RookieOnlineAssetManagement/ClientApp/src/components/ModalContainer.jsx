import React from "react";
import { observer } from "mobx-react-lite";
import { Modal } from "reactstrap";
import { useStore } from "../api/store";

export default observer(function ModalContainer() {
  const { modalStore } = useStore();
  return (
    <Modal
      size={modalStore.modal.size || ""}
      backdrop={true}
      isOpen={modalStore.modal.open}
      fade={false}
      toggle={modalStore.closeModal}
    >
      {modalStore.modal.body}
    </Modal>
  );
});
