import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Formik, Form } from "formik";
import * as Yup from "yup";
import MyDateInput from "../../components/Form/MyDateInput";
import MySelectInput from "../../components/Form/MySelectInput";
import MyTextInput from "../../components/Form/MyTextInput";
import { Button, Header, Segment, Label } from "semantic-ui-react";
import { useStore } from "../../api/store";
import { Link, useParams } from "react-router-dom";
import MyTextArea from "../../components/Form/MyTextArea";
import LoadingComponent from "../../components/LoadingComponent";
import MyStateRadio from "../../components/Form/MyStateRadio.jsx.jsx";
import { stateOptions } from "../../components/option/StateOption";
import CreateCategory from "../../components/modal/CreateCategory";
import "./AssetForm.css";
import { history } from "../..";

export default observer(function AssetForm() {
  const [errors, setError] = useState([]);
  const [asset, setAsset] = useState({
    assetCode: undefined,
    assestName: "",
    categoryId: "",
    specification: "",
    installDate: new Date(),
    state: 1,
  });
  const { categoryStore, modalStore, assetStore, identityStore } = useStore();
  const { categoryOption, loadingInitial, optionRegistry, loadCategories } =
    categoryStore;

  const { id } = useParams();

  const { setLocation } = identityStore;

  useEffect(() => {
    setLocation();
  }, [setLocation]);

  useEffect(() => {
    if (optionRegistry.size <= 0) {
      loadCategories();
    }
  }, [optionRegistry.size, loadCategories]);

  useEffect(() => {
    if (id) {
      assetStore.loadAsset(id).then((asset) => {
        if (!asset || asset.state === "Assigned") {
          history.push("/assets");
        } else {
          setAsset({
            assetCode: asset.assetCode,
            assestName: asset.assetName,
            categoryId: categoryOption.find((c) => c.text === asset.category)
              .value,
            specification: asset.specification,
            installDate: new Date(asset.installDate),
            state: stateOptions.find((s) => s.text === asset.state).value || 1,
          });
        }
      });
    } else {
      assetStore.setLoadingInitial(false);
    }
  }, [id, assetStore, assetStore.setLoadingInitial, categoryOption]);

  const validationSchema = Yup.object({
    assestName: Yup.string()
      .min(2, "At least 2 or more character")
      .required("Asset name is required")
      .trim(),
    categoryId: Yup.string().required("You need to select a category"),
    specification: Yup.string().required().trim(),
    installDate: Yup.string().required("Install Date is required").nullable(),
    state: Yup.number().required(),
  });

  const state = id
    ? stateOptions.filter((s) => s.text !== "Assigned")
    : stateOptions.filter(
        (s) => (s.text === "Available") | (s.text === "NotAvailable")
      );

  const handleFormSubmit = (asset, setSubmitting) => {
    if (!asset.assetCode) {
      assetStore
        .createAsset(asset)
        .then(() => history.push("/assets"))
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
      assetStore
        .updateAsset(asset)
        .then(() => history.push("/assets"))
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

  if (loadingInitial || assetStore.loadingInitial) {
    return <LoadingComponent content="Loading form...." />;
  }
  return (
    <Segment style={{ width: "45%" }} clearing vertical textAlign="left">
      <Header sub color="red">
        {id ? <strong>Edit Asset</strong> : <strong>Create New Asset</strong>}
      </Header>
      <Formik
        validationSchema={validationSchema}
        enableReinitialize
        initialValues={asset}
        onSubmit={(value, { setSubmitting }) =>
          handleFormSubmit(value, setSubmitting)
        }
      >
        {({ handleSubmit, isValid, isSubmitting, dirty, values }) => (
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
              <MyTextInput name="assestName" label="Name" />
              <div style={{ position: "relative" }}>
                <MySelectInput
                  options={categoryOption}
                  placeholder=""
                  name="categoryId"
                  label="Category"
                  disabled={id !== undefined}
                />
                <Button
                  type="button"
                  id="create-category-button"
                  onClick={() => modalStore.openModal(<CreateCategory />, "sm")}
                  disabled={id !== undefined}
                >
                  +
                </Button>
              </div>
              <MyTextArea
                rows={4}
                label="Specification"
                placeholder="Specification"
                name="specification"
              />
              <MyDateInput
                placeholderText=""
                name="installDate"
                label="Install Date"
                timeCaption="time"
                dateFormat="MMMM d, yyyy"
              />
              <MyStateRadio name="state" label="State" option={state} />
            </div>
            <div className="mt-5">
              <Button
                as={Link}
                to="/assets"
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
