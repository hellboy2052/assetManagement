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
export default observer(function ChangePasswordFirstLoginModal() {
  const [passwordShown, setPasswordShown] = useState(false);

  const togglePasswordVisiblity = () => {
    setPasswordShown(passwordShown ? false : true);
  };

  const { identityStore } = useStore();
  return (
    <>
      <ModalHeader className="modal-header-popupModal">
        Change Password
      </ModalHeader>
      <ModalBody className="modal-body-popupModal">
        <Formik
          initialValues={{
            newPassword: "",
            error: null,
          }}
          enableReinitialize
          onSubmit={(value, { setErrors }) =>
            identityStore.resetPassword(value.newPassword).catch((error) => {
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
              <Row className="mb-2">
                <Label>This is the first time you logged in.</Label>
              </Row>
              <Row className="mb-2">
                <Label>You have to change your password to continue</Label>
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
              <div style={{ height: "4rem", position: "relative" }}>
                <div style={{ right: "0", position: "absolute" }}>
                  <Button
                    className="modal-save-button-popupModal"
                    color="danger"
                    type="submit"
                    disabled={isSubmitting}
                  >
                    Save
                  </Button>{" "}
                </div>
              </div>
            </Form>
          )}
        </Formik>
      </ModalBody>
    </>
  );
});
