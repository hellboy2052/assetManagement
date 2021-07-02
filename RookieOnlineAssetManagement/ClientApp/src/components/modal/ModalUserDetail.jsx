import { React } from "react";
import { observer } from "mobx-react-lite";
import { ModalHeader, ModalBody } from "reactstrap";
import { useStore } from "../../api/store";
import "./ModalUser.css";

export default observer(function ModalUserDetail() {
  const { modalStore } = useStore();
  return (
    <>
      <ModalHeader
        className="modal-header-userDetail"
        toggle={modalStore.closeDetaiModal}
      >
        Detailed User Information
      </ModalHeader>
      <ModalBody className="modal-body-userDetail">
        {modalStore.modal.data && (
          <>
            <div className="modal-userDetail">
              <ul>
                <li>Staff Code:</li>
                <li>Full Name:</li>
                <li>UserName:</li>
                <li>Date of Birth:</li>
                <li>Gender:</li>
                <li>Joined Date:</li>
                <li>Type:</li>
                <li>Location:</li>
              </ul>
              <ul style={{ marginLeft: "10px" }}>
                <li>{modalStore.modal.data.staffCode}</li>
                <li>{modalStore.modal.data.fullName}</li>
                <li>{modalStore.modal.data.userName}</li>
                <li>{modalStore.modal.data.dateOfBirth}</li>
                <li>{modalStore.modal.data.gender}</li>
                <li>{modalStore.modal.data.joinedDate}</li>
                <li>{modalStore.modal.data.type}</li>
                <li>{modalStore.modal.data.location}</li>
              </ul>
            </div>
          </>
        )}
      </ModalBody>
    </>
  );
});
