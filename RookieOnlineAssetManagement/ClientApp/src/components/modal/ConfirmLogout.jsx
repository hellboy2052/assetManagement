import { React } from "react";
import { observer } from "mobx-react-lite";
import {
    Button,
    ModalHeader,
    ModalBody,
    Row,
    Col,
} from "reactstrap";
import { useStore } from "../../api/store";
import "./PopupModal.css"

export default observer(function SignOut() {
    const {modalStore, identityStore} = useStore();
    const {logout} = identityStore;

    return (
        <div>
            <ModalHeader className="modal-header-popupModal">Logout</ModalHeader>
            <ModalBody className="modal-body-popupModal">
                <Row>
                    <Col md={12}>
                        <p>Do you want to sign out?</p>
                    </Col>

                </Row>
                <Row className="pt-3">
                    <Col md={3}>
                        <Button
                            color="danger"
                            onClick={() => logout()}
                        >
                            Logout
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
