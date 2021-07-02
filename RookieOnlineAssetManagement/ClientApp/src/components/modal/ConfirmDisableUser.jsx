import { React } from "react";
import { observer } from "mobx-react-lite";
import { Button, ModalHeader, ModalBody, Row, Col, Label } from "reactstrap";
import { useStore } from "../../api/store";
import "./PopupModal.css";

export default observer(function ConfirmDisableUser({ id, assign }) {
  const { modalStore, userStore } = useStore();
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
                Do you want to disable this user?
              </Label>
            </Col>
          </Row>
          <Row className="pt-3">
            <Col md={3}>
              <Button color="danger" onClick={() => userStore.deleteUser(id)}>
                Disable
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
          Can not disable user
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
              <p className="modal-label">
                There is valid assignments belonging to this user.
              </p>
            </Col>
          </Row>
          <Row>
            <Row>
              <Col md={12}>
                <p className="modal-label">
                  Plese close all assignments before disabling user.
                </p>
              </Col>
            </Row>
          </Row>
        </ModalBody>
      </div>
    );
  }
});
