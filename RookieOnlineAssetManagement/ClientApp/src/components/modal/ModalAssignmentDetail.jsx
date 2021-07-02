import React from "react";
import { observer } from "mobx-react-lite";
import { ModalHeader, ModalBody } from "reactstrap";
import { useStore } from "../../api/store";
import "./ModalUser.css";

export default observer(function ModalAssignmentDetail() {
  const { modalStore } = useStore();
  return (
    <>
      <ModalHeader
        className="modal-header-userDetail"
        toggle={modalStore.closeDetaiModal}
      >
        Detailed Assignment Information
      </ModalHeader>
      <ModalBody className="modal-body-userDetail">
        {modalStore.modal.data && (
          <>
            <div className="modal-userDetail">
              <ul>
                <li>Asset Code: </li>
                <li>Asset Name: </li>
                <li>Specification: </li>
                <li>Assigned To: </li>
                <li>Assigned By: </li>
                <li>Assigned Date: </li>
                <li>State: </li>
                <li>Note: </li>
              </ul>
              <ul className="modalListData" style={{ marginLeft: "10px" }}>
                <li>{modalStore.modal.data.assetCode}</li>
                <li>{modalStore.modal.data.assetName}</li>
                <li>{modalStore.modal.data.specification ?? "null"}</li>
                <li>{modalStore.modal.data.assignedTo ?? "null"}</li>
                <li>{modalStore.modal.data.assignedBy ?? "null"}</li>
                <li>{modalStore.modal.data.assignedDate}</li>
                <li>{modalStore.modal.data.state}</li>
                <li>{modalStore.modal.data.note ?? "null"}</li>
              </ul>
            </div>
          </>
        )}
      </ModalBody>
    </>
  );
});
