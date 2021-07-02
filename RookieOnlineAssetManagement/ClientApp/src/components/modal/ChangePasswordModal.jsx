import { React, useState } from "react";
import { observer } from "mobx-react-lite";
import { Button, ModalHeader, ModalBody, Row, Col } from "reactstrap";
import { Label } from "semantic-ui-react";
import { useStore } from "../../api/store";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEye } from "@fortawesome/free-solid-svg-icons";
import "./PopupModal.css";
import { Form, Formik, Field, ErrorMessage } from "formik";
const eye = <FontAwesomeIcon icon={faEye} />;

export default observer(function ChangePasswordModal() {
  const [passwordShown, setPasswordShown] = useState(false);

  const togglePasswordVisiblity = () => {
    setPasswordShown(passwordShown ? false : true);
  };

  const { modalStore, identityStore } = useStore();
  return (
    <>
      <ModalHeader className="modal-header-popupModal">
        Change Password
      </ModalHeader>
      <ModalBody className="modal-body-popupModal">
        <Formik
          initialValues={{
            currentPassword: "",
            newPassword: "",
            error: null,
          }}
          enableReinitialize
          onSubmit={(value, { setErrors }) =>
            identityStore.changePassword(value).catch((error) => {
              setErrors({ error: error[0] });
            })
          }
        >
          {({ handleSubmit, isSubmitting, errors }) => (
            <Form onSubmit={handleSubmit} autoComplete="off">
              <ErrorMessage
                name="error"
                render={() => {
                  return (
                    <Label
                      style={{ marginBottom: 10 }}
                      basic
                      color="red"
                      content={errors.error}
                    />
                  );
                }}
              />
              <Row>
                <Col md={4} style={{ position: "relative" }}>
                  <Label className="modal-label-popupModal">Old password</Label>
                </Col>
                <Col md={8}>
                  <Field
                    className="modal-input-popupModal"
                    type="password"
                    name="currentPassword"
                    id="oldPassword"
                  />
                </Col>
              </Row>
              <Row>
                <Col md={4} style={{ position: "relative" }}>
                  <Label className="modal-label-popupModal">New password</Label>
                </Col>
                <Col md={8} id="newpwd">
                  <Field
                    className="modal-input-popupModal"
                    type={passwordShown ? "text" : "password"}
                    name="newPassword"
                    id="newPassword"
                  />
                  <i onClick={togglePasswordVisiblity}>{eye}</i>
                </Col>
              </Row>
              <div
                className="mt-3"
                style={{ height: "2rem", position: "relative" }}
              >
                <div style={{ right: "0", position: "absolute" }}>
                  <Button
                    className=""
                    color="danger"
                    type="submit"
                    disabled={isSubmitting}
                  >
                    Save
                  </Button>{" "}
                  <Button
                    className=""
                    outline
                    color="secondary"
                    onClick={modalStore.closeModal}
                  >
                    Cancel
                  </Button>
                </div>
              </div>
            </Form>
          )}
        </Formik>
      </ModalBody>
    </>
  );
});
