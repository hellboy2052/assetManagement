import { React } from "react";
import { observer } from "mobx-react-lite";
import {
  Button,
  ModalHeader,
  ModalBody,
  Row,
  Col,
  Form,
  Input,
} from "reactstrap";
import { useStore } from "../../api/store";
import { ErrorMessage, Formik } from "formik";
import "./PopupModal.css";
import { Label } from "semantic-ui-react";

export default observer(function CreateCategory() {
  const { modalStore, categoryStore } = useStore();


  return (
    <div>
      <ModalHeader className="modal-header-popupModal">
        Create new Category
      </ModalHeader>
      <ModalBody className="modal-body-popupModal">
        <Formik
          initialValues={{
            CategoryName: "",
            error: null,
          }}
          enableReinitialize
          onSubmit={(values, { setErrors, setSubmitting }) => {
            categoryStore.createCategory(values).catch((error) => {
              setSubmitting(false);
              setErrors({ error: error[0] });
            });
          }}
        >
          {({ handleSubmit, errors, handleChange, isSubmitting }) => (
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
                <Col md={12}>
                  <p>New category</p>
                </Col>
                <Input
                  type="text"
                  name="CategoryName"
                  id="categoryName"
                  onChange={handleChange}
                  placeholder="Name of Category"
                />
              </Row>
              <Row className="pt-3">
                <Col md={3}>
                  <Button color="danger" type="submit" disabled={isSubmitting}>
                    Create
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
            </Form>
          )}
        </Formik>
      </ModalBody>
    </div>
  );
});
