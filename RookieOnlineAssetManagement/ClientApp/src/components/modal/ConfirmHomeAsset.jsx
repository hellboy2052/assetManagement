import { React } from "react";
import { observer } from "mobx-react-lite";
import { Button, ModalHeader, ModalBody, Row, Col } from "reactstrap";
import { useStore } from "../../api/store";
import {
  ACCEPT_ASSIGNMENT_STATE,
  DECLINE_ASSIGNMENT_STATE,
  RETURN_ASSIGNMENT_STATE,
} from "../../constant/assignmentState";
import "./PopupModal.css";

export default observer(function ConfirmHomeAsset({
  assignmentState,
  assignmentId,
}) {
  const {
    modalStore,
    identityStore: { respondAssign, requestReturn },
  } = useStore();

  if (assignmentState === ACCEPT_ASSIGNMENT_STATE) {
    return (
      <div>
        <ModalHeader className="modal-header-popupModal">Are you sure?</ModalHeader>
        <ModalBody className="modal-body-popupModal">
          <Row>
            <Col md={12}>
              <p>Do you want to accept this assignment?</p>
            </Col>
          </Row>
          <Row className="pt-3">
            <Col md={3}>
              <Button
                color="danger"
                onClick={() => {
                  //call Api set state Assignment here <--
                  respondAssign(assignmentId, 1);
                }}
              >
                Accept
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
  } else if (assignmentState === DECLINE_ASSIGNMENT_STATE) {
    return (
      <div>
        <ModalHeader className="modal-header-popupModal">Are you sure?</ModalHeader>
        <ModalBody className="modal-body-popupModal">
          <Row>
            <Col md={12}>
              <p>Do you want to decline this assignment?</p>
            </Col>
          </Row>
          <Row className="pt-3">
            <Col md={3}>
              <Button
                color="danger"
                onClick={() => {
                  //call Api set state Assignment here <--
                  respondAssign(assignmentId, 2);
                }}
              >
                Decline
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
  } else if (assignmentState === RETURN_ASSIGNMENT_STATE) {
    return (
      <div>
        <ModalHeader className="modal-header-popupModal">Are you sure?</ModalHeader>
        <ModalBody className="modal-body-popupModal">
          <Row>
            <Col md={12}>
              <p>Do you want to create a returning request for this asset?</p>
            </Col>
          </Row>
          <Row className="pt-3">
            <Col md={3}>
              <Button
                color="danger"
                onClick={() => requestReturn(assignmentId)}
              >
                Yes
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
  }
});
