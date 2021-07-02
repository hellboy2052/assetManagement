import { React } from "react";
import { observer } from "mobx-react-lite";
import { Button, ModalHeader, ModalBody, Row, Col } from "reactstrap";
import { useStore } from "../../api/store";

export default observer(function CompleteRequest({ id }) {
  const { modalStore, returnStore } = useStore();
  return (
    <div>
      <ModalHeader className="modal-header-popupModal">
        Are you sure?
      </ModalHeader>
      <ModalBody className="modal-body-popupModal">
        <Row>
          <Col md={12}>
            <p>Do you want to mark this returning request as 'Completed'?</p>
          </Col>
        </Row>
        <Row className="pt-3">
          <Col md={3}>
            <Button
              color="danger"
              onClick={() => returnStore.acceptRequest(id)}
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
