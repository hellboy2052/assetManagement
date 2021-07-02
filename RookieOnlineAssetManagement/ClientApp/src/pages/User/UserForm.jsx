import { Formik, Form } from "formik";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { Button, Header, Label, Segment } from "semantic-ui-react";
import * as Yup from "yup";
import { history } from "../..";
import { useStore } from "../../api/store";
import MyDateInput from "../../components/Form/MyDateInput";
import MyRadioInput from "../../components/Form/MyRadioInput";
import MySelectInput from "../../components/Form/MySelectInput";
import MyTextInput from "../../components/Form/MyTextInput";
import LoadingComponent from "../../components/LoadingComponent";
import { typeOptions } from "../../components/option/TypeOption";

export default observer(function UserForm() {
  const [errors, setError] = useState([]);
  const [user, setUser] = useState({
    id: undefined,
    firstName: "",
    lastName: "",
    doB: new Date("2000-01-01"),
    gender: true,
    joinedDate: new Date(),
    type: "",
  });

  const { userStore, identityStore } = useStore();
  const {
    createUser,
    loadUser,
    loadingInitial,
    setLoadingInitial,
    updateUser,
  } = userStore;
  const { setLocation } = identityStore;

  useEffect(() => {
    setLocation();
  }, [setLocation]);
  const { id } = useParams();

  const validationSchema = Yup.object({
    firstName: Yup.string()
      .min(2, "At least 2 or more character")
      .required("User first name is required")
      .matches(/^(\S+$)/, "No whitespace"),
    lastName: Yup.string().required("User last name is required").trim(),
    doB: Yup.string().required("User birth date is required").nullable(),
    joinedDate: Yup.string().required().nullable(),
    type: Yup.string().required("You need to select type"),
  });

  useEffect(() => {
    if (id) {
      loadUser(id).then((user) => {
        if (!user) {
          history.push("/");
        } else {
          setUser({
            id: user.id || undefined,
            firstName: user.firstName,
            lastName: user.lastName,
            doB: new Date(user.dateOfBirth),
            gender: user.gender,
            joinedDate: new Date(user.joinedDate),
            type: user.type === "Admin" ? 1 : 2,
          });
        }
      });
    } else {
      setLoadingInitial(false);
    }
  }, [id, loadUser, setLoadingInitial]);

  const handleFormSubmit = (user, setSubmitting) => {
    if (!user.id) {
      createUser(user)
        .then(() => {
          history.push("/users");
        })
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
      updateUser(user)
        .then(() => history.push("/users"))
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
    <Segment style={{ width: "45%" }} clearing vertical textAlign="left">
      <Header sub color="red">
        {id ? <strong>Edit User</strong> : <strong>Create New User</strong>}
      </Header>
      <Formik
        validationSchema={validationSchema}
        enableReinitialize
        initialValues={user}
        onSubmit={(value, { setSubmitting }) => {
          handleFormSubmit(value, setSubmitting);
        }}
      >
        {({ handleSubmit, isValid, isSubmitting, dirty }) => (
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
              <MyTextInput name="firstName" label="First Name" disabled={id} />
              <MyTextInput name="lastName" label="Last Name" disabled={id} />
              <MyDateInput
                placeholderText="Date of birth"
                name="doB"
                label="Date of birth"
                timeCaption="time"
                dateFormat="MMMM d, yyyy"
              />
              <MyRadioInput name="gender" label="Gender" />
              <MyDateInput
                placeholderText="Joined date"
                name="joinedDate"
                label="Joined date"
                timeCaption="time"
                dateFormat="MMMM d, yyyy"
              />

              <MySelectInput
                options={typeOptions}
                placeholder="Type"
                name="type"
                label="Type"
              />
            </div>
            <div className="mt-5">
              <Button
                as={Link}
                to="/users"
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
