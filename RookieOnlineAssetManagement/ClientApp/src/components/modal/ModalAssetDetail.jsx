import React from "react";
import { observer } from "mobx-react-lite";
import { ModalHeader, ModalBody } from "reactstrap";
import { useStore } from "../../api/store";
import "./ModalUser.css";
import { HistoryTable } from "../../pages/Asset/HistoryTable";

export default observer(function ModalAssetDetail() {
  const {
    modalStore,
    assetStore: { formattingHistroyDate },
  } = useStore();
  return (
    <>
      <ModalHeader
        className="modal-header-userDetail asset-detail"
        toggle={modalStore.closeDetaiModal}
      >
        Detailed Asset Information
      </ModalHeader>
      <ModalBody className="modal-body-userDetail asset-detail">
        {modalStore.modal.data && (
          <>
            <div className="modal-userDetail">
              <ul>
                <li>Asset Code:</li>
                <li>Asset Name:</li>
                <li>Category:</li>
                <li>Installed Date:</li>
                <li>State:</li>
                <li>Location:</li>
                <li>Specification:</li>
                <li>History:</li>
              </ul>
              <ul>
                <li>{modalStore.modal.data.assetCode}</li>
                <li>{modalStore.modal.data.assetName}</li>
                <li>{modalStore.modal.data.category}</li>
                <li>{modalStore.modal.data.installDate}</li>
                <li>{modalStore.modal.data.state}</li>
                <li>{modalStore.modal.data.location}</li>
                <li>{modalStore.modal.data.specification}</li>
                <li>
                  <div>
                    <HistoryTable
                      histories={formattingHistroyDate(
                        modalStore.modal.data.histories
                      )}
                    ></HistoryTable>
                  </div>
                </li>
              </ul>
            </div>
          </>
        )}
      </ModalBody>
    </>
  );
});
