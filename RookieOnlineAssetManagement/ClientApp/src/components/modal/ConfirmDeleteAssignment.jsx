import { React } from "react";
import { observer } from "mobx-react-lite";
import {
    Button,
    ModalHeader,
    ModalBody,
    Row,
    Col,
    Label,
} from "reactstrap";
import { useStore } from "../../api/store";
import "./PopupModal.css"

export default observer(function ConfirmDeleteAssignment({assignment}) {
    const { modalStore, assignmentStore } = useStore();

    return (
        <div>
            <ModalHeader className="modal-header-popupModal">Are you sure?</ModalHeader>
            <ModalBody className="modal-body-popupModal">
                <Row>
                    <Col md={12}>
                        <Label className="modal-label">Do you want to delete this assignment?</Label>
                    </Col>

                </Row>
                <Row className="pt-3">
                    <Col md={3}>
                        <Button
                            color="danger"
                            onClick={() => assignmentStore.deleteAssignment(assignment)}
                        >
                            Delete
                        </Button>{" "}
                    </Col>

                    <Col md={9}>
                        <Button style={{ color: "gray", border: "1px solid gray", backgroundColor: "white" }}
                            onClick={modalStore.closeModal}
                        >
                            Cancel
                        </Button>
                    </Col>
                </Row>
            </ModalBody>
        </div>
    );
});
