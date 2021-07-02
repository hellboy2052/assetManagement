import { Formik, Form, Field } from "formik";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { Button, Header, Label, Segment, Grid } from "semantic-ui-react";
import * as Yup from "yup";
import MyDateInput from "../../components/Form/MyDateInput";
import MyTextArea from "../../components/Form/MyTextArea";
import SelectAssetTable from "../../components/modal/CreateAssignment/SelectAssetTable";
import SelectUserTable from "../../components/modal/CreateAssignment/SelectUserTable";
import { useStore } from "../../api/store";
import { history } from "../..";
import LoadingComponent from "../../components/LoadingComponent";

export default observer(function AssignmentForm() {
  const { modalStore, assignmentStore, identityStore } = useStore();
  const {
    createAssignment,
    updateAssignment,
    loadEditAssignment,
    loadingInitial,
    setLoadingInitial,
  } = assignmentStore;
  const { setLocation } = identityStore;
  const [errors, setError] = useState([]);
  const [assignment, SetAssignment] = useState({
    AssignmentId: undefined,
    User: {
      id: undefined,
      Fullname: "",
    },
    Asset: {
      AssetCode: undefined,
      AssestName: "",
    },
    AssignedDate: null,
    Note: "",
  });

  const { id } = useParams();

  useEffect(() => {
    setLocation();
  }, [setLocation]);

  useEffect(() => {
    if (id) {
      loadEditAssignment(id).then((assignment) => {
        if (!assignment) {
          history.push("/assignments");
        } else {
          SetAssignment({
            AssignmentId: assignment.assignmentId,
            User: {
              id: assignment.userId,
              Fullname: assignment.userName,
            },
            Asset: {
              AssetCode: assignment.assetCode,
              AssestName: assignment.assetName,
            },
            AssignedDate: new Date(assignment.assignedDate) || null,
            Note: assignment.note || "",
          });
        }
      });
    } else {
      setLoadingInitial(false);
    }
  }, [id, loadEditAssignment, setLoadingInitial]);

  const validationSchema = Yup.object({
    User: Yup.object().shape({
      Fullname: Yup.string().required("You need to select User"),
      id: Yup.string().required("You need to select User"),
    }),
    Asset: Yup.object().shape({
      AssestName: Yup.string().required("You need to select Asset"),
      AssetCode: Yup.string().required("You need to select Asset"),
    }),
    AssignedDate: Yup.string().required("Assigned Date is required").nullable(),
    Note: Yup.string().notRequired(),
  });

  const handleFormSubmit = (assignment, setSubmitting) => {
    if (!assignment.AssignmentId) {
      createAssignment(assignment)
        .then(() => history.push("/assignments"))
        .catch((err) => {
          if (Array.isArray(err)) {
            setError(err);
            setSubmitting(false);
          } else {
            console.log(err);
            setSubmitting(false);
          }
        });
    } else {
      updateAssignment(assignment)
        .then(() => history.push("/assignments"))
        .catch((err) => {
          if (Array.isArray(err)) {
            setError(err);
            setSubmitting(false);
          } else {
            console.log(err);
            setSubmitting(false);
          }
        });
    }
  };

  if (loadingInitial) {
    return <LoadingComponent content="Loading form...." />;
  }
  return (
    <Segment style={{ width: "50%" }} clearing vertical textAlign="left">
      <Header sub color="red">
        {id ? (
          <strong>Edit Assignment</strong>
        ) : (
          <strong>Create New Assignment</strong>
        )}
      </Header>
      <Formik
        enableReinitialize
        validationSchema={validationSchema}
        initialValues={assignment}
        onSubmit={(value, { setSubmitting }) =>
          handleFormSubmit(value, setSubmitting)
        }
      >
        {({
          handleSubmit,
          isValid,
          isSubmitting,
          dirty,
          values,
          setFieldValue,
          setFieldTouched,
        }) => (
          <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
            {errors.length !== 0 &&
              errors.map((err, i) => (
                <Label
                  key={i}
                  style={{ marginBottom: 10 }}
                  basic
                  color="red"
                  content={err}
                />
              ))}
            <div className="mt-4">
              <Grid>
                <Grid.Column verticalAlign="middle" width={5}>
                  <label>User:</label>
                </Grid.Column>
                <Grid.Column verticalAlign="middle" width={11}>
                  <div className="d-inline-flex w-100">
                    <Field readOnly={true} name="User.Fullname" placeholder="Select the user" />
                    <Button
                      size="mini"
                      type="button"
                      icon="search"
                      onClick={() =>
                        modalStore.openModal(
                          <SelectUserTable
                            handleSelectUser={setFieldValue}
                            handleSetTouch={setFieldTouched}
                          />
                        )
                      }
                    />
                  </div>
                </Grid.Column>
              </Grid>
              <Grid>
                <Grid.Column verticalAlign="middle" width={5}>
                  <label>Asset:</label>
                </Grid.Column>
                <Grid.Column verticalAlign="middle" width={11}>
                  <div className="d-inline-flex w-100">
                    <Field readOnly={true} name="Asset.AssestName" placeholder="Select the asset" />
                    <Button
                      size="mini"
                      icon="search"
                      type="button"
                      onClick={() =>
                        modalStore.openModal(
                          <SelectAssetTable
                            handleSelectAsset={setFieldValue}
                            handleSetTouch={setFieldTouched}
                          />
                        )
                      }
                    />
                  </div>
                </Grid.Column>
              </Grid>

              <MyDateInput
                placeholderText="Select assigned date"
                name="AssignedDate"
                label="Assigned Date"
                timeCaption="time"
                dateFormat="MMMM d, yyyy"
                minDate={new Date()}
              />
              <MyTextArea rows={4} label="Note" placeholder="Input some note here..." name="Note" />
            </div>
            <div className="mt-5">
              <Button
                as={Link}
                to="/assignments"
                floated="right"
                size="mini"
                type="button"
                content="Cancel"
              />
              <Button
                disabled={isSubmitting || !dirty || !isValid}
                loading={isSubmitting}
                floated="right"
                color="red"
                size="mini"
                type="submit"
                content={"Save"}
              />
            </div>
          </Form>
        )}
      </Formik>
    </Segment>
  );
});
