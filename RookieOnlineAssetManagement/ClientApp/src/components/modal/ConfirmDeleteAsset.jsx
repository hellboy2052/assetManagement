import { React } from "react";
import { observer } from "mobx-react-lite";
import { Button, ModalHeader, ModalBody, Row, Col, Label } from "reactstrap";
import { useStore } from "../../api/store";
import "./PopupModal.css";
import { NavLink } from "react-router-dom";

export default observer(function ConfirmDeleteAsset({ id, assign }) {
  const { modalStore, assetStore } = useStore();

  if (!assign) {
    return (
      <div>
        <ModalHeader className="modal-header-popupModal">
          Are you sure?
        </ModalHeader>
        <ModalBody className="modal-body-popupModal">
          <Row>
            <Col md={12}>
              <Label className="modal-label">
                Do you want to delete this asset?
              </Label>
            </Col>
          </Row>
          <Row className="pt-3">
            <Col md={3}>
              <Button color="danger" onClick={() => assetStore.deleteAsset(id)}>
                Delete
              </Button>{" "}
            </Col>

            <Col md={9}>
              <Button
                style={{
                  color: "gray",
                  border: "1px solid gray",
                  backgroundColor: "white",
                }}
                onClick={modalStore.closeModal}
              >
                Cancel
              </Button>
            </Col>
          </Row>
        </ModalBody>
      </div>
    );
  } else {
    return (
      <div>
        <ModalHeader className="modal-header-popupModal">
          Cannot delete asset
          <span
            style={{
              marginLeft: "130px",
              cursor: "pointer",
              border: "2px solid",
              borderRadius: "2px",
              padding: "0 3px 0 3px",
              fontStyle: "bold",
              float: "right",
            }}
            onClick={modalStore.closeModal}
          >
            &#10006;
          </span>
        </ModalHeader>
        <ModalBody className="modal-body-popupModal">
          <Row>
            <Col md={12}>
              <p>
                Cannot delete the asset because it belongs to one or more
                historical assignments. If the asset is not able to be used
                anymore, please update its state in{" "}
                <NavLink
                  to={`/assets/edit/${id}`}
                  onClick={modalStore.closeModal}
                >
                  Edit Asset Page.
                </NavLink>
              </p>
            </Col>
          </Row>
        </ModalBody>
      </div>
    );
  }
});
