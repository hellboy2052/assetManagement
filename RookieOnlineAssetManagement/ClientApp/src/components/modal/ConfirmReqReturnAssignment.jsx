import { React } from "react";
import { observer } from "mobx-react-lite";
import { Button, ModalHeader, ModalBody, Row, Col } from "reactstrap";
import { useStore } from "../../api/store";
import "./PopupModal.css";

export default observer(function ConfirmReqReturnAssignment({ id }) {
  const { modalStore, assignmentStore } = useStore();

  return (
    <div>
      <ModalHeader className="modal-header-popupModal">
        Are you sure?
      </ModalHeader>
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
              onClick={() => assignmentStore.requestReturn(id)}
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
              No
            </Button>
          </Col>
        </Row>
      </ModalBody>
    </div>
  );
});
